using System;
using FriedSynapse.FlowEnt.Reflection;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class ControlSection : VisualElement
    {
        [Preserve]
        public new class UxmlFactory : UxmlFactory<ControlSection, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        public ControlSection()
        {
            this.LoadUxml<ControlSection>();
            PrevFrame = this.Query<ControlButton>("prevFrame").First();
            PlayPause = this.Query<ControlButton>("playPause").First();
            NextFrame = this.Query<ControlButton>("nextFrame").First();
            Stop = this.Query<ControlButton>("stop").First();
            TimeScale = this.Query<AutoScalableSlider>("timeScale").First();
            Timeline = this.Query<VisualElement>("timeline").First();
            ControlBar = this.Query<FriedSlider>("controlBar").First();
            TimelineInfoTime = this.Query<TextElement>("time").First();
        }

        protected IControllable Controllable { get; private set; }
        protected bool IsBuilding => (Controllable.PlayState & PlayState.Building) == Controllable.PlayState;
        protected ControlButton PrevFrame { get; }
        protected ControlButton PlayPause { get; }
        protected ControlButton NextFrame { get; }
        protected ControlButton Stop { get; }
        protected AutoScalableSlider TimeScale { get; }
        protected VisualElement Timeline { get; }
        protected TextElement TimelineInfoTime { get; }
        protected FriedSlider ControlBar { get; }

        public virtual void Init(IControllable controllable)
        {
            if (Controllable != null)
            {
                throw new InvalidOperationException("The controllable has already been set.");
            }
            Controllable = controllable;
            if (Controllable is AbstractAnimation animation)
            {
                Timeline.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
                animation.OnUpdated(t => UpdateAnimationTime(animation));
                animation.OnCompleted(UpdatePlayState);
            }

            if (Controllable is Tween)
            {
                ControlBar.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
            }

            TimeScale.MaxValue = Mathf.Max(1f, Controllable.TimeScale * 2f);
            TimeScale.Value = Controllable.TimeScale;

            UpdatePlayState();
            Bind();
        }

        protected virtual void Bind()
        {
            PrevFrame.clicked += OnPrevFrame;
            PlayPause.clicked += OnPlayPause;
            NextFrame.clicked += OnNextFrame;
            Stop.clicked += OnStop;

            foreach (Button button in new[] { PrevFrame, PlayPause, NextFrame, Stop })
            {
                button.clicked += UpdatePlayState;
            }

            TimeScale.OnValueChanged += (data) => Controllable.TimeScale = data.NewValue;
            if (Controllable is Tween tween)
            {
                ControlBar.OnValueChanged += (data) =>
                {
                    tween.Pause();
                    tween.InvokeMethod("UpdateInternal", new object[1] { tween.Time * (data.NewValue - data.OldValue) });
                    UpdatePlayState();
                };
            }
        }

        protected virtual void UpdatePlayState()
        {
            if (!IsBuilding)
            {
                AddToClassList("playing");
            }
            else
            {
                RemoveFromClassList("playing");
            }
            UpdateButtonsState();
            if (Controllable is AbstractAnimation animation)
            {
                UpdateAnimationTime(animation);
            }
        }

        private void UpdateButtonsState()
        {
            PlayPause.Type = Controllable.PlayState switch
            {
                PlayState.Waiting => ControlButton.ControlType.Pause,
                PlayState.Playing => ControlButton.ControlType.Pause,
                _ => ControlButton.ControlType.Play,
            };

            PrevFrame.SetEnabled(!IsBuilding);
            Stop.SetEnabled(!IsBuilding);
        }

        private void UpdateAnimationTime(AbstractAnimation animation)
        {
            float elapsedTime = animation.GetElapsedTime();
            TimelineInfoTime.text = elapsedTime.ToString("F4");
            if (Controllable.PlayState != PlayState.Paused && Controllable is Tween tween)
            {
                ControlBar.SetValueWithoutNotify(elapsedTime / tween.Time);
            }
        }

        protected virtual void OnPrevFrame()
        {
            Controllable.ChangeFrame(-1);
        }

        protected virtual void OnNextFrame()
        {
            Controllable.ChangeFrame(1);
        }

        protected virtual void OnPlayPause()
        {
            switch (Controllable.PlayState)
            {
                case PlayState.Playing:
                    Controllable.Pause();
                    break;
                default:
                    Controllable.Resume();
                    break;
            }
        }

        protected virtual void OnStop()
        {
            Controllable.Stop();
        }
    }
}