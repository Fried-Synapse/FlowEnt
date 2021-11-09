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
            window.Show();
        }

        private Vector2 motionListScrollPosition;
        private bool wasPaused;
        private Dictionary<ulong, bool> animationFoldouts = new Dictionary<ulong, bool>();
        private bool collapseFlowsByDefault;
        private int flowCount;
        private int tweenCount;
        private float? timeScale;
        private float? maxTimeScale;
        private void Update()
        {
            if (EditorApplication.isPaused != wasPaused)
            {
                ResetData();
            }
            wasPaused = EditorApplication.isPaused;
            Repaint();
        }

        private void ResetData()
        {
            animationFoldouts = new Dictionary<ulong, bool>();
        }

        private void OnGUI()
        {
            FlowEntEditorGUILayout.Header("FlowEnt Inspector");

            EditorGUI.indentLevel++;
            if (!FlowEntController.HasInstance)
            {
                EditorGUILayout.HelpBox("Inspector only available in play mode when animations are playing.", MessageType.Info);
                EditorGUI.indentLevel--;
                return;
            }

            ShowControls();

            ShowMotionList();

            EditorGUI.indentLevel--;
        }

        private void ShowControls()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Pause"))
            {
                FlowEntController.Instance.Pause();
            }
            if (GUILayout.Button("Resume"))
            {
                FlowEntController.Instance.Resume();
            }
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
            timeScale = EditorGUILayout.Slider("Time scale", timeScale.Value, 0f, maxTimeScale.Value);
            maxTimeScale = EditorGUILayout.FloatField(maxTimeScale.Value, GUILayout.Width(50));
            FlowEntController.Instance.TimeScale = timeScale.Value;
            EditorGUILayout.EndHorizontal();
        }

        private void ShowMotionList()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"<color={FlowEntConstants.Grey}><b>Flow count: {flowCount}</b></color>", FlowEntEditorGUILayout.LabelStyle, GUILayout.Width(200f));
            collapseFlowsByDefault = EditorGUILayout.Toggle("Collapse flows by default", collapseFlowsByDefault);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"<color={FlowEntConstants.Grey}><b>Tween count: {tweenCount}</b></color>", FlowEntEditorGUILayout.LabelStyle, GUILayout.Width(200f));
            EditorGUILayout.EndHorizontal();

            flowCount = 0;
            tweenCount = 0;

            motionListScrollPosition = EditorGUILayout.BeginScrollView(motionListScrollPosition, GUILayout.Height(position.height - 150f));
            ShowAnimationList(FlowEntController.Instance.GetUpdatableIndex());
            EditorGUILayout.EndScrollView();
        }

        private bool UpdatableFoldout(AbstractUpdatable updatable, string name, bool collapseByDefault)
        {
            if (!animationFoldouts.ContainsKey(updatable.Id))
            {
                animationFoldouts.Add(updatable.Id, !collapseByDefault);
            }

            bool shouldShow = animationFoldouts[updatable.Id];
            EditorGUILayout.BeginHorizontal();
            shouldShow = EditorGUILayout.Foldout(shouldShow, $"{name} {updatable}");
            EditorGUILayout.EndHorizontal();
            animationFoldouts[updatable.Id] = shouldShow;

            return shouldShow;
        }

        private void ShowAnimationList(AbstractUpdatable index)
        {
            while (index != null)
            {
                switch (index)
                {
                    case Flow flow:
                        flowCount++;
                        if (UpdatableFoldout(flow, "Flow", collapseFlowsByDefault))
                        {
                            EditorGUI.indentLevel++;
                            ShowAnimationList(flow.GetUpdatableIndex());
                            EditorGUI.indentLevel--;
                        }
                        break;
                    case Tween tween:
                        tweenCount++;
                        EditorGUILayout.BeginHorizontal();
                        FlowEntEditorGUILayout.LabelField(tween, "Tween");
                        if (GUILayout.Button("ⓘ", GUILayout.Width(50)))
                        {
                            TweenInspectorWindow.Show(tween);
                        }
                        EditorGUILayout.EndHorizontal();
                        break;
                }
                index = index.GetFieldValue<AbstractUpdatable>("next");
            }
        }
    }
}
