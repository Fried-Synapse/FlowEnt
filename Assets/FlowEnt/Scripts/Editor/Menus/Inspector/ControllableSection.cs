using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public class ControllableSection
    {
        public ControllableSection(IControllable controllable)
        {
            this.Controllable = controllable;
        }

        public IControllable Controllable { get; }
        private float? timeScale;
        private float? maxTimeScale;

        internal void ShowControls()
        {
            EditorGUILayout.BeginHorizontal();

            if (Controllable.PlayState == PlayState.Playing)
            {
                if (GUILayout.Button(Icon.Pause, Icon.Style))
                {
                    Controllable.Pause();
                }
            }
            else
            {
                if (GUILayout.Button(Icon.Play, Icon.Style))
                {
                    Controllable.Resume();
                }
            }

            GUI.enabled = Controllable.PlayState == PlayState.Paused;
            if (GUILayout.Button(Icon.NextFrame, Icon.Style))
            {
                Controllable.NextFrame();
            }
            GUI.enabled = true;

            if (GUILayout.Button(Icon.Stop, Icon.Style))
            {
                Controllable.Stop();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (timeScale == null)
            {
                timeScale = Controllable.TimeScale;
            }
            if (maxTimeScale == null)
            {
                maxTimeScale = timeScale * 2f;
            }
            EditorGUILayout.LabelField("Time scale", GUILayout.Width(80f));
            timeScale = EditorGUILayout.Slider(timeScale.Value, 0f, maxTimeScale.Value);
            EditorGUILayout.LabelField("Max", GUILayout.Width(30f));
            maxTimeScale = EditorGUILayout.FloatField(maxTimeScale.Value, GUILayout.Width(50f));
            Controllable.TimeScale = timeScale.Value;
            EditorGUILayout.EndHorizontal();
        }
    }
}
