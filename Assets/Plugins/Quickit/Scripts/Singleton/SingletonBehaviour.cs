using UnityEngine;

namespace FriedSynapse.Quickit
{
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
                    if (instance == null)
                    {
                        instance = new TInstanceFactory().CreateInstance();
                    }
                }
                return instance;
            }
        }
        public static bool HasInstance => instance != null;
    }

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
}