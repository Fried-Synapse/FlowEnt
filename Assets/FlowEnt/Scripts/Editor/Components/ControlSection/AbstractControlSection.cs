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
            Buttons = new[] { PrevFrame, PlayPause, NextFrame, Stop };
            TimeScale = this.Query<AutoScalableSlider>("timeScale").First();
            Timeline = this.Query<VisualElement>("timeline").First();
            ControlBar = this.Query<FriedSlider>("controlBar").First();
            TimelineInfoTime = this.Query<TextElement>("time").First();
        }

        protected TControllable Controllable { get; private set; }
        protected ISeekable Seekable { get; private set; }
        protected bool IsBuilding => (Controllable.PlayState & PlayState.Building) == Controllable.PlayState;
        protected ControlButton PrevFrame { get; }
        protected ControlButton PlayPause { get; }
        protected ControlButton NextFrame { get; }
        protected ControlButton Stop { get; }
        protected ControlButton[] Buttons { get; }
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
            if (Controllable is AbstractAnimation animation)
            {
                Timeline.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
                animation.OnUpdated(_ => UpdateSeekable());
                animation.OnCompleted(UpdatePlayState);
            }
            if (Controllable is ISeekable result)
            {
                Seekable = result;
            }
            if (Seekable?.IsSeekable == true)
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

            foreach (Button button in Buttons)
            {
                button.clicked += UpdatePlayState;
            }

            TimeScale.OnValueChanged += (data) => Controllable.TimeScale = data.NewValue;
            if (Seekable?.IsSeekable == true)
            {
                ControlBar.OnValueChanged += (data) =>
                {
                    Seekable.Ratio = data.NewValue;
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
            UpdateSeekable();
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

        private void UpdateSeekable()
        {
            if (Seekable == null)
            {
                return;
            }
            if (Controllable.PlayState == PlayState.Waiting)
            {
                ControlBar.SetEnabled(false);
                TimelineInfoTime.text = "Waiting";
                TimelineInfoTime.tooltip = "Animation is either delayed or skipping frames.";
            }
            else
            {
                ControlBar.SetEnabled(true);
                TimelineInfoTime.text = Seekable.ElapsedTime.ToString("F4");
                TimelineInfoTime.tooltip = string.Empty;
            }

            if (Controllable.PlayState != PlayState.Paused && Seekable.IsSeekable)
            {
                ControlBar.SetValueWithoutNotify(Seekable.Ratio);
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
