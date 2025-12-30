using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public static class Icon
    {
        public static readonly GUIContent Menu = EditorGUIUtility.IconContent("_Menu@2x", "Menu");
        public static readonly GUIContent Select = EditorGUIUtility.IconContent("d_icon dropdown@2x", "Select");
        public static readonly GUIContent Info = EditorGUIUtility.IconContent("console.infoicon@2x", "Info");
        public static readonly GUIContent Warning = EditorGUIUtility.IconContent("console.warnicon@2x", "Warning");
        public static readonly GUIContent Locate = EditorGUIUtility.IconContent("d_Animation.FilterBySelection", "Locate");

        public static readonly GUIStyle Style = new GUIStyle(EditorStyles.miniButton) { padding = new RectOffset(2, 2, 2, 2) };
    }
}