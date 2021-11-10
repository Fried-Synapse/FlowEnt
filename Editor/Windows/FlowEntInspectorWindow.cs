using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public class FlowEntInspectorWindow : EditorWindow
    {
        private ControllableSection<FlowEntController> controllableSection;
        private Vector2 motionListScrollPosition;
        private int flowCount;
        private int tweenCount;
        private string search;

        private void Update()
        {
            Repaint();
            controllableSection?.Update();
        }

        private void OnGUI()
        {
            FlowEntEditorGUILayout.Header("FlowEnt Inspector");

            if (!FlowEntController.HasInstance)
            {
                EditorGUILayout.HelpBox("Inspector only available in play mode when animations are playing.", MessageType.Info);
                search = null;
                controllableSection = null;
                return;
            }

            controllableSection ??= new ControllableSection<FlowEntController>(FlowEntController.Instance);
            controllableSection.ShowControls();

            EditorGUILayout.Space(20f);

            ShowMotionList();
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
