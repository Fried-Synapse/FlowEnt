using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public class ControllableSection
    {
        public ControllableSection(IControllable controllable)
        {
            Controllable = controllable;
        }

        private static GUILayoutOption ButtonWidth { get; } = GUILayout.Width(EditorGUIUtility.singleLineHeight);
        public IControllable Controllable { get; }
        private float? timeScale;
        private float? maxTimeScale;

        internal void ShowControls()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(EditorGUI.indentLevel * EditorGUIUtility.singleLineHeight + 10);
            {//TODO maybe disable the buttons if they can't be used?
                if (Controllable.PlayState == PlayState.Playing)
                {
                    if (GUILayout.Button(Icon.Pause, Icon.Style, ButtonWidth))
                    {
                        Controllable.Pause();
                    }
                }
                else
                {
                    if (GUILayout.Button(Icon.Play, Icon.Style, ButtonWidth))
                    {
                        Controllable.Resume();
                    }
                }

                if (GUILayout.Button(Icon.NextFrame, Icon.Style, ButtonWidth))
                {
                    Controllable.NextFrame();
                }

                if (GUILayout.Button(Icon.Stop, Icon.Style, ButtonWidth))
                {
                    Controllable.Stop();
                }

                if (timeScale == null)
                {
                    timeScale = Controllable.TimeScale;
                }
                if (maxTimeScale == null)
                {
                    maxTimeScale = timeScale * 2f;
                }
                int indentLevel = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;
                EditorGUILayout.LabelField("Time scale", GUILayout.ExpandWidth(false), GUILayout.Width(70f));
                timeScale = EditorGUILayout.Slider(timeScale.Value, 0f, maxTimeScale.Value);
                EditorGUILayout.LabelField("Max", GUILayout.ExpandWidth(false), GUILayout.Width(30f));
                maxTimeScale = EditorGUILayout.FloatField(maxTimeScale.Value, GUILayout.ExpandWidth(false), GUILayout.Width(30f));
                Controllable.TimeScale = timeScale.Value;
                EditorGUILayout.Space(5);
                EditorGUI.indentLevel = indentLevel;
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
