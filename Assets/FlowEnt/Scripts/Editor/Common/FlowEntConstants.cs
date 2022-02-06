using UnityEditor;

namespace FriedSynapse.FlowEnt.Editor
{
    internal static class FlowEntConstants
    {
        internal const float DrawerSpacing = 2f;
        internal static float SingleLineHeight => EditorGUIUtility.singleLineHeight;
        internal static float SpacedSingleLineHeight => EditorGUIUtility.singleLineHeight + DrawerSpacing;

        internal const string FlowEnt = "#3065ba";
        internal const string Blue = "#4871F3";
        internal const string Green = "#48F352";
        internal const string Cyan = "#48ECF3";
        internal const string Orange = "#F3C648";
        internal const string Red = "#F06D35";
        internal const string Grey = "#DEDEDE";
    }
}
