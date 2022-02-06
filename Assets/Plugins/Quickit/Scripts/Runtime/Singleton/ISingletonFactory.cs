namespace FriedSynapse.Quickit
{
    public interface ISingletonFactory<TInstance>
    {
        public TInstance CreateInstance();
    }
}
