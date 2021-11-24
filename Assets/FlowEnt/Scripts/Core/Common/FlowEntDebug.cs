using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public static class FlowEntDebug
    {
        public static void Log(string format)
        {
            Debug.Log($"<b><color={FlowEntConstants.FlowEnt}>[FlowEnt]</color></b> {format}");
        }
        public static void LogWarning(string format)
        {
            Debug.LogWarning($"<b><color={FlowEntConstants.FlowEnt}>[FlowEnt]</color></b> {format}");
        }
        public static void LogError(string format)
        {
            Debug.LogError($"<b><color={FlowEntConstants.FlowEnt}>[FlowEnt]</color></b> {format}");
        }
    }
}
