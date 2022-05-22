namespace FriedSynapse.FlowEnt.Editor
{
    internal class PreviewableControlSection : ControlSection
    {
        public PreviewableControlSection(AbstractAnimation animation) : base(animation)
        {
            Animation = animation;
            Animation.OnCompleted(OnCompleted);
        }
        private AbstractAnimation Animation { get; }

        protected override void OnPlayPause()
        {
            switch (Animation.PlayState)
            {
                case PlayState.Playing:
                    Animation.Pause();
                    break;
                case PlayState.Finished:
                    PreviewController.Reset();
                    break;
                default:
                    if (IsBuilding)
                    {
                        StartPreview();
                    }
                    else
                    {
                        Animation.Resume();
                    }
                    break;
            }
        }

        protected override void OnNextFrame()
        {
            if (IsBuilding)
            {
                StartPreview();
                Animation.Pause();
            }
            base.OnNextFrame();
        }

        protected override void OnStop()
        {
            PreviewController.Stop();
        }

        private void OnCompleted()
        {
            UpdatePlayState();
        }

        private void StartPreview()
        {
            //TODO update the correct value
            PreviewController.Start(new PreviewOptions(Animation)
            {
                OnUpdate = () => ControlBar.Value = Animation.GetElapsedTime(),
                OnStop = () =>
                {
                    ControlBar.Value = 0;
                    UpdatePlayState();
                }
            });
        }
    }
}
