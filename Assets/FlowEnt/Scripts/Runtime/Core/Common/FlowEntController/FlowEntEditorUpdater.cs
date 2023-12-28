#if UNITY_EDITOR
using System;
using UnityEditor;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Editor updater for the <see cref="FlowEntController" />.
    /// </summary>
    public class FlowEntEditorUpdater : IFlowEntUpdater
    {
        public FlowEntEditorUpdater()
        {
            lastTimeSinceStartup = (float)EditorApplication.timeSinceStartup;
            EditorApplication.update += Update;
        }
        private FlowEntController controller;
        private float lastTimeSinceStartup;
        private float editorDeltaTime;
        public static Action OnException { get; set; }

        void IFlowEntUpdater.SetController(FlowEntController controller)
        {
            this.controller = controller;
        }

        DeltaTimes IFlowEntUpdater.GetDeltaTimes()
            => new()
            {
                deltaTime = editorDeltaTime,
                smoothDeltaTime = editorDeltaTime,
                lateDeltaTime = editorDeltaTime,
                lateSmoothDeltaTime = editorDeltaTime,
                fixedDeltaTime = editorDeltaTime,
            };

        private void Update()
        {
            editorDeltaTime = (float)EditorApplication.timeSinceStartup - lastTimeSinceStartup;
            lastTimeSinceStartup = (float)EditorApplication.timeSinceStartup;
            try
            {
                controller.Update(editorDeltaTime, editorDeltaTime);
                controller.LateUpdate(editorDeltaTime, editorDeltaTime);
                controller.FixedUpdate(editorDeltaTime);
                controller.CustomUpdate(editorDeltaTime);
            }
            catch (Exception ex)
            {
                FlowEntDebug.LogError(
                    $"<color={FlowEntInternalConstants.Red}><b>Exception on update</b></color>\n" +
                    $"<color={FlowEntInternalConstants.Orange}><b>The preview animation is throwing an exception</b></color>:\n\n" +
                    $"<b>Exception</b>:\n{ex}");
                OnException?.Invoke();
            }
        }
    }
}
#endif