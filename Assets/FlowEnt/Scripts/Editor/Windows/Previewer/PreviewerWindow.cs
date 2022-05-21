using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class PreviewerWindow : EditorWindow
    {
        private enum MemberType
        {
            Field,
            Property,
            Method
        }
        private class AnimationInfo
        {
            public AnimationInfo(string name, MemberType type, AbstractAnimation animation)
            {
                this.name = name;
                this.type = type;
                this.animation = animation;
                if (animation.PlayState != PlayState.Building)
                {
                    animation.Stop();
                    animation.Reset();
                }
            }
            internal string name;
            internal MemberType type;
            internal AbstractAnimation animation;
        }

        private const BindingFlags DefaultBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        private static readonly Type abstractAnimationType = typeof(AbstractAnimation);
        private static readonly Type abstractAnimationBuilderType = typeof(IAbstractAnimationBuilder);
        private static readonly object[] emptyArray = { };
        internal static PreviewerWindow Instance { get; private set; }
        private Transform transform;
        private List<AnimationInfo> animations;

        private TextElement label;
        private VisualElement animationsElement;

#pragma warning disable IDE0051, RCS1213
        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (animations != null && transform == Selection.activeTransform)
            {
                return;
            }

            if (Selection.activeTransform == null)
            {
                transform = null;
                animations = null;
            }
            else
            {
                transform = Selection.activeTransform;
                ReadAnimations();
            }
            Render();
        }

        private void OnDestroy()
        {
            Instance = default;
        }

        internal void CreateGUI()
        {
            this.LoadUxml();
            label = rootVisualElement.Query<TextElement>("name").First();
            animationsElement = rootVisualElement.Query<VisualElement>("animations").First();
            Render();
        }

        private void Render()
        {
            label.text = transform == null ? "Select an object from the hierarchy first." : transform.name;

            animationsElement.Clear();

            if (animations == null)
            {
                return;
            }

            foreach (AnimationInfo animationInfo in animations)
            {
                animationsElement.Add(CreateLabel(animationInfo));
                animationsElement.Add(new PreviewableControlSection(animationInfo.animation));
            }
        }

        private TextElement CreateLabel(AnimationInfo animationInfo)
        {
            TextElement label = new TextElement();
            label.text = $"{animationInfo.name} [{animationInfo.animation.GetType().Name}]";
            Color? color = animationInfo.type switch
            {
                MemberType.Property => new Color(0.45f, 0.81f, 1f),
                MemberType.Method => new Color(0.86f, 0.86f, 0.34f),
                _ => null,
            };
            if (color != null)
            {
                label.style.color = new StyleColor(color.Value);
            }
            return label;
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
                    .Select(fi => new AnimationInfo(
                        char.ToUpper(fi.Name[0]) + fi.Name.Substring(1),
                        MemberType.Field,
                        ((IAbstractAnimationBuilder)fi.GetValue(behaviour)).Build()))
                    .ToList());

                animations.AddRange(
                    behaviour
                    .GetType()
                    .GetProperties(DefaultBindingFlags)
                    .Where(pi => abstractAnimationType.IsAssignableFrom(pi.PropertyType))
                    .Select(pi => new AnimationInfo(pi.Name,
                        MemberType.Property,
                        (AbstractAnimation)pi.GetValue(behaviour)))
                    .ToList());

                animations.AddRange(
                    behaviour
                    .GetType()
                    .GetMethods(DefaultBindingFlags)
                    .Where(mi
                        => !mi.IsSpecialName
                        && abstractAnimationType.IsAssignableFrom(mi.ReturnType)
                        && mi.GetParameters().Length == 0)
                    .Select(mi => new AnimationInfo(mi.Name,
                        MemberType.Method,
                        (AbstractAnimation)mi.Invoke(behaviour, emptyArray)))
                    .ToList());
            }
        }
    }
}