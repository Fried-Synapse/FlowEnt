using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    public class PreviewerWindow : EditorWindow
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
            }
            public string name;
            public AbstractAnimation animation;
        }

        private const string BaseAssetPath = "Assets/FlowEnt/Scripts/Editor/Menus/Previewer/" + nameof(PreviewerWindow);
        private const string VisualTreePath = BaseAssetPath + ".uxml";
        private const string StyleSheetPath = BaseAssetPath + ".uss";
        private const BindingFlags DefaultBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        private static readonly Type abstractAnimationType = typeof(AbstractAnimation);
        private static readonly Type abstractAnimationBuilderType = typeof(IAbstractAnimationBuilder);
        private static readonly object[] emptyArray = { };
        internal static PreviewerWindow Instance { get; private set; }
        private Transform transform;
        private List<AnimationInfo> animations;

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

            //TODO do we need this?
            //Repaint();
        }

        private void OnDestroy()
        {
            Instance = default;
        }

        private TextElement Label { get; set; }
        private VisualElement Animations { get; set; }

        public void CreateGUI()
        {
            this.LoadUxml();
            rootVisualElement.Query<VisualElement>("head").First().Add(new Header("FlowEnt Previewer"));
            Label = rootVisualElement.Query<TextElement>("name").First();
            Animations = rootVisualElement.Query<VisualElement>("animations").First();
            Render();
        }

        private void Render()
        {
            Label.text = transform == null ? "Select an object from the hierarchy first." : transform.name;
            if (animations?.Count == 0)
            {
                Animations.Clear();
            }
            else
            {
                foreach (AnimationInfo animationInfo in animations)
                {
                    Animations.Add(new TextElement()
                    {
                        text = animationInfo.name
                    });
                    Animations.Add(new ControllableSection(animationInfo.animation));
                }
            }
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