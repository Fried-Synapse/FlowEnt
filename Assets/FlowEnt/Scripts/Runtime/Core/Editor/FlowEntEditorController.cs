#if UNITY_EDITOR
using System.Reflection;
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
    }
}
#endif