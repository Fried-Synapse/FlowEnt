using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public class FlowEntPreviewerWindow : EditorWindow
    {
        private class Foldouts
        {
            public bool animations;
        }
        private class AnimationInfo
        {
            public AbstractAnimation animation;
            public string name;
        }
        private const BindingFlags DefaultBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        private static readonly Type abstractAnimationType = typeof(AbstractAnimation);
        private static readonly object[] emptyArray = { };
        private readonly Dictionary<object, bool> scriptedPreviewFoldouts = new Dictionary<object, bool>();
        private readonly Dictionary<AbstractAnimation, ControllableSection> controlableSections = new Dictionary<AbstractAnimation, ControllableSection>();

#pragma warning disable IDE0051, RCS1213
        private void Update()
        {
            Repaint();
        }

        private void OnGUI()
        {
            FlowEntEditorGUILayout.Header("FlowEnt Previewer");

            if (Selection.gameObjects.Length == 0)
            {
                GUIStyle style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
                EditorGUILayout.LabelField("Select an object from the hierarchy first.", style);
                return;
            }
            EditorGUILayout.Space(20f);
            foreach (Transform transform in Selection.transforms)
            {
                ShowFor(transform);
            }
        }
#pragma warning restore IDE0051, RCS1213

        private void ShowFor(Transform transform)
        {
            if (!scriptedPreviewFoldouts.ContainsKey(transform))
            {
                scriptedPreviewFoldouts.Add(transform, false);
            }

            List<AnimationInfo> animations = GetAnimations(transform);

            if (animations.Count == 0)
            {
                EditorGUILayout.LabelField(transform.name);
            }
            else
            {
                scriptedPreviewFoldouts[transform] = EditorGUILayout.Foldout(scriptedPreviewFoldouts[transform], transform.name);
                if (scriptedPreviewFoldouts[transform])
                {
                    EditorGUI.indentLevel++;
                    foreach (AnimationInfo animationInfo in animations)
                    {
                        EditorGUILayout.LabelField(animationInfo.name);
                        if (!controlableSections.TryGetValue(animationInfo.animation, out ControllableSection controllableSection))
                        {
                            controllableSection = new ControllableSection(animationInfo.animation);
                        }
                        controllableSection.ShowControls();
                    }
                    EditorGUI.indentLevel--;
                }
            }
        }

        private List<AnimationInfo> GetAnimations(Transform transform)
        {
            List<AnimationInfo> animations = new List<AnimationInfo>();
            foreach (MonoBehaviour behaviour in transform.GetComponents<MonoBehaviour>())
            {
                animations.AddRange(
                    behaviour
                    .GetType()
                    .GetProperties(DefaultBindingFlags)
                    .Where(pi => abstractAnimationType.IsAssignableFrom(pi.PropertyType))
                    .Select(pi => new AnimationInfo() { animation = (AbstractAnimation)pi.GetValue(behaviour), name = pi.Name })
                    .ToList());

                animations.AddRange(
                    behaviour
                    .GetType()
                    .GetMethods(DefaultBindingFlags)
                    .Where(mi => abstractAnimationType.IsAssignableFrom(mi.ReturnType))
                    .Select(mi => new AnimationInfo() { animation = (AbstractAnimation)mi.Invoke(behaviour, emptyArray), name = mi.Name })
                    .ToList());
            }
            return animations;
        }
    }
}
