namespace FriedSynapse.Quickit
{
    public class SingletonInstanceFactory<TInstance> : ISingletonFactory<TInstance>
        where TInstance : new()
    {
        public TInstance CreateInstance() => new TInstance();
    }

    public abstract class Singleton<TInstance> : Singleton<TInstance, SingletonInstanceFactory<TInstance>>
        where TInstance : new()
    {
    }

    public abstract class Singleton<TInstance, TInstanceFactory>
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
}