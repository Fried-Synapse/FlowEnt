using System.Reflection;

namespace FriedSynapse.FlowEnt.Editor
{
    internal static class ReflectionExtensions
    {
        internal const BindingFlags DefaultBindingFlags =
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        internal static T GetFieldValue<T>(this object obj, string name, BindingFlags bindingFlags = DefaultBindingFlags)
            => (T)obj.GetType().GetField(name, bindingFlags)?.GetValue(obj);

        internal static T GetPropertyValue<T>(this object obj, string name,
            BindingFlags bindingFlags = DefaultBindingFlags)
            => (T)obj.GetType().GetProperty(name, bindingFlags)?.GetValue(obj);
    }
}