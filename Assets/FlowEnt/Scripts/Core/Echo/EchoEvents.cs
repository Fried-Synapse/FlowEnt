using System;

namespace FriedSynapse.FlowEnt
{
    public class EchoEvents : AbstractAnimationEvents,
        IFluentEchoEventable<EchoEvents>
    {
        /// <summary>
        /// The event called when an animation is starting.
        /// </summary>
        public Action OnStartingEvent { get; set; }
        /// <summary>
        /// The event called when an animation is updating.
        /// </summary>
        public Action<float> OnUpdatingEvent { get; set; }
        /// <summary>
        /// The event called when an animation is completing.
        /// </summary>
        public Action OnCompletingEvent { get; set; }

        /// <inheritdoc />
        /// \copydoc IFluentEchoEventable.OnStarting
        public EchoEvents OnStarting(Action callback)
        {
            OnStartingEvent += callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnStarted
        public new EchoEvents OnStarted(Action callback)
        {
            base.OnStarted(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentEchoEventable.OnUpdating
        public EchoEvents OnUpdating(Action<float> callback)
        {
            OnUpdatingEvent += callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnUpdated
        public new EchoEvents OnUpdated(Action<float> callback)
        {
            base.OnUpdated(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnLoopCompleted
        public new EchoEvents OnLoopCompleted(Action<int?> callback)
        {
            base.OnLoopCompleted(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentEchoEventable.OnCompleting
        public EchoEvents OnCompleting(Action callback)
        {
            OnCompletingEvent += callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnCompleted
        public new EchoEvents OnCompleted(Action callback)
        {
            base.OnCompleted(callback);
            return this;
        }
    }
}
