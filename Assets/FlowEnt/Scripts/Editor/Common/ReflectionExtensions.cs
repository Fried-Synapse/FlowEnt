#if UNITY_EDITOR
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace FriedSynapse.FlowEnt.Reflection
{
    public static class ReflectionExtensions
    {
        public const BindingFlags DefaultBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        public static T GetFieldValue<T>(this object obj, string name, BindingFlags bindingFlags = DefaultBindingFlags)
            => (T)obj.GetType().GetField(name, bindingFlags)?.GetValue(obj);

        public static void SetFieldValue<T>(this object obj, string name, T value, BindingFlags bindingFlags = DefaultBindingFlags)
            => obj.GetType().GetField(name, bindingFlags)?.SetValue(obj, value);

        public static T GetPropertyValue<T>(this object obj, string name, BindingFlags bindingFlags = DefaultBindingFlags)
            => (T)obj.GetType().GetProperty(name, bindingFlags)?.GetValue(obj);

        public static void InvokeMethod(this object obj, string name, object[] data = null, BindingFlags bindingFlags = DefaultBindingFlags)
            => obj.GetType().GetMethod(name, bindingFlags)?.Invoke(obj, data ?? new object[0]);

        internal static string PrettyfyMemberName(this string name)
        {
            string[] words = Regex.Matches(name, "([A-Z]+(?![a-z])|[A-Z][a-z]+|[0-9]+|[a-z]+)")
                .OfType<Match>()
                .Select(m => m.Value)
                .ToArray();
            words[0] = $"{char.ToUpper(words[0][0])}{words[0].Substring(1)}";
            return string.Join(" ", words);
        }
    }
}
#endif