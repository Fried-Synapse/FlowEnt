using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal partial class PreviewerWindow : AbstractThemedWindow<PreviewerWindow>
    {
        private const string SelectMessage = "Please Select an object from the hierarchy first.";
        private const string PlaymodeMessage = "Previewer not available during play mode.";

        private enum MemberType
        {
            Field,
            Property,
            Method
        }

        protected override string Name => "FlowEnt Previewer";

        private const BindingFlags DefaultBindingFlags =
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

        private static readonly Type abstractAnimationType = typeof(AbstractAnimation);
        private static readonly object[] emptyArray = { };

        private TextElement label;
        private Button exitFocusButton;
        private ScrollView animationsElement;

        private IAbstractAnimationBuilder FocusedAnimationBuilder { get; set; }

        protected override void CreateGUI()
        {
            LoadHeader();
            LoadContent();
            label = Content.Query<TextElement>("name").First();
            exitFocusButton = Content.Query<Button>("exitFocus").First();
            animationsElement = Content.Query<ScrollView>("animations").First();
            Bind();
            Selection.selectionChanged += () => RefreshAnimations();
            EditorApplication.playModeStateChanged += _ => RefreshAnimations();
            EditorApplication.delayCall += () => RefreshAnimations();
        }

        private void Bind()
        {
            exitFocusButton.clicked += ExitFocus;
        }

        internal void RefreshAnimations()
        {
            if (Application.isPlaying)
            {
                label.text = PlaymodeMessage;
                return;
            }

            InitRender();

            if (FocusedAnimationBuilder == null)
            {
                RenderSelectedAnimations();
                return;
            }

            RenderFocusedAnimation();
        }

        internal void FocusAnimation(IAbstractAnimationBuilder animationBuilder)
        {
            exitFocusButton.visible = true;
            FocusedAnimationBuilder = animationBuilder;
            RefreshAnimations();
        }

        private void ExitFocus()
        {
            exitFocusButton.visible = false;
            FocusedAnimationBuilder = null;
            RefreshAnimations();
        }

        private void RenderFocusedAnimation()
        {
            AnimationInfo animationInfo = new("", MemberType.Field, FocusedAnimationBuilder);
            label.text = $"[Focused] {animationInfo.Animation?.Name}";

            RenderAnimationsInternal(new List<AnimationInfo> { animationInfo });
        }

        private void RenderSelectedAnimations()
        {
            Transform transform = Selection.activeTransform;
            label.text = transform == null ? SelectMessage : transform.name;

            if (transform == null)
            {
                return;
            }

            RenderAnimationsInternal(GetAnimations(transform));
        }

        private void InitRender()
        {
            if (PreviewController.IsRunning)
            {
                PreviewController.Stop();
            }

            animationsElement.Clear();
        }

        private void RenderAnimationsInternal(List<AnimationInfo> animationsInfo)
        {
            foreach (AnimationInfo animationInfo in animationsInfo)
            {
                VisualElement animationElement = new();
                animationElement.AddToClassList("animation");
                animationElement.AddToClassList(animationInfo.Type.ToClassName());
                TextElement label = new();
                label.AddToClassList("label");
                animationElement.Add(label);
                if (animationInfo.Exception != null)
                {
                    label.text = "Invalid animation";
                    animationElement.Add(new TextElement { text = "Looks like one of the motions is in an invalid state. Please check them." });
                    animationElement.Add(new HelpBox(animationInfo.Exception?.Message, HelpBoxMessageType.Error));
                    animationElement.Add(new Button(() => { Debug.LogException(animationInfo.Exception); })
                    {
                        text = "Log exception to console"
                    });
                }
                else
                {
                    label.text = $"{animationInfo.Name} [{animationInfo.Animation}]";
                    PreviewableControlSection controlSection = new();
                    controlSection.Init(animationInfo.Animation);
                    animationElement.Add(controlSection);
                }

                animationsElement.contentContainer.Add(animationElement);
            }
        }

        private List<AnimationInfo> GetAnimations(Transform transform)
        {
            Type abstractAnimationBuilderType = typeof(IAbstractAnimationBuilder);
            List<AnimationInfo> animations = new();
            foreach (MonoBehaviour behaviour in transform.GetComponents<MonoBehaviour>())
            {
                animations.AddRange(
                    behaviour
                        .GetType()
                        .GetFields(DefaultBindingFlags)
                        .Where(fi => abstractAnimationBuilderType.IsAssignableFrom(fi.FieldType))
                        .Select(fi => (
                            FieldInfo: fi,
                            AnimationBuilder: (IAbstractAnimationBuilder)fi.GetValue(behaviour)))
                        .Where(t => t.AnimationBuilder != null)
                        .Select(t => new AnimationInfo(t.FieldInfo.Name, MemberType.Field, t.AnimationBuilder))
                        .ToList());

                animations.AddRange(
                    behaviour
                        .GetType()
                        .GetProperties(DefaultBindingFlags)
                        .Where(pi => abstractAnimationType.IsAssignableFrom(pi.PropertyType))
                        .Select(pi => (
                            PropertyInfo: pi,
                            Animation: (AbstractAnimation)pi.GetValue(behaviour)))
                        .Where(t => t.Animation != null)
                        .Select(t => new AnimationInfo(t.PropertyInfo.Name, MemberType.Property, t.Animation))
                        .ToList());

                animations.AddRange(
                    behaviour
                        .GetType()
                        .GetMethods(DefaultBindingFlags)
                        .Where(mi
                            => !mi.IsSpecialName
                               && abstractAnimationType.IsAssignableFrom(mi.ReturnType)
                               && mi.GetParameters().Length == 0)
                        .Select(mi => (
                            FieldInfo: mi,
                            Animation: (AbstractAnimation)mi.Invoke(behaviour, emptyArray)))
                        .Where(t => t.Animation != null)
                        .Select(t => new AnimationInfo(t.FieldInfo.Name, MemberType.Method, t.Animation))
                        .ToList());
            }

            foreach (AnimationInfo animationInfo in animations)
            {
                switch (animationInfo.Animation)
                {
                    case Echo echo:
                        if (echo.Timeout == null)
                        {
                            echo.SetTimeout(EchoOptions.DefaultTime);
                        }

                        break;
                }

                if (animationInfo.Animation.AutoStart)
                {
                    animationInfo.Animation.SetAutoStart(false);
                }
            }

            return animations;
        }
    }
}