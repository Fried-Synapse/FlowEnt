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
    public class PreviewOptions
    {
        public PreviewOptions(AbstractAnimation animation)
        {
            Animation = animation;
        }

        public AbstractAnimation Animation { get; }
        public Action OnUpdate { get; set; }
        public Action OnStop { get; set; }
    }

    public static class PreviewController
    {
        static PreviewController()
        {
            EditorApplication.update += Update;
            FlowEntEditorUpdater.OnException += () => Stop();
        }

        private static int? undoGroupId;
        private static PreviewOptions options;

        public static void Start(PreviewOptions options)
        {
            Stop();
            PreviewController.options = options;

            if (RecordObjects(PreviewController.options.Animation))
            {
                undoGroupId = Undo.GetCurrentGroup();
                Undo.IncrementCurrentGroup();
            }
            try
            {
                options.Animation.Start();
            }
            catch (Exception ex)
            {
                FlowEntDebug.LogError(
                    $"<color={FlowEntConstants.Red}><b>Exception on starting animation</b></color>\n" +
                    $"<color={FlowEntConstants.Orange}><b>The preview animation is throwing an exception</b></color>:\n\n" +
                    $"<b>Exception</b>:\n{ex}");
                Stop();
            }
        }

        public static void Stop(bool exitGui = true)
        {
            options?.Animation?.Stop().Reset();
            options?.OnStop?.Invoke();
            options = null;

            if (undoGroupId != null)
            {
                Undo.RevertAllDownToGroup(undoGroupId.Value);
                undoGroupId = null;
                if (exitGui)
                {
                    GUIUtility.ExitGUI();
                }
            }
        }

        private static void Update()
        {
            options?.OnUpdate?.Invoke();
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
