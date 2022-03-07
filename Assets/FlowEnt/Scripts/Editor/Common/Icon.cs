using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{

    public static class Icon
    {
        public static GUIContent Menu = EditorGUIUtility.IconContent("_Menu@2x", "Menu");
        public static GUIContent Play = EditorGUIUtility.IconContent("PlayButton@2x", "Play");
        public static GUIContent Pause = EditorGUIUtility.IconContent("PauseButton@2x", "Pause");
        public static GUIContent Stop = EditorGUIUtility.IconContent("PreMatQuad@2x", "Stop");

        public static GUIStyle Style = new GUIStyle(EditorStyles.miniButton) { padding = new RectOffset(2, 2, 2, 2) };
    }
}
