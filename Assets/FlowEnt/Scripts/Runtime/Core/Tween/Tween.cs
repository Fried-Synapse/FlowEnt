using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// A tween is a simple interpolation from 0 to 1 which has several options and events attached.
    /// For more information please go to https://flowent.friedsynapse.com/tween
    /// </summary>
    public sealed partial class Tween : AbstractAnimation,
        IFluentControllable<Tween>
    {
        private enum LoopDirection
        {
            Forward,
            Backward
        }

        /// <summary>
        /// Creates a new tween using the options provided.
        /// </summary>
        /// <param name="options"></param>
        public Tween(TweenOptions options)
        {
            SetOptions(options);
        }

        /// <summary>
        /// Creates a new tween.
        /// </summary>
        /// <param name="time">The amount of time for this tween in seconds.</param>
        /// <param name="autoStart">Whether the tween should start automatically or not.</param>
        public Tween(float time = TweenOptions.DefaultTime, bool autoStart = AbstractAnimationOptions.DefaultAutoStart)
        {
            if (time < 0)
            {
                throw new ArgumentException(TweenOptions.ErrorTimeNegative);
            }
            if (float.IsInfinity(time))
            {
                throw new ArgumentException(TweenOptions.ErrorTimeInfinity);
            }
            AutoStart = autoStart;
            this.time = time;
        }

        private ITweenMotion[] motions = new ITweenMotion[0];
        private int? remainingLoops;
        private float remainingTime;
        private LoopDirection loopDirection;

        #region Controls

        /// <inheritdoc cref="AbstractAnimation.Start" />
        /// <exception cref="AnimationException">If the tween has already started.</exception>
        /// \copydoc AbstractAnimation.Start
        public new Tween Start()
        {
            base.Start();
            return this;
        }

        /// <inheritdoc cref="AbstractAnimation.StartAsync" />
        /// \copydoc AbstractAnimation.StartAsync
        /// <exception cref="AnimationException">If the tween has already started.</exception>
        public new async Task<Tween> StartAsync()
        {
            await base.StartAsync();
            return this;
        }

        /// <inheritdoc cref="AbstractAnimation.Resume" />
        /// \copydoc AbstractAnimation.Resume
        public new Tween Resume()
        {
            base.Resume();
            return this;
        }

        /// <inheritdoc cref="AbstractAnimation.Pause" />
        /// \copydoc AbstractAnimation.Pause
        public new Tween Pause()
        {
            base.Pause();
            return this;
        }

        /// <inheritdoc cref="AbstractAnimation.Stop(bool)" />
        /// \copydoc AbstractUpdatable.Stop
        public new Tween Stop(bool triggerOnCompleted = false)
        {
            StopInternal(triggerOnCompleted);
            return this;
        }

        /// <inheritdoc cref="AbstractAnimation.Reset" />
        /// \copydoc AbstractAnimation.Reset
        /// <exception cref="AnimationException">If the tween is not finished.</exception>
        public new Tween Reset()
        {
            ResetInternal();
            return this;
        }

        #endregion

        #region Lifecycle

        internal override void StartInternal(float deltaTime = 0)
        {
            playState = PlayState.Waiting;

            if (skipFrames > 0)
            {
                StartSkipFrames();
                return;
            }

            if (delay > 0f)
            {
                StartDelay();
                return;
            }

            remainingLoops = loopCount;
            remainingTime = time;

            onStarting?.Invoke();
            for (int i = 0; i < motions.Length; i++)
            {
                motions[i].OnStart();
            }
            updateController.SubscribeToUpdate(this);
            playState = PlayState.Playing;
            onStarted?.Invoke();

            UpdateInternal(deltaTime);
        }

        internal override void UpdateInternal(float deltaTime)
        {
            remainingTime -= deltaTime * timeScale;

            if (remainingTime < 0)
            {
                overdraft = -remainingTime / timeScale;
                remainingTime = 0;
            }

            float currentLoopTime = loopDirection == LoopDirection.Forward ? time - remainingTime : remainingTime;
            float t = easing.GetValue(currentLoopTime / time);

            onUpdating?.Invoke(t);
            for (int i = 0; i < motions.Length; i++)
            {
                motions[i].OnUpdate(t);
            }
            onUpdated?.Invoke(t);

            if (overdraft != null)
            {
                CompleteLoop();
            }
        }

        private void CompleteLoop()
        {
            remainingTime = time;
            remainingLoops--;

            if (!(remainingLoops <= 0))
            {
                for (int i = 0; i < motions.Length; i++)
                {
                    motions[i].OnLoopComplete();
                }

                onLoopCompleted?.Invoke(remainingLoops);
                if (loopType == LoopType.PingPong)
                {
                    loopDirection = loopDirection == LoopDirection.Forward ? LoopDirection.Backward : LoopDirection.Forward;
                }
                float overdraft = this.overdraft.Value;
                this.overdraft = null;
                UpdateInternal(overdraft);
                return;
            }

            if (remainingLoops == 0)
            {
                onLoopCompleted?.Invoke(remainingLoops);
            }

            updateController.UnsubscribeFromUpdate(this);

            onCompleting?.Invoke();
            playState = PlayState.Finished;
            for (int i = 0; i < motions.Length; i++)
            {
                motions[i].OnComplete();
            }
            onCompleted?.Invoke();

            if (updateController is Flow parentFlow)
            {
                parentFlow.CompleteUpdatable(this);
            }
        }

        protected override void ResetInternal()
        {
            base.ResetInternal();
            loopDirection = LoopDirection.Forward;
            remainingLoops = 0;
            remainingTime = 0f;
        }

        #endregion

        #region Motions

        /// <summary>
        /// Applies all the motions to the current tween.
        /// </summary>
        /// <param name="motions"></param>
        public Tween Apply(params ITweenMotion[] motions)
        {
            this.motions = this.motions.Concat(motions).ToArray();
            return this;
        }

        /// <inheritdoc cref="Apply(ITweenMotion[])"/>
        /// \copydoc Tween.Apply
        /// <param name="motions"></param>
        public Tween Apply(IEnumerable<ITweenMotion> motions)
        {
            this.motions = this.motions.Concat(motions).ToArray();
            return this;
        }

        /// <summary>
        /// Creates a scope for the object so you can add motions designed specifically for that object.
        /// </summary>
        /// <param name="element"></param>
        /// <typeparam name="TItem"></typeparam>
        public TweenMotionProxy<TItem> For<TItem>(TItem element)
            where TItem : class
        {
            return new TweenMotionProxy<TItem>(this, element);
        }

        /// <summary>
        /// Creates a scope for the object so you can add motions designed specifically for that array of objects.
        /// </summary>
        /// <param name="elements"></param>
        /// <typeparam name="TItem"></typeparam>
        public TweenMotionProxyArray<TItem> For<TItem>(params TItem[] elements)
            where TItem : class
        {
            return new TweenMotionProxyArray<TItem>(this, elements);
        }

        /// <summary>
        /// Creates a scope for the object so you can add motions designed specifically for that enumeration of objects.
        /// </summary>
        /// <param name="elements"></param>
        /// <typeparam name="TItem"></typeparam>
        public TweenMotionProxyArray<TItem> ForAll<TItem>(IEnumerable<TItem> elements)
            where TItem : class
        {
            return new TweenMotionProxyArray<TItem>(this, elements.ToArray());
        }

        #endregion
    }
}
