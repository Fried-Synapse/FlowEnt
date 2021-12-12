using UnityEngine;

namespace FriedSynapse.Quickit
{
    internal enum LogCategory
    {
        Singleton,
        ObjectPool
    }

    internal static class Logger
    {
        private static string GetMessage(LogCategory category, object message)
            => $"<b>{category}</b> {message}";

        internal static void Log(LogCategory category, object message)
            => Debug.Log(GetMessage(category, message));

        internal static void LogWarning(LogCategory category, object message)
            => Debug.LogWarning(GetMessage(category, message));

        internal static void LogError(LogCategory category, object message)
            => Debug.LogError(GetMessage(category, message));

    }
}
