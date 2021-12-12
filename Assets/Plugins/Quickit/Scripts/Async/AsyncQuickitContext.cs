using System;
using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.Quickit
{
    public class AsyncQuickitContext : SingletonBehaviour<AsyncQuickitContext, AsyncQuickitContext.SingletonFactory>
    {
        public class SingletonFactory : ISingletonFactory<AsyncQuickitContext>
        {
            public AsyncQuickitContext CreateInstance()
            {
                GameObject gameObject = new GameObject("AsyncRunnerUnityContext");
                gameObject.hideFlags = HideFlags.HideInHierarchy;
                return gameObject.AddComponent<AsyncQuickitContext>();
            }
        }
        internal Queue<Exception> ExceptionsToBePrinted { get; } = new Queue<Exception>();

#pragma warning disable IDE0051, RCS1213
        private void Update()
        {
            while (ExceptionsToBePrinted.Count > 0)
            {
                Exception exception = ExceptionsToBePrinted.Dequeue();
                Debug.LogException(exception);
            }
        }
#pragma warning restore IDE0051, RCS1213
    }
}
