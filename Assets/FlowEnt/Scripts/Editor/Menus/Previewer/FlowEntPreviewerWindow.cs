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
        private class AnimationInfo
        {
            public AnimationInfo(string name, AbstractAnimation animation)
            {
                this.name = name;
                this.animation = animation;
                if (animation.PlayState != PlayState.Building)
                {
                    animation.Stop();
                    animation.Reset();
                }
                controllableSection = new PreviewControllableSection(this.animation);
            }
            public string name;
            public AbstractAnimation animation;
            public PreviewControllableSection controllableSection;
        }

        internal static FlowEntPreviewerWindow Instance { get; private set; }
        private const BindingFlags DefaultBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        private static readonly Type abstractAnimationType = typeof(AbstractAnimation);
        private static readonly Type abstractAnimationBuilderType = typeof(IAbstractAnimationBuilder);
        private static readonly object[] emptyArray = { };
        private Transform transform;
        private List<AnimationInfo> animations;

#pragma warning disable IDE0051, RCS1213
        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            Repaint();
        }

        private void OnDestroy()
        {
            Instance = default;
        }

        private void OnGUI()
        {
            FlowEntEditorGUILayout.Header("FlowEnt Previewer");
            GUIStyle errorStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
            if (Selection.gameObjects.Length == 0)
            {
                EditorGUILayout.LabelField("Select an object from the hierarchy first.", errorStyle);
                return;
            }
            EditorGUILayout.Space(20f);
            bool shouldReadAnimations = animations == null;
            if (transform != Selection.activeTransform)
            {
                transform = Selection.activeTransform;
                shouldReadAnimations = true;
            }
            if (shouldReadAnimations)
            {
                ReadAnimations();
            }
            ShowAnimations();
        }
#pragma warning restore IDE0051, RCS1213

        internal void ResetAnimations()
        {
            if (Instance == null)
            {
                return;
            }
            Instance.animations = null;
        }

        private void ShowAnimations()
        {
            EditorGUILayout.LabelField(transform.name);
            EditorGUI.indentLevel++;
            if (animations?.Count == 0)
            {
                EditorGUILayout.LabelField("No available animations to preview.");
            }
            else
            {
                foreach (AnimationInfo animationInfo in animations)
                {
                    EditorGUILayout.LabelField(animationInfo.name);
                    animationInfo.controllableSection.Show();
                }
            }
            EditorGUI.indentLevel--;
        }

        private void ReadAnimations()
        {
            animations = new List<AnimationInfo>();
            foreach (MonoBehaviour behaviour in transform.GetComponents<MonoBehaviour>())
            {
                animations.AddRange(
                    behaviour
                    .GetType()
                    .GetFields(DefaultBindingFlags)
                    .Where(fi => abstractAnimationBuilderType.IsAssignableFrom(fi.FieldType))
                    .Select(fi => new AnimationInfo(fi.Name, ((IAbstractAnimationBuilder)fi.GetValue(behaviour)).Build()))
                    .ToList());

                animations.AddRange(
                    behaviour
                    .GetType()
                    .GetProperties(DefaultBindingFlags)
                    .Where(pi => abstractAnimationType.IsAssignableFrom(pi.PropertyType))
                    .Select(pi => new AnimationInfo(pi.Name, (AbstractAnimation)pi.GetValue(behaviour)))
                    .ToList());

                animations.AddRange(
                    behaviour
                    .GetType()
                    .GetMethods(DefaultBindingFlags)
                    .Where(mi
                        => !mi.IsSpecialName
                        && abstractAnimationType.IsAssignableFrom(mi.ReturnType)
                        && mi.GetParameters().Length == 0)
                    .Select(mi => new AnimationInfo(mi.Name, (AbstractAnimation)mi.Invoke(behaviour, emptyArray)))
                    .ToList());
            }
        }
    }
}