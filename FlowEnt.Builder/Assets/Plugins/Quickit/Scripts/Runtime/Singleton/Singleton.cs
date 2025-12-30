namespace FriedSynapse.Quickit
{
    #region With Factory

    public abstract class Singleton<TInstance, TInstanceFactory>
        where TInstance : Singleton<TInstance, TInstanceFactory>
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
    }

    #endregion

    #region Basic

    public class SingletonInstanceFactory<TInstance> : ISingletonFactory<TInstance>
        where TInstance : new()
    {
        public TInstance CreateInstance() => new TInstance();
    }

    public abstract class Singleton<TInstance> : Singleton<TInstance, SingletonInstanceFactory<TInstance>>
        where TInstance : Singleton<TInstance>, new()
    {
    }

    #endregion
}