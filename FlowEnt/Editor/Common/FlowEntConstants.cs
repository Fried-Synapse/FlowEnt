using UnityEditor;

namespace FriedSynapse.FlowEnt.Editor
{
    internal static class FlowEntConstants
    {
        internal const float DrawerSpacing = 2f;
        internal const float PaddingFix = 12f;
        internal static float SpacedSingleLineHeight => EditorGUIUtility.singleLineHeight + DrawerSpacing;

        internal const string Orange = "#F3C648";
        internal const string Red = "#F06D35";
    }
}
