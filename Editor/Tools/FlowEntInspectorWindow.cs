using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public class FlowEntInspectorWindow : EditorWindow
    {
        private const string DefaultColour = "#AAA";

        [MenuItem("FlowEnt/Inspector", false, 101)]
        private static void Init()
        {
            FlowEntInspectorWindow window = GetWindow<FlowEntInspectorWindow>("FlowEnt Inspector");
            window.Show();
        }

        private Vector2 motionListScrollPosition;
        private GUIStyle labelStyle;
        private GUIStyle foldoutStyle;
        private bool wasPaused;
        private Dictionary<ulong, bool> animationFoldouts = new Dictionary<ulong, bool>();
        private bool collapseFlowsByDefault;
        private bool collapseTweensByDefault = true;
        private int flowCount;
        private int tweenCount;

        private void Awake()
        {
            labelStyle = new GUIStyle(EditorStyles.label)
            {
                richText = true
            };
        }
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
                EditorGUILayout.HelpBox("Inspector only available in play mode", MessageType.Info);
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
        }

        private void ShowMotionList()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"<color={DefaultColour}><b>Flow count: {flowCount}</b></color>", labelStyle, GUILayout.Width(200));
            collapseFlowsByDefault = EditorGUILayout.Toggle("Collapse flows by default", collapseFlowsByDefault);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"<color={DefaultColour}><b>Tween count: {tweenCount}</b></color>", labelStyle, GUILayout.Width(200));
            collapseTweensByDefault = EditorGUILayout.Toggle("Collapse tweens by default", collapseTweensByDefault);
            EditorGUILayout.EndHorizontal();

            flowCount = 0;
            tweenCount = 0;

            motionListScrollPosition = EditorGUILayout.BeginScrollView(motionListScrollPosition);
            ShowAnimationList(FlowEntController.Instance.GetUpdatableIndex());
            EditorGUILayout.EndScrollView();
        }

        private void LabelField(IMotion motion)
        {
            EditorGUILayout.LabelField($"<color={DefaultColour}><b>{motion.GetType().Name}</b> - {motion.GetType().FullName}</color>", labelStyle);
        }

        private bool UpdatableFoldout(AbstractUpdatable updatable, string name, bool collapseByDefault)
        {
            if (!animationFoldouts.ContainsKey(updatable.Id))
            {
                animationFoldouts.Add(updatable.Id, !collapseByDefault);
            }

            bool shouldShow = animationFoldouts[updatable.Id];
            EditorGUILayout.BeginHorizontal();
            string updatableName = updatable.Name == null ? string.Empty : $" - {updatable.Name}";
            shouldShow = EditorGUILayout.Foldout(shouldShow, $"{name} [{updatable.Id}]{updatableName}");
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
                        if (UpdatableFoldout(tween, "Tween", collapseTweensByDefault))
                        {
                            EditorGUI.indentLevel++;
                            foreach (IMotion motion in tween.GetFieldValue<IMotion[]>("motions"))
                            {
                                LabelField(motion);
                            }
                            EditorGUI.indentLevel--;
                        }
                        break;
                }
                index = index.GetFieldValue<AbstractUpdatable>("next");
            }
        }
    }
}
