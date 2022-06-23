using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public static class Icon
    {
        public static GUIContent Menu = EditorGUIUtility.IconContent("_Menu@2x", "Menu");

        public static GUIStyle Style = new GUIStyle(EditorStyles.miniButton) { padding = new RectOffset(2, 2, 2, 2) };
    }
}
