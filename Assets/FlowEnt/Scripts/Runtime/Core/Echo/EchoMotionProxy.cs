using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Wrapper class that is used to apply motions to an object of any type using a echo
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class EchoMotionProxy<TItem> :
        IFluentControllable<EchoMotionProxy<TItem>>,
        IFluentEchoOptionable<EchoMotionProxy<TItem>>,
        IFluentEchoEventable<EchoMotionProxy<TItem>>
    {
        public EchoMotionProxy(Echo echo, TItem item)
        {
            Echo = echo;
            Item = item;
        }

        public Echo Echo { get; }
        public TItem Item { get; }

        public static implicit operator Echo(EchoMotionProxy<TItem> proxy) => proxy.Echo;

        #region Motions

        /// <inheritdoc cref="Echo.Apply(AbstractEchoMotion[])"/>
        /// \copydoc Echo.Apply
        public EchoMotionProxy<TItem> Apply(params AbstractEchoMotion[] motions)
        {
            Echo.Apply(motions);
            return this;
        }

        /// <inheritdoc cref="Echo.Apply(AbstractEchoMotion[])"/>
        /// \copydoc Echo.Apply
        public EchoMotionProxy<TItem> Apply(IEnumerable<AbstractEchoMotion> motions)
        {
            Echo.Apply(motions);
            return this;
        }

        /// <inheritdoc cref="Echo.For{TItem}(TItem)"/>
        /// \copydoc Echo.For
        public EchoMotionProxy<TItem2> For<TItem2>(TItem2 item)
            where TItem2 : class
            => new EchoMotionProxy<TItem2>(Echo, item);

        /// <inheritdoc cref="Echo.For{TItem}(TItem[])"/>
        /// \copydoc Echo.For
        public EchoMotionProxyArray<TItem2> For<TItem2>(params TItem2[] elements)
            where TItem2 : class
        {
            return new EchoMotionProxyArray<TItem2>(Echo, elements);
        }

        /// <inheritdoc cref="Echo.ForAll{TItem}(IEnumerable{TItem})"/>
        /// \copydoc Echo.ForAll
        public EchoMotionProxyArray<TItem2> ForAll<TItem2>(IEnumerable<TItem2> elements)
            where TItem2 : class
        {
            return new EchoMotionProxyArray<TItem2>(Echo, elements.ToArray());
        }

        #endregion

        #region Controls

        /// <inheritdoc cref="Echo.Start"/>
        /// \copydoc Echo.Start
        public EchoMotionProxy<TItem> Start()
        {
            Echo.Start();
            return this;
        }

        /// <inheritdoc cref="Echo.StartAsync(CancellationToken?)"/>
        /// \copydoc Echo.StartAsync(CancellationToken?)
        public async Task<EchoMotionProxy<TItem>> StartAsync(CancellationToken? token = null)
        {
            await Echo.StartAsync(token);
            return this;
        }

        /// <inheritdoc cref="Echo.Resume"/>
        /// \copydoc Echo.Resume
        public EchoMotionProxy<TItem> Resume()
        {
            Echo.Resume();
            return this;
        }

        /// <inheritdoc cref="Echo.Pause"/>
        /// \copydoc Echo.Pause
        public EchoMotionProxy<TItem> Pause()
        {
            Echo.Pause();
            return this;
        }

        /// <inheritdoc cref="Echo.Stop(bool)"/>
        /// \copydoc Echo.Stop
        public EchoMotionProxy<TItem> Stop(bool triggerOnCompleted = false)
        {
            Echo.Stop(triggerOnCompleted);
            return this;
        }

        /// <inheritdoc cref="Echo.Reset"/>
        /// \copydoc Echo.Reset
        public EchoMotionProxy<TItem> Reset()
        {
            Echo.Reset();
            return this;
        }

        /// <inheritdoc cref="AbstractAnimation.AsAsync"/>
        /// \copydoc AbstractAnimation.AsAsync
        public async Task AsAsync()
        {
            await Echo.AsAsync();
        }

        #endregion

        #region Events

        /// <inheritdoc />
        /// \copydoc IFluentEchoEventable.OnStarting
        public EchoMotionProxy<TItem> OnStarting(Action callback)
        {
            Echo.OnStarting(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnStarted
        public EchoMotionProxy<TItem> OnStarted(Action callback)
        {
            Echo.OnStarted(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentEchoEventable.OnUpdating
        public EchoMotionProxy<TItem> OnUpdating(Action<float> callback)
        {
            Echo.OnUpdating(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnUpdated
        public EchoMotionProxy<TItem> OnUpdated(Action<float> callback)
        {
            Echo.OnUpdated(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnLoopStarted
        public EchoMotionProxy<TItem> OnLoopStarted(Action<int?> callback)
        {
            Echo.OnLoopStarted(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnLoopCompleted
        public EchoMotionProxy<TItem> OnLoopCompleted(Action<int?> callback)
        {
            Echo.OnLoopCompleted(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentEchoEventable.OnCompleting
        public EchoMotionProxy<TItem> OnCompleting(Action callback)
        {
            Echo.OnCompleting(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnCompleted
        public EchoMotionProxy<TItem> OnCompleted(Action callback)
        {
            Echo.OnCompleted(callback);
            return this;
        }

        #endregion

        #region Options

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetName
        public EchoMotionProxy<TItem> SetName(string name)
        {
            Echo.SetName(name);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetUpdateType
        public EchoMotionProxy<TItem> SetUpdateType(UpdateType updateType)
        {
            Echo.SetUpdateType(updateType);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetAutoStart
        public EchoMotionProxy<TItem> SetAutoStart(bool autoStart)
        {
            Echo.SetAutoStart(autoStart);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetSkipFrames
        public EchoMotionProxy<TItem> SetSkipFrames(int frames)
        {
            Echo.SetSkipFrames(frames);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetDelay
        public EchoMotionProxy<TItem> SetDelay(float time)
        {
            Echo.SetDelay(time);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetDelayUntil
        public EchoMotionProxy<TItem> SetDelayUntil(Func<bool> callback)
        {
            Echo.SetDelayUntil(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetLoopCount
        public EchoMotionProxy<TItem> SetLoopCount(int? loopCount)
        {
            Echo.SetLoopCount(loopCount);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetTimeScale
        public EchoMotionProxy<TItem> SetTimeScale(float timeScale)
        {
            Echo.SetTimeScale(timeScale);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentEchoOptionable.SetTimeout
        public EchoMotionProxy<TItem> SetTimeout(float? timeout)
        {
            Echo.SetTimeout(timeout);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentEchoOptionable.SetStopCondition
        public EchoMotionProxy<TItem> SetStopCondition(Func<float, bool> stopCondition)
        {
            Echo.SetStopCondition(stopCondition);
            return this;
        }

        #endregion
    }
}
