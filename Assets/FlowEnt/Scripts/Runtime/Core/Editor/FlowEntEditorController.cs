#if UNITY_EDITOR
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

    public class FlowEntEditorController : IUpdateController
    {
        protected static readonly object lockObject = new object();
        protected static FlowEntEditorController instance;
        public static FlowEntEditorController Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == default)
                    {
                        instance = new FlowEntEditorController();
                        instance.Init();
                    }
                }
                return instance;
            }
        }

        private readonly UpdatablesFastList<AbstractUpdatable> updatables = new UpdatablesFastList<AbstractUpdatable>();

        private int? undoGroupId;
        private IPreviewable previewProperty;
        public bool IsInPreview => previewProperty != null;
        private float lastTimeSinceStartup;
        private float editorDeltaTime;

        public void Init()
        {
            lastTimeSinceStartup = (float)EditorApplication.timeSinceStartup;
            EditorApplication.update += Update;
            Undo.postprocessModifications += PostprocessModificationsCallback;
        }

        UndoPropertyModification[] PostprocessModificationsCallback(UndoPropertyModification[] modifications)
        {
            return modifications;
        }

        private void Update()
        {
            editorDeltaTime = (float)EditorApplication.timeSinceStartup - lastTimeSinceStartup;
            lastTimeSinceStartup = (float)EditorApplication.timeSinceStartup;
            try
            {
                FlowEntController.Update(updatables, editorDeltaTime);
            }
            catch (Exception ex)
            {
                FlowEntDebug.LogError(
                    $"<color={FlowEntConstants.Red}><b>Exception on update</b></color>\n" +
                    $"<color={FlowEntConstants.Orange}><b>The preview animation is throwing an exception</b></color>:\n\n" +
                    $"<b>Exception</b>:\n{ex}");
                StopPreview();
            }

            if (previewProperty == null)
            {
                return;
            }

            foreach (UnityEditor.Editor item in ActiveEditorTracker.sharedTracker.activeEditors)
            {
                if (item.serializedObject == previewProperty?.SerializedObject)
                {
                    return;
                }
            }
            StopPreview();
        }

        public void SubscribeToUpdate(AbstractUpdatable updatable)
        {
            updatables.Add(updatable);
        }

        public void UnsubscribeFromUpdate(AbstractUpdatable updatable)
        {
            updatables.Remove(updatable);
        }

        public void StartPreview(IPreviewable previewProperty)
        {
            this.previewProperty = previewProperty;

            UnityEngine.Object[] getObjects()
            {
                IMotion[] motions = this.previewProperty.PreviewAnimation.GetFieldValue<IMotion[]>("motions");
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

        public void StopPreview()
        {
            previewProperty?.Reset();
            previewProperty = null;

            if (undoGroupId != null)
            {
                Undo.RevertAllDownToGroup(undoGroupId.Value);
                undoGroupId = null;
            }
        }
    }
}
#endif