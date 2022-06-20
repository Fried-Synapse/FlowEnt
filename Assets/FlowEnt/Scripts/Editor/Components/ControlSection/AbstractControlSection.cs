using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class AbstractControlSection : VisualElement
    {
        public AbstractControlSection()
        {
            this.LoadUxml<AbstractControlSection>();
        }
    }
    internal class AbstractControlSection<TControllable> : AbstractControlSection
            where TControllable : IControllable
    {
        public AbstractControlSection()
        {
            PrevFrame = this.Query<ControlButton>("prevFrame").First();
            PlayPause = this.Query<ControlButton>("playPause").First();
            NextFrame = this.Query<ControlButton>("nextFrame").First();
            Stop = this.Query<ControlButton>("stop").First();
            TimeScale = this.Query<AutoScalableSlider>("timeScale").First();
            Timeline = this.Query<VisualElement>("timeline").First();
            ControlBar = this.Query<FriedSlider>("controlBar").First();
            TimelineInfoTime = this.Query<TextElement>("time").First();
        }

        protected TControllable Controllable { get; private set; }
        protected IManuallyUpdatable ManuallyUpdatable { get; private set; }
        protected bool IsManuallyUpdatable { get; private set; }
        protected bool IsBuilding => (Controllable.PlayState & PlayState.Building) == Controllable.PlayState;
        protected ControlButton PrevFrame { get; }
        protected ControlButton PlayPause { get; }
        protected ControlButton NextFrame { get; }
        protected ControlButton Stop { get; }
        protected AutoScalableSlider TimeScale { get; }
        protected VisualElement Timeline { get; }
        protected TextElement TimelineInfoTime { get; }
        protected FriedSlider ControlBar { get; }

        public virtual void Init(TControllable controllable)
        {
            if (Controllable != null)
            {
                throw new InvalidOperationException("The controllable has already been set.");
            }
            Controllable = controllable;
            InitManuallyUpdatable();
            if (Controllable is AbstractAnimation animation)
            {
                Timeline.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
                animation.OnUpdated(_ => UpdateAnimationTime());
                animation.OnCompleted(UpdatePlayState);
            }

            if (IsManuallyUpdatable)
            {
                ControlBar.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
            }

            TimeScale.MaxValue = Mathf.Max(1f, Controllable.TimeScale * 2f);
            TimeScale.Value = Controllable.TimeScale;

            UpdatePlayState();
            Bind();
        }

        protected void InitManuallyUpdatable()
        {
            if (!(Controllable is IManuallyUpdatable result))
            {
                IsManuallyUpdatable = false;
                return;
            }
            ManuallyUpdatable = result;
            IsManuallyUpdatable = !(Controllable is Echo echo && echo.Timeout == null)
                && !(Controllable is Flow);
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
            if (IsManuallyUpdatable)
            {
                ControlBar.OnValueChanged += (data) =>
                {
                    ManuallyUpdatable.Ratio = data.NewValue;
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
            if (Controllable is AbstractAnimation)
            {
                UpdateAnimationTime();
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

        private void UpdateAnimationTime()
        {
            TimelineInfoTime.text = ManuallyUpdatable.ElapsedTime.ToString("F4");
            if (Controllable.PlayState != PlayState.Paused && IsManuallyUpdatable)
            {
                ControlBar.SetValueWithoutNotify(ManuallyUpdatable.Ratio);
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
