#if UNITY_EDITOR
using System.Reflection;

namespace FriedSynapse.FlowEnt.Reflection
{
    public static class ReflectionExtensions
    {
        public const BindingFlags DefaultBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        public static T GetFieldValue<T>(this object obj, string name)
            => (T)obj.GetType().GetField(name, DefaultBindingFlags)?.GetValue(obj);

        public static void SetFieldValue<T>(this object obj, string name, T value)
            => obj.GetType().GetField(name, DefaultBindingFlags)?.SetValue(obj, value);

        public static T GetPropertyValue<T>(this object obj, string name)
            => (T)obj.GetType().GetProperty(name, DefaultBindingFlags)?.GetValue(obj);

        public static void InvokeMethod(this object obj, string name)
            => obj.GetType().GetMethod(name, DefaultBindingFlags)?.Invoke(obj, new object[0]);
    }
}
#endif