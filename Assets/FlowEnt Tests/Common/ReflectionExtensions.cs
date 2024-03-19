using System.Reflection;

namespace FriedSynapse.FlowEnt.Tests
{
    public static class ReflectionExtensions
    {
        private const BindingFlags DefaultBindingFlags =
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        public static T GetFieldValue<T>(this object obj, string name, BindingFlags bindingFlags = DefaultBindingFlags)
            => (T)obj.GetType().GetField(name, bindingFlags)?.GetValue(obj);
        
        public static void SetFieldValue<T>(this object obj, string name, T value,
            BindingFlags bindingFlags = DefaultBindingFlags)
            => obj.GetType().GetField(name, bindingFlags)?.SetValue(obj, value);
    }
}
