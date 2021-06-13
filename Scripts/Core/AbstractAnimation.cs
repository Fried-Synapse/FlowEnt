using System;

namespace FlowEnt
{
    public abstract class AbstractAnimation : FlowEntObject, IUpdatable
    {

        private class AutoStartHelper : IUpdatable
        {
            public AutoStartHelper(Action<float> callback)
            {
                Callback = callback;
            }

            public int UpdateIndex { get; set; }
            private Action<float> Callback { get; set; }

            public void Update(float deltaTime)
            {
                FlowEntController.Instance.UnsubscribeFromUpdate(this);
                Callback.Invoke(deltaTime);
            }
        }

        public AbstractAnimation(bool autoStart = false)
        {
            AutoStart = autoStart;
            if (AutoStart)
            {
                FlowEntController.Instance.SubscribeToUpdate(new AutoStartHelper(OnAutoStart));
            }
        }

        #region Settings Properties

        protected bool AutoStart { get; }

        public PlayState PlayState { get; protected set; } = PlayState.Building;

        #endregion

        #region Flow Data

        public AbstractAnimation Next { get; protected set; }
        public int UpdateIndex { get; set; }

        #endregion

        protected abstract void OnAutoStart(float deltaTime);

        public abstract void Update(float deltaTime);

        public void Pause()
        {
            FlowEntController.Instance.UnsubscribeFromUpdate(this);
        }

        public void Play()
        {
            FlowEntController.Instance.SubscribeToUpdate(this);
        }
    }
}
