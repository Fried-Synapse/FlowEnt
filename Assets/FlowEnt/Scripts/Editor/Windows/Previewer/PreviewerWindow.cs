using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class PreviewerWindow : FlowEntWindow<PreviewerWindow>
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

        protected override string Name => "FlowEnt Previewer";
        private const BindingFlags DefaultBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        private static readonly Type abstractAnimationType = typeof(AbstractAnimation);
        private static readonly Type abstractAnimationBuilderType = typeof(IAbstractAnimationBuilder);
        private static readonly object[] emptyArray = { };

        private TextElement label;
        private ScrollView animationsElement;

#pragma warning disable IDE0051, RCS1213

        protected override void CreateGUI()
        {
            LoadHeader();
            LoadContent();
            label = Content.Query<TextElement>("name").First();
            animationsElement = Content.Query<ScrollView>("animations").First();
            Selection.selectionChanged += RenderAnimations;
            EditorApplication.playModeStateChanged += _ => RenderAnimations();
            RenderAnimations();
        }

        internal void RenderAnimations()
        {
            if (PreviewController.IsRunning)
            {
                PreviewController.Stop();
            }

            Transform transform = Selection.activeTransform;
            label.text = transform == null ? "Select an object from the hierarchy first." : transform.name;

            animationsElement.Clear();
            if (transform == null)
            {
                return;
            }

            foreach (AnimationInfo animationInfo in GetAnimations(transform))
            {
                VisualElement animationElement = new VisualElement();
                animationElement.AddToClassList("animation");
                animationElement.AddToClassList(animationInfo.type.ToClassName());
                TextElement label = new TextElement
                {
                    text = $"{animationInfo.name} [{animationInfo.animation.GetType().Name}]",
                };
                label.AddToClassList("label");
                animationElement.Add(label);
                animationElement.Add(new PreviewableControlSection(animationInfo.animation));
                animationsElement.contentContainer.Add(animationElement);
            }
        }

#pragma warning restore IDE0051, RCS1213

        private List<AnimationInfo> GetAnimations(Transform transform)
        {
            List<AnimationInfo> animations = new List<AnimationInfo>();
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
            return animations;
        }
    }
}