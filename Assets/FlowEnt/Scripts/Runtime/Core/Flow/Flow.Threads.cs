
using System;
using System.Threading.Tasks;

namespace FriedSynapse.FlowEnt
{
    public partial class Flow
    {
        #region Utils

        private void InitUpdatable(AbstractUpdatable updatable)
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
            InitUpdatable(updatable);
            AddOrQueue(new UpdatableWrapperDirect(updatable), forceAdd);
        }

        private void AddOrQueue(Func<AbstractUpdatable> updatableBuilder, bool forceAdd = false)
        {
            AbstractUpdatable createUpdatable()
            {
                AbstractUpdatable updatable = updatableBuilder();
                InitUpdatable(updatable);
                return updatable;
            }

            AddOrQueue(new UpdatableWrapperCallback(createUpdatable), forceAdd);
        }

        /// <summary>
        /// Executes the <paramref name="onConditionTrue"/> if <paramref name="condition"/> returns true.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="onConditionTrue">The callback. The flow object represents the current flow.</param>
        public Flow Conditional(Func<bool> condition, Action<Flow> onConditionTrue)
        {
            if (condition?.Invoke() == true)
            {
                onConditionTrue?.Invoke(this);
            }

            return this;
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

        /// <summary>
        /// Creates a tween and provides a context to build it and then queues the built animation in the current sequence.
        /// </summary>
        /// <param name="tweenBuilder"></param>
        public Flow Queue(Func<Tween, Tween> tweenBuilder)
            => Queue(tweenBuilder(new Tween()));

        /// <summary>
        /// Creates a flow and provides a context to build it and then queues the built animation in the current sequence.
        /// </summary>
        /// <param name="flowBuilder"></param>
        public Flow Queue(Func<Flow, Flow> flowBuilder)
            => Queue(flowBuilder(new Flow()));

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
        /// <param name="animationBuilder"></param>
        public Flow QueueDeferred(Func<AbstractAnimation> animationBuilder)
        {
            AddOrQueue(animationBuilder);
            return this;
        }

        /// <summary>
        /// Queues a callback, that creates a tween and provides a context to build it, in the current sequence.
        /// This is useful when you need to create an animation after the current flow has started.
        /// </summary>
        /// <param name="tweenBuilder"></param>
        public Flow QueueDeferred(Func<Tween, Tween> tweenBuilder)
            => QueueDeferred(() => tweenBuilder(new Tween()));

        /// <summary>
        /// Queues a callback, that creates a flow and provides a context to build it, in the current sequence.
        /// This is useful when you need to create an animation after the current flow has started.
        /// </summary>
        /// <param name="flowBuilder"></param>
        public Flow QueueDeferred(Func<Flow, Flow> flowBuilder)
            => QueueDeferred(() => flowBuilder(new Flow()));

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

        /// <summary>
        /// Creates a tween and provides a context to build it and then starts a new sequence at the <paramref name="timeIndex"/> provided.
        /// </summary>
        /// <param name="timeIndex">Time index for the sequence to start.</param>
        /// <param name="tweenBuilder"></param>
        public Flow At(float timeIndex, Func<Tween, Tween> tweenBuilder)
            => At(timeIndex, tweenBuilder(new Tween(new TweenOptions())));

        /// <summary>
        /// Creates a flow and provides a context to build it and then starts a new sequence at the <paramref name="timeIndex"/> provided.
        /// </summary>
        /// <param name="timeIndex">Time index for the sequence to start.</param>
        /// <param name="flowBuilder"></param>
        public Flow At(float timeIndex, Func<Flow, Flow> flowBuilder)
            => At(timeIndex, flowBuilder(new Flow()));

        #endregion

        #region AtDeferred

        /// <summary>
        /// Starts a new sequence at the <paramref name="timeIndex"/> provided and uses the callback for the animation builder to add an animation to the sequence.
        /// This is useful when you need to create an animation after the current flow has started.
        /// </summary>
        /// <param name="timeIndex">Time index for the sequence to start.</param>
        /// <param name="animationBuilder"></param>
        public Flow AtDeferred(float timeIndex, Func<AbstractAnimation> animationBuilder)
        {
            if (timeIndex < 0)
            {
                throw new ArgumentException($"Time index cannot be negative. Value: {timeIndex}");
            }

            if (timeIndex == 0)
            {
                AddOrQueue(animationBuilder, true);
            }
            else
            {
                AddOrQueue(new DelayFlowAwaiter(timeIndex), true);
                AddOrQueue(animationBuilder);
            }

            return this;
        }

        /// <summary>
        /// Starts a new sequence at the <paramref name="timeIndex"/> provided and uses the callback for the animation builder to add an animation to the sequence.
        /// This is useful when you need to create an animation after the current flow has started.
        /// </summary>
        /// <param name="timeIndex">Time index for the sequence to start.</param>
        /// <param name="tweenBuilder"></param>
        public Flow AtDeferred(float timeIndex, Func<Tween, Tween> tweenBuilder)
            => AtDeferred(timeIndex, () => tweenBuilder(new Tween()));

        /// <summary>
        /// Starts a new sequence at the <paramref name="timeIndex"/> provided and uses the callback for the animation builder to add an animation to the sequence.
        /// This is useful when you need to create an animation after the current flow has started.
        /// </summary>
        /// <param name="timeIndex">Time index for the sequence to start.</param>
        /// <param name="flowBuilder"></param>
        public Flow AtDeferred(float timeIndex, Func<Flow, Flow> flowBuilder)
            => AtDeferred(timeIndex, () => flowBuilder(new Flow()));

        #endregion
    }
}
