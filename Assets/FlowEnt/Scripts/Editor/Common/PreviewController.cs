using System;
using System.Collections.Generic;
using System.Linq;
using FriedSynapse.FlowEnt.Motions.Abstract;
using FriedSynapse.FlowEnt.Reflection;
using UnityEditor;

namespace FriedSynapse.FlowEnt.Editor
{
    public interface IPreviewable
    {
        public SerializedObject SerializedObject { get; }
        public AbstractAnimation PreviewAnimation { get; }
        public void Reset();
    }

    public static class PreviewController
    {
        static PreviewController()
        {
            EditorApplication.update += Update;
            Undo.postprocessModifications += PostprocessModificationsCallback;
            FlowEntEditorController.OnException += StopPreview;
        }

        private static int? undoGroupId;
        private static IPreviewable preview;

        private static UndoPropertyModification[] PostprocessModificationsCallback(UndoPropertyModification[] modifications)
        {
            return modifications;
        }

        private static void Update()
        {
            if (preview == null)
            {
                return;
            }

            foreach (UnityEditor.Editor item in ActiveEditorTracker.sharedTracker.activeEditors)
            {
                if (item.serializedObject == preview?.SerializedObject)
                {
                    return;
                }
            }
            StopPreview();
        }

        public static void StartPreview(IPreviewable preview)
        {
            PreviewController.preview = preview;

            UnityEngine.Object[] getObjects()
            {
                IMotion[] motions = PreviewController.preview.PreviewAnimation.GetFieldValue<IMotion[]>("motions");
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
                return result.ToArray();
            }

            UnityEngine.Object[] objects = getObjects();
            if (objects?.Length > 0)
            {
                undoGroupId = Undo.GetCurrentGroup();
                Undo.IncrementCurrentGroup();
                Undo.RecordObjects(objects, "Animation Preview");
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
            }
        }
    }
}
