#if UNITY_EDITOR
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
            EditorApplication.update += Update;
        }

        public void SubscribeToUpdate(AbstractUpdatable updatable)
        {
            updatables.Add(updatable);
        }

        public void UnsubscribeFromUpdate(AbstractUpdatable updatable)
        {
            updatables.Remove(updatable);
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            AbstractUpdatable index = updatables.anchor.next;

            while (index != null)
            {
#if FlowEnt_Debug || (UNITY_EDITOR && FlowEnt_Debug_Editor)
                try
                {
#endif
                index.UpdateInternal(deltaTime);
#if FlowEnt_Debug || (UNITY_EDITOR && FlowEnt_Debug_Editor)
                }
                catch (Exception ex)
                {
                    FlowEntDebug.LogError(
                        $"<color={FlowEntConstants.Red}><b>Exception on update</b></color>\n" +
                        $"<color={FlowEntConstants.Orange}><b>Origin of animation that generated the exception</b></color>:\n" +
                        $"<color={FlowEntConstants.Orange}>{index.stackTrace}</color>\n\n" +
                        $"<b>Exception</b>:\n{ex}");
                }
#endif
                index = index.next;
            }
        }
    }
}
#endif