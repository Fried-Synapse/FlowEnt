using System;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class PreviewableControlSection : ControlSection
    {
        private AbstractAnimation Animation => (AbstractAnimation)Controllable;

        public override void Init(IControllable controllable)
        {
            throw new InvalidOperationException($"Use the other {nameof(Init)}");
        }

        public void Init(AbstractAnimation animation)
        {
            base.Init(animation);
        }

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
            PreviewController.Start(Animation);
        }
    }
}
