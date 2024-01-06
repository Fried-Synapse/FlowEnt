using System.Collections.Generic;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using FriedSynapse.FlowEnt.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class AnimationInspectorWindow : AbstractThemedWindow<AnimationInspectorWindow>
    {
        internal static void ShowGrouped(AbstractAnimation animation)
        {
            AnimationInspectorWindow window = CreateWindow<AnimationInspectorWindow>(Types);
            window.Init(animation);
            window.titleContent = new GUIContent(window.Name, window.Logo);
        }

        protected override string Name => animation.ToString();

        private AbstractAnimation animation;
        private new AnimationNameElement name;
        private ControlSection controlSection;
        private TextElement updateType;
        private VisualElement motions;
        private VisualElement stackTraceContent;

        protected override void CreateGUI()
        {
            LoadContent();
            EditorApplication.update += Update;
            name = Content.Query<AnimationNameElement>("name").First();
            controlSection = Content.Query<ControlSection>("control").First();
            updateType = Content.Query<VisualElement>("updateType").First().Query<TextElement>("value").First();
            motions = Content.Query<VisualElement>("motions").First();
            stackTraceContent = Content.Query<VisualElement>("stackTrace").First().Query<VisualElement>("content").First();
        }

        private void OnDestroy()
        {
            EditorApplication.update -= Update;
        }

        private void Init(AbstractAnimation animation)
        {
            this.animation = animation;
            name.Init(this.animation);
            controlSection.Init(this.animation);
            updateType.text = this.animation.GetFieldValue<UpdateType>("updateType").ToString();
            switch (this.animation)
            {
                case Tween:
                    InitMotions<AbstractTweenMotion>();
                    break;
                case Echo:
                    InitMotions<AbstractEchoMotion>();
                    break;
                case Flow:
                    Content.Remove(motions);
                    break;
            }

            InitStackTrace();
        }

        private void InitMotions<TMotion>()
        {
            foreach (TMotion motion in animation.GetFieldValue<List<TMotion>>("motions"))
            {
                motions.Add(new TextElement
                {
                    text = motion.GetType().Name,
                    tooltip = motion.GetType().FullName
                });
            }
        }

        private void InitStackTrace()
        {
            stackTraceContent.Clear();
#if FlowEnt_Debug || (UNITY_EDITOR && FlowEnt_Debug_Editor)
            TextElement text = new()
            {
                text = animation.GetFieldValue<string>("stackTrace")
            };
            ScrollView scroll = new();
            scroll.contentContainer.Add(text);
            stackTraceContent.Add(scroll);
#else
            stackTraceContent.Add(new HelpBox("Stack trace only available when debugging enabled. Please enable it in settings.", HelpBoxMessageType.Info));
            Button settingButton = new Button();
            settingButton.text = "Open settings";
            settingButton.clicked += FlowEntMenu.ShowSettings;
            stackTraceContent.Add(settingButton);
#endif
        }

        private void Update()
        {
            if (animation == null)
            {
                Close();
            }
        }
    }
}