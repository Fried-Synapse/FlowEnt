using System.Reflection;

namespace FriedSynapse.FlowEnt.Reflection
{
    public static class ReflectionExtensions
    {
        public const BindingFlags DefaultBindingFlags =
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        public static T GetFieldValue<T>(this object obj, string name, BindingFlags bindingFlags = DefaultBindingFlags)
            => (T)obj.GetType().GetField(name, bindingFlags)?.GetValue(obj);

        public static void SetFieldValue<T>(this object obj, string name, T value,
            BindingFlags bindingFlags = DefaultBindingFlags)
            => obj.GetType().GetField(name, bindingFlags)?.SetValue(obj, value);

        public static T GetPropertyValue<T>(this object obj, string name,
            BindingFlags bindingFlags = DefaultBindingFlags)
            => (T)obj.GetType().GetProperty(name, bindingFlags)?.GetValue(obj);

        public static TResult InvokeMethod<TClass, TResult>(this object obj, string name, object[] data = null,
            BindingFlags bindingFlags = DefaultBindingFlags)
            => (TResult)typeof(TClass).GetMethod(name, bindingFlags)?.Invoke(obj, data ?? new object[0]);
    }
}