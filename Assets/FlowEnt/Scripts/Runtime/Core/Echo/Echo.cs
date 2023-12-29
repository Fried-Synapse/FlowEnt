using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// An echo is a simple update loop that provides a delta time and other settings for custom animation
    /// For more information please go to https://flowent.friedsynapse.com/echo
    /// </summary>
    public sealed partial class Echo : AbstractAnimation,
        IFluentControllable<Echo>
    {
        /// <summary>
        /// Creates a new echo using the options provided.
        /// </summary>
        /// <param name="options"></param>
        public Echo(EchoOptions options)
        {
            SetOptions(options);
        }

        /// <summary>
        /// Creates a new echo.
        /// </summary>
        /// <param name="timeout">The amount of time for this echo in seconds.</param>
        /// <param name="autoStart">Whether the echo should start automatically or not.</param>
        public Echo(float? timeout = default, bool autoStart = AbstractAnimationOptions.DefaultAutoStart)
        {
            if (timeout < EchoOptions.MinTime)
            {
                throw new ArgumentException(EchoOptions.ErrorTimeoutMin);
            }

            if (timeout != null && float.IsInfinity(timeout.Value))
            {
                throw new ArgumentException(EchoOptions.ErrorTimeoutInfinity);
            }

            this.timeout = timeout != null && float.IsInfinity(timeout.Value) ? null : timeout;
            AutoStart = autoStart;
        }

        private readonly List<AbstractEchoMotion> motions = new(1);
        private int? remainingLoops;

        #region Seek

        private protected override bool IsSeekable => timeout != null;
        private protected override float TotalSeekTime => timeout ?? 0;

        private protected override float GetSeekingDeltaTimeFromRatio(float ratio)
            => ((ratio * timeout ?? 0) - elapsedTime) / timeScale;

        #endregion

        #region Controls

        /// <inheritdoc cref="AbstractAnimation.Start" />
        /// <exception cref="AnimationException">If the echo has already started.</exception>
        /// \copydoc AbstractAnimation.Start
        public new Echo Start()
        {
            base.Start();
            return this;
        }

        /// <inheritdoc cref="AbstractAnimation..StartAsync(CancellationToken?)" />
        /// \copydoc AbstractAnimation..StartAsync(CancellationToken?)
        /// <exception cref="AnimationException">If the echo has already started.</exception>
        public new async Task<Echo> StartAsync(CancellationToken? token = null)
        {
            await base.StartAsync(token);
            return this;
        }

        /// <inheritdoc cref="AbstractAnimation.Resume" />
        /// \copydoc AbstractAnimation.Resume
        public new Echo Resume()
        {
            base.Resume();
            return this;
        }

        /// <inheritdoc cref="AbstractAnimation.Pause" />
        /// \copydoc AbstractAnimation.Pause
        public new Echo Pause()
        {
            base.Pause();
            return this;
        }

        /// <inheritdoc cref="AbstractAnimation.Stop(bool)" />
        /// \copydoc AbstractUpdatable.Stop(bool)
        public new Echo Stop(bool triggerOnCompleted = false)
        {
            StopInternal(triggerOnCompleted);
            return this;
        }

        /// <inheritdoc cref="AbstractAnimation.Reset" />
        /// \copydoc AbstractAnimation.Reset
        /// <exception cref="AnimationException">If the echo is not finished.</exception>
        public new Echo Reset()
        {
            ResetInternal();
            return this;
        }

        #endregion

        #region Lifecycle

        internal override void StartInternal(float deltaTime = 0)
        {
            if (startHelperType != StartHelperType.None && TryStartHelpers())
            {
                return;
            }

            remainingLoops = loopCount;

            onStarting?.Invoke();
            for (int i = 0; i < motions.Count; i++)
            {
                motions[i].OnStart();
            }

            updateController.SubscribeToUpdate(this);
            playState = PlayState.Playing;
            onStarted?.Invoke();
            StartLoop();
            UpdateInternal(deltaTime);
        }

        internal override void UpdateInternal(float deltaTime)
        {
            float scaledDeltaTime = deltaTime * timeScale;
            elapsedTime += scaledDeltaTime;

            if (elapsedTime > timeout)
            {
                overdraft = (elapsedTime - timeout.Value) / timeScale;
            }

            if (stopCondition?.Invoke(elapsedTime) == true)
            {
                overdraft = 0f;
            }

            onUpdating?.Invoke(scaledDeltaTime);
            for (int i = 0; i < motions.Count; i++)
            {
                motions[i].OnUpdate(scaledDeltaTime);
            }

            onUpdated?.Invoke(scaledDeltaTime);

            if (overdraft != null)
            {
                CompleteLoop();
            }
        }

        private void StartLoop()
        {
            for (int i = 0; i < motions.Count; i++)
            {
                motions[i].OnLoopStart();
            }

            onLoopStarted?.Invoke(remainingLoops);
        }

        private void CompleteLoop()
        {
            remainingLoops--;

            if (!(remainingLoops <= 0))
            {
                elapsedTime = 0;

                for (int i = 0; i < motions.Count; i++)
                {
                    motions[i].OnLoopComplete();
                }

                onLoopCompleted?.Invoke(remainingLoops);
                float overdraft = this.overdraft.Value;
                this.overdraft = null;
                StartLoop();
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
            for (int i = 0; i < motions.Count; i++)
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
            remainingLoops = 0;
        }

        #endregion

        #region Motions

        /// <summary>
        /// Applies all the motions to the current echo.
        /// </summary>
        /// <param name="motions"></param>
        public Echo Apply(params AbstractEchoMotion[] motions)
        {
            this.motions.AddRange(motions);
            return this;
        }

        /// <inheritdoc cref="Apply(AbstractEchoMotion[])"/>
        /// \copydoc Echo.Apply
        /// <param name="motions"></param>
        public Echo Apply(IEnumerable<AbstractEchoMotion> motions)
        {
            this.motions.AddRange(motions);
            return this;
        }

        /// <summary>
        /// Creates a scope for the object so you can add motions designed specifically for that object.
        /// </summary>
        /// <param name="element"></param>
        /// <typeparam name="TItem"></typeparam>
        public EchoMotionProxy<TItem> For<TItem>(TItem element)
        {
            return new EchoMotionProxy<TItem>(this, element);
        }

        /// <summary>
        /// Creates a scope for the object so you can add motions designed specifically for that array of objects.
        /// </summary>
        /// <param name="elements"></param>
        /// <typeparam name="TItem"></typeparam>
        public EchoMotionProxyArray<TItem> For<TItem>(params TItem[] elements)
        {
            return new EchoMotionProxyArray<TItem>(this, elements);
        }

        /// <summary>
        /// Creates a scope for the object so you can add motions designed specifically for that enumeration of objects.
        /// </summary>
        /// <param name="elements"></param>
        /// <typeparam name="TItem"></typeparam>
        public EchoMotionProxyArray<TItem> ForAll<TItem>(IEnumerable<TItem> elements)
        {
            return new EchoMotionProxyArray<TItem>(this, elements.ToArray());
        }

        #endregion
    }
}