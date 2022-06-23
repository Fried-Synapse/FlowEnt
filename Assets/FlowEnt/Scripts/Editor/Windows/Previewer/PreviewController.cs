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
    public static class PreviewController
    {
        static PreviewController()
        {
            EditorApplication.update += Update;
            FlowEntEditorUpdater.OnException += () => Stop();
        }

        private static int? undoGroupId;
        private static AbstractAnimation animation;
        public static bool IsRunning => animation != null;

        public static void Start(AbstractAnimation animation)
        {
            PreviewController.animation = animation;

            if (RecordObjects(PreviewController.animation))
            {
                undoGroupId = Undo.GetCurrentGroup();
                Undo.IncrementCurrentGroup();
            }
            try
            {
                animation.Start();
            }
            catch (Exception ex)
            {
                FlowEntDebug.LogError(
                    $"<color={FlowEntConstants.Red}><b>Exception on starting animation</b></color>\n" +
                    $"<color={FlowEntConstants.Orange}><b>The preview animation is throwing an exception</b></color>:\n\n" +
                    $"<b>Exception</b>:\n{ex}");
                Debug.LogException(ex);
                Stop();
            }
        }

        public static void Stop()
        {
            animation?.Stop(true).Reset();
            animation = null;

            if (undoGroupId != null)
            {
                Undo.RevertAllDownToGroup(undoGroupId.Value);
                undoGroupId = null;
            }
        }

        private static void Update()
        {
            if (!UnityEditorInternal.InternalEditorUtility.isApplicationActive && IsRunning)
            {
                Stop();
            }
        }

        private static bool RecordObjects(AbstractAnimation animation)
            => animation is Flow flow ? RecordFlowObjects(flow) : RecordAnimationObjects(animation);

        private static bool RecordFlowObjects(Flow flow)
        {
            bool hasRecordedObjects = false;

            IList updatableWrappers = flow.GetFieldValue<IList>("updatableWrappersQueue");

            void addObjects(object updatableWrapper)
            {
                AbstractUpdatable updatable = updatableWrapper.GetFieldValue<AbstractUpdatable>("updatable");
                if (updatable != null && updatable is AbstractAnimation animation)
                {
                    hasRecordedObjects = RecordObjects(animation);
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
            return hasRecordedObjects;
        }

        private static bool RecordAnimationObjects(AbstractAnimation animation)
        {
            bool hasRecordedObjects = false;
            IMotion[] motions = animation.GetFieldValue<IMotion[]>("motions");
            Type type = typeof(UnityEngine.Object);
            foreach (IMotion motion in motions)
            {
                List<UnityEngine.Object> objects = motion
                    .GetType()
                    .GetFields()
                    .Where(fi => type.IsAssignableFrom(fi.FieldType))
                    .Select(fi => (UnityEngine.Object)fi.GetValue(motion))
                    .ToList();

                objects.AddRange(motion
                    .GetType()
                    .GetProperties()
                    .Where(pi => type.IsAssignableFrom(pi.PropertyType))
                    .Select(pi => (UnityEngine.Object)pi.GetValue(motion)));

                objects = objects
                    .Distinct()
                    .Where(o => o != null)
                    .ToList();

                if (objects.Count > 0)
                {
                    hasRecordedObjects = true;
                    void record()
                    {
                        Undo.RegisterCompleteObjectUndo(objects.ToArray(), "FlowEnt Animation Preview");
                    }

                    switch (animation)
                    {
                        case Echo echo:
                            echo.OnStarting(record);
                            break;
                        case Tween tween:
                            tween.OnStarting(record);
                            break;
                    }
                }
            }
            return hasRecordedObjects;
        }
    }
}
