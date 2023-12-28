using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public static class FlowEntDebug
    {
        public static void Log(object message)
        {
            Debug.Log($"<b><color={FlowEntInternalConstants.FlowEnt}>[FlowEnt]</color></b> {message}");
        }
        public static void LogWarning(object message)
        {
            Debug.LogWarning($"<b><color={FlowEntInternalConstants.FlowEnt}>[FlowEnt]</color></b> {message}");
        }
        public static void LogError(object message)
        {
            Debug.LogError($"<b><color={FlowEntInternalConstants.FlowEnt}>[FlowEnt]</color></b> {message}");
        }
    }
}
