#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using FriedSynapse.FlowEnt.Motions.Abstract;
using FriedSynapse.FlowEnt.Reflection;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
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

        public void Init()
        {
            EditorApplication.update += () => FlowEntController.Update(updatables, Time.deltaTime);
        }

        public void SubscribeToUpdate(AbstractUpdatable updatable)
        {
            updatables.Add(updatable);
        }

        public void UnsubscribeFromUpdate(AbstractUpdatable updatable)
        {
            updatables.Remove(updatable);
        }

        private int? undoGroupId;
        private AbstractAnimation previewAnimation;
        public AbstractAnimation PreviewAnimation => previewAnimation;

        public void StartPreview(AbstractAnimation previewAnimation, bool shouldOverride = true)
        {
            if (undoGroupId != null)
            {
                if (shouldOverride)
                {
                    StopPreview();
                }
                else
                {
                    return;
                }
            }

            this.previewAnimation = previewAnimation;

            UnityEngine.Object[]
            getObjects()
            {
                IMotion[] motions = previewAnimation.GetFieldValue<IMotion[]>("motions");
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

            UnityEngine.Object[]
            objects = getObjects();
            if (objects.Length > 0)
            {
                undoGroupId = Undo.GetCurrentGroup();
                Undo.IncrementCurrentGroup();
                Undo.RecordObjects(objects, "Animation Preview");
            }
        }

        public void StopPreview()
        {
            previewAnimation?.Stop();
            previewAnimation = null;
            if (undoGroupId != null)
            {
                Undo.RevertAllDownToGroup(undoGroupId.Value);
                undoGroupId = null;
            }
        }
    }
}
#endif