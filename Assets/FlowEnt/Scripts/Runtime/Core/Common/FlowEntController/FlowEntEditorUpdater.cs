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

        public void SetController(FlowEntController controller)
        {
            this.controller = controller;
        }

        private void Update()
        {
            editorDeltaTime = (float)EditorApplication.timeSinceStartup - lastTimeSinceStartup;
            lastTimeSinceStartup = (float)EditorApplication.timeSinceStartup;
            try
            {
                controller.Update(editorDeltaTime, editorDeltaTime);
                controller.LateUpdate(editorDeltaTime, editorDeltaTime);
                controller.FixedUpdate(editorDeltaTime);
                //TODO this should be custom
                controller.CustomUpdate(editorDeltaTime);
            }
            catch (Exception ex)
            {
                FlowEntDebug.LogError(
                    $"<color={FlowEntConstants.Red}><b>Exception on update</b></color>\n" +
                    $"<color={FlowEntConstants.Orange}><b>The preview animation is throwing an exception</b></color>:\n\n" +
                    $"<b>Exception</b>:\n{ex}");
                OnException?.Invoke();
            }
        }
    }
}
#endif