using System;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public class MotionPickerWindow : EditorWindow
    {
        private static MotionPickerWindow instance;
        public static void Show(Action<AbstractTweenMotionBuilder> callback)
        {
            instance?.Close();
            instance = CreateWindow<MotionPickerWindow>("Select animation");
            instance.callback = callback;
            instance.Show();
        }

        private Action<AbstractTweenMotionBuilder> callback;
        private string searchText;
        private void OnGUI()
        {
            searchText = EditorGUILayout.TextField(searchText);

            if (GUILayout.Button("Select"))
            {
            }
        }
    }
}
