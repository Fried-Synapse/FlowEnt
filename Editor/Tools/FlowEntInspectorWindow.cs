using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public class FlowEntInspectorWindow : EditorWindow
    {
        [MenuItem("FlowEnt/Inspector", false, 101)]
        private static void Init()
        {
            FlowEntInspectorWindow window = GetWindow<FlowEntInspectorWindow>("FlowEnt Inspector");
            window.titleContent = new GUIContent("FlowEnt Inspector", Resources.Load<Texture2D>("Logo"));
            window.Show();
        }

        private Vector2 motionListScrollPosition;
        private int flowCount;
        private int tweenCount;
        private float? timeScale;
        private float? maxTimeScale;
        private int skipFrames;
        private string search;
        private void Update()
        {
            Repaint();
            if (skipFrames > 0)
            {
                skipFrames--;
                if (skipFrames == 0)
                {
                    FlowEntController.Instance.Pause();
                }
            }
        }

        private void OnGUI()
        {
            FlowEntEditorGUILayout.Header("FlowEnt Inspector");

            if (!FlowEntController.HasInstance)
            {
                EditorGUILayout.HelpBox("Inspector only available in play mode when animations are playing.", MessageType.Info);
                search = null;
                return;
            }

            ShowControls();

            EditorGUILayout.Space(20f);

            ShowMotionList();
        }

        private void ShowControls()
        {
            EditorGUILayout.BeginHorizontal();

            if (FlowEntController.Instance.PlayState == PlayState.Playing)
            {
                if (GUILayout.Button("Pause"))
                {
                    FlowEntController.Instance.Pause();
                }
            }
            else
            {
                if (GUILayout.Button("Resume"))
                {
                    FlowEntController.Instance.Resume();
                }
            }

            GUI.enabled = FlowEntController.Instance.PlayState == PlayState.Paused;
            if (GUILayout.Button("Skip"))
            {
                FlowEntController.Instance.Resume();
                skipFrames = 2;
            }
            GUI.enabled = true;

            if (GUILayout.Button("Stop"))
            {
                FlowEntController.Instance.Stop();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (timeScale == null)
            {
                timeScale = FlowEntController.Instance.TimeScale;
            }
            if (maxTimeScale == null)
            {
                maxTimeScale = timeScale * 2f;
            }
            EditorGUILayout.LabelField("Time scale", GUILayout.Width(80f));
            timeScale = EditorGUILayout.Slider(timeScale.Value, 0f, maxTimeScale.Value);
            EditorGUILayout.LabelField("Max", GUILayout.Width(30f));
            maxTimeScale = EditorGUILayout.FloatField(maxTimeScale.Value, GUILayout.Width(50f));
            FlowEntController.Instance.TimeScale = timeScale.Value;
            EditorGUILayout.EndHorizontal();
        }

        private void ShowMotionList()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Search", GUILayout.Width(70f));
            search = EditorGUILayout.TextField(search);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            FlowEntEditorGUILayout.LabelFieldBold($"Flow count: {flowCount}", FlowEntConstants.Grey, GUILayout.Width(200f));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            FlowEntEditorGUILayout.LabelFieldBold($"Tween count: {tweenCount}", FlowEntConstants.Grey, GUILayout.Width(200f));
            EditorGUILayout.EndHorizontal();

            flowCount = 0;
            tweenCount = 0;

            motionListScrollPosition = EditorGUILayout.BeginScrollView(motionListScrollPosition, GUILayout.Height(position.height - 150f));
            ShowAnimationList(FlowEntController.Instance.GetUpdatableIndex());
            EditorGUILayout.EndScrollView();
        }

        private void ShowAnimationList(AbstractUpdatable index)
        {
            while (index != null)
            {
                switch (index)
                {
                    case Flow flow:
                        if (string.IsNullOrEmpty(search) || flow.Name?.ToLower().Contains(search.ToLower()) == true)
                        {
                            flowCount++;
                            EditorGUILayout.BeginHorizontal();
                            FlowEntEditorGUILayout.LabelField(flow, "Flow");
                            if (GUILayout.Button("ⓘ", GUILayout.Width(50)))
                            {
                                FlowInspectorWindow.Show(flow);
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUI.indentLevel++;
                        ShowAnimationList(flow.GetUpdatableIndex());
                        EditorGUI.indentLevel--;
                        break;
                    case Tween tween:
                        if (string.IsNullOrEmpty(search) || tween.Name?.ToLower().Contains(search.ToLower()) == true)
                        {
                            tweenCount++;
                            EditorGUILayout.BeginHorizontal();
                            FlowEntEditorGUILayout.LabelField(tween, "Tween");
                            if (GUILayout.Button("ⓘ", GUILayout.Width(50)))
                            {
                                TweenInspectorWindow.Show(tween);
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        break;
                }
                index = index.GetFieldValue<AbstractUpdatable>("next");
            }
        }
    }
}
