using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public class MotionSelectionWindow : EditorWindow
    {
        public static void Open(Action<AbstractMotionBuilder> callback)
        {
            MotionSelectionWindow window = GetWindow<MotionSelectionWindow>("Select Motion");
            window.titleContent = new GUIContent("Select Motion", Resources.Load<Texture2D>("Logo"));
            window.Callback = callback;
            window.Show();
        }

        private Action<AbstractMotionBuilder> Callback { get; set; }

        private void OnGUI()
        {
            FlowEntEditorGUILayout.Header("Select Motion");
            EditorGUI.indentLevel++;

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(AbstractMotionBuilder))))
                {
                    var index = type.Name.LastIndexOf("Builder");
                    string name = index >= 0 ? type.Name.Substring(0, index) : type.Name;
                    if (GUILayout.Button(name))
                    {
                        Callback?.Invoke((AbstractMotionBuilder)Activator.CreateInstance(type));
                        Close();
                    }
                }
            }

            EditorGUI.indentLevel--;
        }
    }
}
