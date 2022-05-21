using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class ControlSection : VisualElement
    {
        internal ControlSection(IControllable controllable)
        {
            this.LoadUxml();
            this.LoadUss();
            Controllable = controllable;
            PrevFrame = this.Query<ControlButton>("prevFrame").First();
            PlayPause = this.Query<ControlButton>("playPause").First();
            NextFrame = this.Query<ControlButton>("nextFrame").First();
            Stop = this.Query<ControlButton>("stop").First();
            Init();
            Bind();
        }

        private IControllable Controllable { get; }
        private bool IsBuilding => (Controllable.PlayState & PlayState.Building) == Controllable.PlayState;
        private ControlButton PrevFrame { get; }
        private ControlButton PlayPause { get; }
        private ControlButton NextFrame { get; }
        private ControlButton Stop { get; }

        private void Init()
        {
            UpdatePlayState();
        }

        private void Bind()
        {
            foreach (Button button in new[] { PrevFrame, PlayPause, NextFrame, Stop })
            {
                button.clicked += UpdatePlayState;
            }
            PrevFrame.clicked += () => Controllable.ChangeFrame(-1);
            PlayPause.clicked += () => OnPlayPause();
            NextFrame.clicked += () => Controllable.ChangeFrame(1);
            Stop.clicked += () => Controllable.Stop();
        }

        private void UpdatePlayState()
        {
            UpdateButtonsState();
            //TODO add the red background
        }

        private void UpdateButtonsState()
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

        private void OnPlayPause()
        {

        }
    }
}