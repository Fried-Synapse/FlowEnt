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

        protected IControllable Controllable { get; private set; }
        protected bool IsRunning => (Controllable.PlayState & PlayState.Playing) != 0 || (Controllable.PlayState & PlayState.Paused) != 0;
        protected ControlButton PrevFrame { get; }
        protected ControlButton PlayPause { get; }
        protected ControlButton NextFrame { get; }
        protected ControlButton Stop { get; }
        protected ControlButton[] Buttons { get; }
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
                animation.OnUpdated(_ => UpdateSeekable());
                animation.OnCompleted(UpdatePlayState);
            }
            if (Controllable.IsSeekable)
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
            if (Controllable.IsSeekable)
            {
                ControlBar.OnValueChanged += (data) =>
                {
                    if (Controllable.PlayState == PlayState.Playing)
                    {
                        Controllable.Pause();
                    }
                    Controllable.SeekRatio = data.NewValue;
                    UpdatePlayState();
                };
            }
        }

        protected virtual void UpdatePlayState()
        {
            if (IsRunning)
            {
                AddToClassList("running");
            }
            else
            {
                RemoveFromClassList("running");
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

            PrevFrame.SetEnabled(IsRunning);
            Stop.SetEnabled(IsRunning);
        }

        private void UpdateSeekable()
        {
            if (!Controllable.IsSeekable)
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
                TimelineInfoTime.text = Controllable.ElapsedTime.ToString("F4");
                TimelineInfoTime.tooltip = string.Empty;
            }

            if (Controllable.PlayState != PlayState.Paused && Controllable.IsSeekable)
            {
                ControlBar.SetValueWithoutNotify(Controllable.SeekRatio);
            }
        }

        protected virtual void OnPrevFrame()
        {
            Controllable.SimulateFrames(-1);
        }

        protected virtual void OnNextFrame()
        {
            Controllable.SimulateFrames(1);
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
