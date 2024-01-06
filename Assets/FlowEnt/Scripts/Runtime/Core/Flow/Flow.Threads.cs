using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriedSynapse.FlowEnt
{
    public partial class Flow
    {
        #region Utils

        private void InitUpdatable(ref AbstractUpdatable updatable)
        {
            if (updatable is AbstractAnimation animation)
            {
                if (animation.PlayState != PlayState.Building)
                {
                    throw new AnimationException(this, ErrorAnimationAlreadyStarted);
                }

                if (animation.AutoStart)
                {
                    animation.CancelAutoStart();
                }
            }

            updatable ??= new EmptyUpdatable();
            updatable.updateController = this;
        }

        private void AddOrQueue(AbstractUpdatableWrapper updatableWrapper, bool forceAdd = false)
        {
            if (lastQueuedUpdatableWrapper == null || forceAdd)
            {
                lastQueuedUpdatableWrapper = updatableWrapper;
                updatableWrappersQueue.Add(lastQueuedUpdatableWrapper);
            }
            else
            {
                lastQueuedUpdatableWrapper.next = updatableWrapper;
                lastQueuedUpdatableWrapper = updatableWrapper;
            }
        }

        private void AddOrQueue(AbstractUpdatable updatable, bool forceAdd = false)
        {
            InitUpdatable(ref updatable);
            AddOrQueue(new UpdatableWrapperDirect(updatable), forceAdd);
        }

        private void AddOrQueue(Func<AbstractUpdatable> updatableBuilder, bool forceAdd = false)
        {
            AbstractUpdatable createUpdatable()
            {
                AbstractUpdatable updatable = updatableBuilder();
                InitUpdatable(ref updatable);
                return updatable;
            }

            AddOrQueue(new UpdatableWrapperCallback(createUpdatable), forceAdd);
        }

        #endregion

        #region Queue

        /// <summary>
        /// Queues an animation in the current sequence.
        /// </summary>
        /// <param name="animation"></param>
        public Flow Queue(AbstractAnimation animation)
        {
            AddOrQueue(animation);
            return this;
        }

        /// <inheritdoc cref="Queue(AbstractAnimation)" />
        /// \copydoc At
        /// <param name="builder">Callback to build the animation.</param>
        public Flow Queue(Func<Tween, AbstractAnimation> builder)
            => Queue(builder(new Tween()));

        /// <inheritdoc cref="Queue(Func{Tween, AbstractAnimation})" />
        /// \copydoc Queue
        public Flow Queue(Func<Echo, AbstractAnimation> builder)
            => Queue(builder(new Echo()));

        /// <inheritdoc cref="Queue(Func{Tween, AbstractAnimation})" />
        /// \copydoc Queue
        public Flow Queue(Func<Flow, AbstractAnimation> builder)
            => Queue(builder(new Flow()));

        /// <summary>
        /// Queues an awaiter in the current sequence.
        /// </summary>
        /// <param name="flowAwaiter"></param>
        public Flow QueueAwaiter(AbstractFlowAwaiter flowAwaiter)
        {
            AddOrQueue(flowAwaiter);
            return this;
        }

        /// <summary>
        /// Queues a delay in the current sequence.
        /// </summary>
        /// <param name="delay"></param>
        public Flow QueueDelay(float delay)
            => QueueAwaiter(new DelayFlowAwaiter(delay));

        /// <summary>
        /// Queues a callback as an awaiter in the current sequence.
        /// </summary>
        /// <param name="waitCondition"></param>
        public Flow QueueAwaiter(Func<bool> waitCondition)
            => QueueAwaiter(new CallbackFlowAwaiter(waitCondition));

        /// <summary>
        /// Queues a task as an awaiter in the current sequence.
        /// </summary>
        /// <param name="task"></param>
        public Flow QueueAwaiter(Task task)
            => QueueAwaiter(new TaskFlowAwaiter(task));

        #endregion

        #region QueueDeferred

        /// <summary>
        /// Queues a callback for the animation builder in the current sequence.
        /// This is useful when you need to create an animation after the current flow has started.
        /// </summary>
        /// <param name="builder">Callback to build the animation.</param>
        public Flow QueueDeferred(Func<AbstractAnimation> builder)
        {
            AddOrQueue(builder);
            return this;
        }

        /// <inheritdoc cref="QueueDeferred(Func{AbstractAnimation})" />
        /// \copydoc QueueDeferred
        public Flow QueueDeferred(Func<Tween, AbstractAnimation> builder)
            => QueueDeferred(() => builder(new Tween()));

        /// <inheritdoc cref="QueueDeferred(Func{AbstractAnimation})" />
        /// \copydoc QueueDeferred
        public Flow QueueDeferred(Func<Echo, AbstractAnimation> builder)
            => QueueDeferred(() => builder(new Echo()));

        /// <inheritdoc cref="QueueDeferred(Func{AbstractAnimation})" />
        /// \copydoc QueueDeferred
        public Flow QueueDeferred(Func<Flow, AbstractAnimation> builder)
            => QueueDeferred(() => builder(new Flow()));

        #endregion

        #region At

        /// <summary>
        /// Starts a new sequence at the <paramref name="timeIndex"/> provided.
        /// </summary>
        /// <param name="timeIndex">Time index for the sequence to start.</param>
        /// <param name="animation"></param>
        public Flow At(float timeIndex, AbstractAnimation animation)
        {
            if (timeIndex < 0)
            {
                throw new ArgumentException($"Time index cannot be negative. Value: {timeIndex}");
            }

            if (timeIndex == 0)
            {
                AddOrQueue(animation, true);
            }
            else
            {
                AddOrQueue(new DelayFlowAwaiter(timeIndex), true);
                AddOrQueue(animation);
            }

            return this;
        }

        /// <inheritdoc cref="At(float, AbstractAnimation)" />
        /// \copydoc At
        /// <param name="timeIndex">Time index for the sequence to start.</param>
        /// <param name="builder">Callback to build the animation.</param>
        public Flow At(float timeIndex, Func<Tween, AbstractAnimation> builder)
            => At(timeIndex, builder(new Tween()));

        /// <inheritdoc cref="At(float, Func{Tween, AbstractAnimation})" />
        /// \copydoc At
        public Flow At(float timeIndex, Func<Echo, AbstractAnimation> builder)
            => At(timeIndex, builder(new Echo()));

        /// <inheritdoc cref="At(float, Func{Tween, AbstractAnimation})" />
        /// \copydoc At
        public Flow At(float timeIndex, Func<Flow, AbstractAnimation> builder)
            => At(timeIndex, builder(new Flow()));

        /// <inheritdoc cref="At(float, Func{Tween, AbstractAnimation})" />
        /// \copydoc At
        public Flow At(float timeIndex, List<AbstractAnimation> animations)
        {
            if (animations.Count == 0)
            {
                return this;
            }

            At(timeIndex, animations[0]);
            for (int i = 1; i < animations.Count; i++)
            {
                Queue(animations[i]);
            }

            return this;
        }

        #endregion

        #region AtDeferred

        /// <summary>
        /// Starts a new sequence at the <paramref name="timeIndex"/> provided and uses the callback for the animation builder to add an animation to the sequence.
        /// This is useful when you need to create an animation after the current flow has started.
        /// </summary>
        /// <param name="timeIndex">Time index for the sequence to start.</param>
        /// <param name="builder">Callback to build the animation.</param>
        public Flow AtDeferred(float timeIndex, Func<AbstractAnimation> builder)
        {
            if (timeIndex < 0)
            {
                throw new ArgumentException($"Time index cannot be negative. Value: {timeIndex}");
            }

            if (timeIndex == 0)
            {
                AddOrQueue(builder, true);
            }
            else
            {
                AddOrQueue(new DelayFlowAwaiter(timeIndex), true);
                AddOrQueue(builder);
            }

            return this;
        }

        /// <inheritdoc cref="AtDeferred(float, Func{AbstractAnimation})" />
        /// \copydoc AtDeferred
        public Flow AtDeferred(float timeIndex, Func<Tween, AbstractAnimation> builder)
            => AtDeferred(timeIndex, () => builder(new Tween()));

        /// <inheritdoc cref="AtDeferred(float, Func{AbstractAnimation})" />
        /// \copydoc AtDeferred
        public Flow AtDeferred(float timeIndex, Func<Echo, AbstractAnimation> builder)
            => AtDeferred(timeIndex, () => builder(new Echo()));

        /// <inheritdoc cref="AtDeferred(float, Func{AbstractAnimation})" />
        /// \copydoc AtDeferred
        public Flow AtDeferred(float timeIndex, Func<Flow, AbstractAnimation> builder)
            => AtDeferred(timeIndex, () => builder(new Flow()));

        #endregion
    }
}