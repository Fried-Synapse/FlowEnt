using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// A tween is a simple interpolation from 0 to 1 which has several options and events attached.
    /// For more information please go to https://flowent.friedsynapse.com/tween
    /// </summary>
    public sealed partial class Tween : AbstractAnimation,
        IFluentTweenOptionable<Tween>,
        IFluentTweenEventable<Tween>
    {
        public const float DefaultTime = 1f;
        public const bool DefaultAutoStart = false;
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
            CopyOptions(options);
        }

        /// <summary>
        /// Creates a new tween.
        /// </summary>
        /// <param name="time">The amount of time for this tween in seconds.</param>
        /// <param name="autoStart">Whether the tween should start automatically or not.</param>
        public Tween(float time = DefaultTime, bool autoStart = DefaultAutoStart)
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

        private IMotion[] motions = new IMotion[0];
        private int? remainingLoops;
        private float remainingTime;
        private LoopDirection loopDirection;

        #region Lifecycle

        /// <summary>
        /// Starts the tween.
        /// </summary>
        /// <exception cref="FlowEntException">If the tween has already started.</exception>
        public Tween Start()
        {
            if (playState != PlayState.Building)
            {
                throw new TweenException(this, "Tween already started.");
            }

            if (autoStartHelper != null)
            {
                CancelAutoStart();
            }
            StartInternal();
            return this;
        }

        /// <summary>
        /// Starts the tween async(you can await this till the tween finishes).
        /// </summary>
        /// <exception cref="FlowEntException">If the tween has already started.</exception>
        public async Task<Tween> StartAsync()
        {
            if (playState != PlayState.Building)
            {
                throw new TweenException(this, "Tween already started.");
            }

            if (autoStartHelper != null)
            {
                CancelAutoStart();
            }
            StartInternal();
            await new AwaitableAnimation(this);
            return this;
        }

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

            updateController.SubscribeToUpdate(this);

            onStarting?.Invoke();
            for (int i = 0; i < motions.Length; i++)
            {
                motions[i].OnStart();
            }
            playState = PlayState.Playing;
            onStarted?.Invoke();

            UpdateInternal(deltaTime);
        }

        internal override void UpdateInternal(float deltaTime)
        {
            remainingTime -= deltaTime * timeScale;

            if (remainingTime < 0)
            {
                overdraft = -remainingTime;
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

        #endregion

        #region Motions

        /// <summary>
        /// Applies the motion to the current tween.
        /// </summary>
        /// <param name="motion"></param>
        public Tween Apply(IMotion motion)
        {
            //TODO this could be really slow when adding a lot of motions. Consider using the fast list
            motions = motions.Append(motion).ToArray();
            return this;
        }

        /// <summary>
        /// Creates a scope for the object so you can add motions designed sepcifically for that object.
        /// </summary>
        /// <param name="element"></param>
        /// <typeparam name="T"></typeparam>
        public TweenMotion<T> For<T>(T element)
        {
            return new TweenMotion<T>(this, element);
        }

        /// <summary>
        /// Creates a scope for the object so you can add motions designed sepcifically for that array of objects.
        /// </summary>
        /// <param name="elements"></param>
        /// <typeparam name="T"></typeparam>
        public TweenMotionArray<T> For<T>(params T[] elements)
        {
            return new TweenMotionArray<T>(this, elements);
        }

        /// <summary>
        /// Creates a scope for the object so you can add motions designed sepcifically for that list of objects.
        /// </summary>
        /// <param name="elements"></param>
        /// <typeparam name="T"></typeparam>
        public TweenMotionArray<T> For<T>(List<T> elements)
        {
            return new TweenMotionArray<T>(this, elements.ToArray());
        }

        #endregion

    }
}
