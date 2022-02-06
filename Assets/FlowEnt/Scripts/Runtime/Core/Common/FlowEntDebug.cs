using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    internal static class FlowEntDebug
    {
        internal static void Log(object message)
        {
            Debug.Log($"<b><color={FlowEntConstants.FlowEnt}>[FlowEnt]</color></b> {message}");
        }
        internal static void LogWarning(object message)
        {
            Debug.LogWarning($"<b><color={FlowEntConstants.FlowEnt}>[FlowEnt]</color></b> {message}");
        }
        internal static void LogError(object message)
        {
            Debug.LogError($"<b><color={FlowEntConstants.FlowEnt}>[FlowEnt]</color></b> {message}");
        }
    }
}
