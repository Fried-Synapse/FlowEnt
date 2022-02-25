#if UNITY_EDITOR
using System.Reflection;

namespace FriedSynapse.FlowEnt.Reflection
{
    public static class ReflectionExtensions
    {
        public const BindingFlags defaultBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        public static T GetFieldValue<T>(this object obj, string name)
            => (T)obj.GetType().GetField(name, defaultBindingFlags)?.GetValue(obj);

        public static void SetFieldValue<T>(this object obj, string name, T value)
            => obj.GetType().GetField(name, defaultBindingFlags)?.SetValue(obj, value);
    }
}
#endif