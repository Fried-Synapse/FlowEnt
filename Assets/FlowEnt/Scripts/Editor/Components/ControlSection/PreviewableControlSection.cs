namespace FriedSynapse.FlowEnt.Editor
{
    internal class PreviewableControlSection : ControlSection
    {
        public PreviewableControlSection(AbstractAnimation animation) : base(animation)
        {
            Animation = animation;
        }
        private AbstractAnimation Animation { get; }

        protected override void Bind()
        {
            if (Controllable is Tween tween)
            {
                ControlBar.OnValueChanging += (_) =>
                {
                    if (IsBuilding)
                    {
                        StartPreview();
                    }
                };
            }
            base.Bind();
        }

        protected override void OnPlayPause()
        {
            switch (Animation.PlayState)
            {
                case PlayState.Waiting:
                case PlayState.Playing:
                    Animation.Pause();
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

        private void StartPreview()
        {
            PreviewController.Start(new PreviewOptions(Animation));
        }
    }
}
