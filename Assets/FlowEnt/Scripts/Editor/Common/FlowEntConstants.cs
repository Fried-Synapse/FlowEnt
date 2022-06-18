using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    internal static class FlowEntConstants
    {
        internal const float DrawerSpacing = 2f;
        internal static float SpacedSingleLineHeight => EditorGUIUtility.singleLineHeight + DrawerSpacing;

        internal const string FlowEnt = "#3065ba";
        internal const string Blue = "#4871F3";
        internal const string Green = "#48F352";
        internal const string Cyan = "#48ECF3";
        internal const string Orange = "#F3C648";
        internal const string Red = "#F06D35";
        internal const string Grey = "#DEDEDE";
        internal const string Preview = "#FF000030";

        private static GUIStyle foldoutHeader;
        public static GUIStyle FoldoutHeader
        {
            get
            {
                if (foldoutHeader == null)
                {
                    foldoutHeader = new GUIStyle(EditorStyles.foldoutHeader);
                    Texture2D texture = new Texture2D(1, 1);
                    texture.SetPixel(0, 0, Color.clear);
                    texture.Apply();
                    foldoutHeader.normal.background = texture;
                }
                return foldoutHeader;
            }
        }
    }
}
