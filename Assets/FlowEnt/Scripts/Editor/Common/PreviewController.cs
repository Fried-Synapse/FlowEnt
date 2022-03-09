using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FriedSynapse.FlowEnt.Motions.Abstract;
using FriedSynapse.FlowEnt.Reflection;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public interface IPreviewable
    {
        public SerializedObject SerializedObject { get; }
        public AbstractAnimation Animation { get; }
        public void Reset();
    }

    public static class PreviewController
    {
        static PreviewController()
        {
            EditorApplication.update += Update;
            FlowEntEditorUpdater.OnException += StopPreview;
        }

        private static int? undoGroupId;
        private static List<UnityEngine.Object> previewObjects;
        private static IPreviewable preview;

        public static void StartPreview(IPreviewable preview)
        {
            PreviewController.preview = preview;

            previewObjects = GetObjects(preview.Animation).ToList();
            if (previewObjects.Count > 0)
            {
                undoGroupId = Undo.GetCurrentGroup();
                Undo.IncrementCurrentGroup();
                Undo.RecordObjects(previewObjects.ToArray(), "FlowEnt Animation Preview");
            }
        }

        public static void StopPreview()
        {
            preview?.Reset();
            preview = null;

            if (undoGroupId != null)
            {
                Undo.RevertAllDownToGroup(undoGroupId.Value);
                undoGroupId = null;
                GUIUtility.ExitGUI();
            }
        }

        private static void Update()
        {
            if (preview != null)
            {
                foreach (UnityEditor.Editor item in ActiveEditorTracker.sharedTracker.activeEditors)
                {
                    if (item.serializedObject == preview?.SerializedObject)
                    {
                        return;
                    }
                }
                StopPreview();
            }
        }

        private static IEnumerable<UnityEngine.Object> GetObjects(AbstractAnimation animation)
            => animation is Flow flow ? GetFlowObjects(flow) : GetAnimationObjects(animation);

        private static IEnumerable<UnityEngine.Object> GetFlowObjects(Flow flow)
        {
            IList updatableWrappers = flow.GetFieldValue<IList>("updatableWrappersQueue");
            List<UnityEngine.Object> result = new List<UnityEngine.Object>();

            void addObjects(object updatableWrapper)
            {
                AbstractUpdatable updatable = updatableWrapper.GetFieldValue<AbstractUpdatable>("updatable");
                if (updatable != null && updatable is AbstractAnimation animation)
                {
                    result.AddRange(GetObjects(animation));
                }
                object next = updatableWrapper.GetFieldValue<object>("next");
                if (next != null)
                {
                    addObjects(next);
                }
            }

            foreach (object updatableWrapper in updatableWrappers)
            {
                addObjects(updatableWrapper);
            }

            return result;
        }

        private static IEnumerable<UnityEngine.Object> GetAnimationObjects(AbstractAnimation animation)
        {
            IMotion[] motions = animation.GetFieldValue<IMotion[]>("motions");
            List<UnityEngine.Object> result = new List<UnityEngine.Object>();
            Type type = typeof(UnityEngine.Object);
            foreach (IMotion motion in motions)
            {
                List<UnityEngine.Object> allObjects = motion
                    .GetType()
                    .GetFields()
                    .Where(fi => type.IsAssignableFrom(fi.FieldType))
                    .Select(fi => (UnityEngine.Object)fi.GetValue(motion))
                    .ToList();

                allObjects.AddRange(motion
                    .GetType()
                    .GetProperties()
                    .Where(pi => type.IsAssignableFrom(pi.PropertyType))
                    .Select(pi => (UnityEngine.Object)pi.GetValue(motion)));

                IEnumerable<UnityEngine.Object> objects = allObjects
                    .Distinct()
                    .Where(o => o != null);

                result.AddRange(objects);
            }

            return result;
        }
    }
}
