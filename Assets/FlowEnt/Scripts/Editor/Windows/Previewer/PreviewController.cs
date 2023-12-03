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
        public class Options
        {
            public AbstractAnimation Animation { get; set; }
            public Action OnStop { get; set; }
        }

        static PreviewController()
        {
            EditorApplication.update += Update;
            FlowEntEditorUpdater.OnException += () => Stop();
        }

        private static int? undoGroupId;
        private static Options options;
        public static bool IsRunning => options != null;

        public static void Start(Options options)
        {
            if (IsRunning)
            {
                Stop();
            }

            PreviewController.options = options;

            if (RecordObjects(PreviewController.options.Animation))
            {
                undoGroupId = Undo.GetCurrentGroup();
                Undo.IncrementCurrentGroup();
            }

            options.Animation.OnCompleted(Reset);
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
                Debug.LogException(ex);
                Stop();
            }
        }

        /// <summary>
        /// Stops all running animations and undoes all the changes done while playing
        /// </summary>
        public static void Stop()
        {
            options?.Animation?.Stop();
            Reset();
        }

        private static void Reset()
        {
            options?.Animation?.Reset();
            options?.OnStop?.Invoke();
            options = null;

            if (undoGroupId == null)
            {
                return;
            }

            //HACK this is to not undo the change that might have triggered the reset
            Undo.postprocessModifications += modifications => modifications.SkipLast(1).ToArray();
            Undo.RevertAllDownToGroup(undoGroupId.Value);
            undoGroupId = null;
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
                    hasRecordedObjects |= RecordObjects(animation);
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