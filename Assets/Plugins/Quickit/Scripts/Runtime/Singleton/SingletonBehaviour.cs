using UnityEngine;

namespace FriedSynapse.Quickit
{
    #region With Factory

    public abstract class SingletonBehaviour<TInstance, TInstanceFactory> : MonoBehaviour
        where TInstance : SingletonBehaviour<TInstance, TInstanceFactory>
        where TInstanceFactory : ISingletonFactory<TInstance>, new()
    {
        protected static readonly object lockObject = new object();
        protected static TInstance instance;
        public static TInstance Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == default)
                    {
                        instance = new TInstanceFactory().CreateInstance();
                    }
                }
                return instance;
            }
        }

#pragma warning disable RCS1158
        public static bool HasInstance => Instance != default;
#pragma warning restore RCS1158

        protected void ResetInstance()
        {
            instance = default;
        }

        protected virtual void OnDestroy()
        {
            ResetInstance();
        }
    }

    #endregion

    #region Auto

    public class AutoSingletonBehaviourInstanceFactory<TInstance> : ISingletonFactory<TInstance>
        where TInstance : AutoSingletonBehaviour<TInstance>
    {
        public TInstance CreateInstance()
        {
            GameObject gameObject = new GameObject(typeof(TInstance).Name);
            gameObject.hideFlags = HideFlags.HideInHierarchy;
            return gameObject.AddComponent<TInstance>();
        }
    }

    public abstract class AutoSingletonBehaviour<TInstance> : SingletonBehaviour<TInstance, AutoSingletonBehaviourInstanceFactory<TInstance>>
        where TInstance : AutoSingletonBehaviour<TInstance>
    {
    }

    #endregion

    #region Manual

    public class ManualSingletonBehaviourInstanceFactory<TInstance> : ISingletonFactory<TInstance>
        where TInstance : ManualSingletonBehaviour<TInstance>
    {
        public TInstance CreateInstance()
        {
            TInstance instance = Object.FindObjectOfType<TInstance>(true);

            if (instance == null)
            {
                Logger.LogWarning(LogCategory.Singleton, $"No gameobject with a MonoBehaviour of type <b>{typeof(TInstance)}</b> was found.");
            }

            return instance;
        }
    }

    public abstract class ManualSingletonBehaviour<TInstance> : SingletonBehaviour<TInstance, ManualSingletonBehaviourInstanceFactory<TInstance>>
        where TInstance : ManualSingletonBehaviour<TInstance>
    {
    }

    #endregion
}