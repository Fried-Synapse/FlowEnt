using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class ControlSection : VisualElement
    {
        internal ControlSection(IControllable controllable)
        {
            this.LoadUxml<ControlSection>();
            this.LoadUss<ControlSection>();
            Controllable = controllable;
            PrevFrame = this.Query<ControlButton>("prevFrame").First();
            PlayPause = this.Query<ControlButton>("playPause").First();
            NextFrame = this.Query<ControlButton>("nextFrame").First();
            Stop = this.Query<ControlButton>("stop").First();
            //TODO improve these selectors
            ControlBar = this.Query<ControlBar>("controlBar").First();
            TimelineInfo = this.Query<VisualElement>("info").First();
            TimelineInfoTime = this.Query<TextElement>("time").First();
            Init();
            Bind();
        }

        protected IControllable Controllable { get; }
        protected bool IsBuilding => (Controllable.PlayState & PlayState.Building) == Controllable.PlayState;
        protected ControlButton PrevFrame { get; }
        protected ControlButton PlayPause { get; }
        protected ControlButton NextFrame { get; }
        protected ControlButton Stop { get; }
        protected ControlBar ControlBar { get; }
        protected VisualElement TimelineInfo { get; }
        protected TextElement TimelineInfoTime { get; }

        protected virtual void Init()
        {
            switch (Controllable)
            {
                case Tween _:
                    ControlBar.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
                    break;
                case Echo _:
                case Flow _:
                    TimelineInfo.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
                    break;
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
            //TODO add the red background
        }

        protected virtual void UpdateButtonsState()
        {
            PlayPause.Type = Controllable.PlayState switch
            {
                PlayState.Playing => ControlButton.ControlType.Pause,
                PlayState.Finished => ControlButton.ControlType.Replay,
                _ => ControlButton.ControlType.Play,
            };

            PrevFrame.SetEnabled(!IsBuilding);
            Stop.SetEnabled(!IsBuilding);
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