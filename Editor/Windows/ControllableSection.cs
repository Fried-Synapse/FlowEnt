using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public class ControllableSection<TControllable>
        where TControllable : IControllable
    {
        public ControllableSection(TControllable controllable)
        {
            this.controllable = controllable;
        }

        private readonly TControllable controllable;
        private float? timeScale;
        private float? maxTimeScale;
        private int skipFrames;

        internal void Update()
        {
            if (skipFrames > 0)
            {
                skipFrames--;
                if (skipFrames == 0)
                {
                    FlowEntController.Instance.Pause();
                }
            }
        }

        internal void ShowControls()
        {
            EditorGUILayout.BeginHorizontal();

            if (controllable.PlayState == PlayState.Playing)
            {
                if (GUILayout.Button("Pause"))
                {
                    controllable.Pause();
                }
            }
            else
            {
                if (GUILayout.Button("Resume"))
                {
                    controllable.Resume();
                }
            }

            GUI.enabled = controllable.PlayState == PlayState.Paused;
            if (GUILayout.Button("Skip"))
            {
                controllable.Resume();
                skipFrames = 2;
            }
            GUI.enabled = true;

            if (GUILayout.Button("Stop"))
            {
                controllable.Stop();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (timeScale == null)
            {
                timeScale = controllable.TimeScale;
            }
            if (maxTimeScale == null)
            {
                maxTimeScale = timeScale * 2f;
            }
            EditorGUILayout.LabelField("Time scale", GUILayout.Width(80f));
            timeScale = EditorGUILayout.Slider(timeScale.Value, 0f, maxTimeScale.Value);
            EditorGUILayout.LabelField("Max", GUILayout.Width(30f));
            maxTimeScale = EditorGUILayout.FloatField(maxTimeScale.Value, GUILayout.Width(50f));
            controllable.TimeScale = timeScale.Value;
            EditorGUILayout.EndHorizontal();
        }
    }
}
