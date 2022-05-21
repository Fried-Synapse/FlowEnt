using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    public static class Icon
    {
        public static GUIContent Menu = EditorGUIUtility.IconContent("_Menu@2x", "Menu");
        [Obsolete]
        public static GUIContent Play = EditorGUIUtility.IconContent("Animation.Play", "Play");
        [Obsolete]
        public static GUIContent Replay = EditorGUIUtility.IconContent("d_preAudioAutoPlayOff", "Replay");
        [Obsolete]
        public static GUIContent Pause = EditorGUIUtility.IconContent("PauseButton On@2x", "Pause");
        [Obsolete]
        public static GUIContent PrevFrame = EditorGUIUtility.IconContent("Animation.PrevKey", "Previous Frame");
        [Obsolete]
        public static GUIContent NextFrame = EditorGUIUtility.IconContent("Animation.NextKey", "Next Frame");
        [Obsolete]
        public static GUIContent Stop = EditorGUIUtility.IconContent("d_PreMatQuad@2x", "Stop");

        public static GUIStyle Style = new GUIStyle(EditorStyles.miniButton) { padding = new RectOffset(2, 2, 2, 2) };
    }
}
