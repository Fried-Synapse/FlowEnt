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
                GameObject gameObject = new("AsyncRunnerUnityContext");
                gameObject.hideFlags = HideFlags.HideInHierarchy;
                return gameObject.AddComponent<AsyncQuickitContext>();
            }
        }

        internal Queue<Exception> ExceptionsToBePrinted { get; } = new();

        private void Update()
        {
            while (ExceptionsToBePrinted.Count > 0)
            {
                Exception exception = ExceptionsToBePrinted.Dequeue();
                Debug.LogException(exception);
            }
        }
    }
}