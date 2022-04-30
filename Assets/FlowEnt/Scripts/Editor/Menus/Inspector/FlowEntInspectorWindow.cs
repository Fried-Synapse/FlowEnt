using FriedSynapse.FlowEnt.Reflection;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public class FlowEntInspectorWindow : EditorWindow
    {
        private ControllableSection controllableSection;
        private Vector2 motionListScrollPosition;
        private int tweenCount;
        private int echoCount;
        private int flowCount;
        private string search;

#pragma warning disable IDE0051, RCS1213
        private void Update()
        {
            Repaint();
        }

        private void OnGUI()
        {
            FlowEntEditorGUILayout.Header("FlowEnt Inspector");

#if FlowEnt_Builder
            object updater = FlowEntController.Instance.GetFieldValue<object>("updater", BindingFlags.NonPublic | BindingFlags.Static);
            EditorGUILayout.LabelField("Active Controller:", updater is FlowEntRuntimeUpdater ? "Runtime" : "Editor");
#endif

            if (controllableSection == null || controllableSection.Controllable != FlowEntController.Instance)
            {
                controllableSection = new ControllableSection(FlowEntController.Instance);
            }
            controllableSection.ShowControls();

            ShowMotionList();
        }
#pragma warning restore IDE0051, RCS1213

        private void ShowMotionList()
        {
            EditorGUILayout.Space(20f);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Search", GUILayout.Width(70f));
            search = EditorGUILayout.TextField(search);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            FlowEntEditorGUILayout.LabelFieldBold($"Tween count: {tweenCount}", FlowEntConstants.Grey, GUILayout.Width(200f));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            FlowEntEditorGUILayout.LabelFieldBold($"Echo count: {echoCount}", FlowEntConstants.Grey, GUILayout.Width(200f));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            FlowEntEditorGUILayout.LabelFieldBold($"Flow count: {flowCount}", FlowEntConstants.Grey, GUILayout.Width(200f));
            EditorGUILayout.EndHorizontal();

            tweenCount = 0;
            echoCount = 0;
            flowCount = 0;

            motionListScrollPosition = EditorGUILayout.BeginScrollView(motionListScrollPosition, GUILayout.Height(position.height - 150f));
            ShowAnimationList("updatables");
            ShowAnimationList("smoothUpdatables");
            ShowAnimationList("lateUpdatables");
            ShowAnimationList("smoothLateUpdatables");
            ShowAnimationList("fixedUpdatables");
            ShowAnimationList("customUpdatables");
            EditorGUILayout.EndScrollView();
        }

        private void ShowAnimationList(string group)
            => ShowAnimationList(FlowEntController.Instance.GetUpdatableIndex(group));

        private void ShowAnimationList(AbstractUpdatable index)
        {
            while (index != null)
            {
                switch (index)
                {
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
                    case Echo echo:
                        if (string.IsNullOrEmpty(search) || echo.Name?.ToLower().Contains(search.ToLower()) == true)
                        {
                            echoCount++;
                            EditorGUILayout.BeginHorizontal();
                            FlowEntEditorGUILayout.LabelField(echo, "Echo");
                            if (GUILayout.Button("ⓘ", GUILayout.Width(50)))
                            {
                                EchoInspectorWindow.Show(echo);
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        break;
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
                }
                index = index.GetFieldValue<AbstractUpdatable>("next");
            }
        }
    }
}
