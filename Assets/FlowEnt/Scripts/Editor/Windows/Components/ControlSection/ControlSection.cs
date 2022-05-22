using System;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class ControlSection : VisualElement
    {
        internal ControlSection(IControllable controllable)
        {
            this.LoadUxml<ControlSection>();
            Controllable = controllable;
            PrevFrame = this.Query<ControlButton>("prevFrame").First();
            PlayPause = this.Query<ControlButton>("playPause").First();
            NextFrame = this.Query<ControlButton>("nextFrame").First();
            Stop = this.Query<ControlButton>("stop").First();
            TimeScale = this.Query<AutoScalableSlider>("timeScale").First();
            Timeline = this.Query<VisualElement>("timeline").First();
            ControlBar = this.Query<UnitSlider>("controlBar").First();
            TimelineInfoTime = this.Query<TextElement>("time").First();
            Init();
            Bind();
        }

        protected IControllable Controllable { get; }
        private AbstractAnimation Animation { get; set; }
        protected bool IsBuilding => (Controllable.PlayState & PlayState.Building) == Controllable.PlayState;
        protected ControlButton PrevFrame { get; }
        protected ControlButton PlayPause { get; }
        protected ControlButton NextFrame { get; }
        protected ControlButton Stop { get; }
        protected AutoScalableSlider TimeScale { get; }
        protected VisualElement Timeline { get; }
        protected UnitSlider ControlBar { get; }
        protected TextElement TimelineInfoTime { get; }

        protected virtual void Init()
        {
            if (Controllable is AbstractAnimation animation)
            {
                Animation = animation;
                Timeline.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
                Animation.OnUpdated(_ => UpdateTime());
            }

            if (Controllable is Tween)
            {
                ControlBar.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
            }

            UpdatePlayState();
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
        }

        protected virtual void UpdatePlayState()
        {
            UpdateButtonsState();
            UpdateTime();
            //TODO add the red background
        }

        protected void UpdateButtonsState()
        {
            PlayPause.Type = Controllable.PlayState switch
            {
                PlayState.Playing => ControlButton.ControlType.Pause,
                PlayState.Finished => ControlButton.ControlType.Replay,
                _ => ControlButton.ControlType.Play,
            };

            PrevFrame.SetEnabled(!IsBuilding);
            Stop.SetEnabled(!IsBuilding);
            TimeScale.Value = Controllable.TimeScale;
        }

        protected void UpdateTime()
        {
            TimelineInfoTime.text = Animation.GetElapsedTime().ToString("F4");
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
            UpdatePlayState();
        }

        protected virtual void OnStop()
        {
            Controllable.Stop();
        }
    }
}