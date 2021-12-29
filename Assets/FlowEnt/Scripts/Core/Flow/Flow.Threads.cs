
using System;
using System.Threading.Tasks;

namespace FriedSynapse.FlowEnt
{
    public partial class Flow
    {
        #region Utils

        private void InitAnimation(AbstractAnimation animation)
        {
            if (animation.PlayState != PlayState.Building)
            {
                throw new AnimationException(this, ErrorAnimationAlreadyStarted);
            }

            if (animation.AutoStart)
            {
                animation.CancelAutoStart();
            }
            animation.updateController = this;
        }

        private void AddOrQueue(AbstractUpdatable updatable)
        {
            if (lastQueuedUpdatableWrapper == null)
            {
                lastQueuedUpdatableWrapper = new UpdatableWrapper(updatable, updatableWrappersQueue.Count, 0);
                updatableWrappersQueue.Add(lastQueuedUpdatableWrapper);
            }
            else
            {
                UpdatableWrapper animationWrapper = new UpdatableWrapper(updatable, lastQueuedUpdatableWrapper.index);
                lastQueuedUpdatableWrapper.next = animationWrapper;
                lastQueuedUpdatableWrapper = animationWrapper;
            }
        }

        private void AddOrQueue(Func<AbstractUpdatable> updatableGetter)
        {
            if (lastQueuedUpdatableWrapper == null)
            {
                lastQueuedUpdatableWrapper = new UpdatableWrapper(updatableGetter, updatableWrappersQueue.Count, 0);
                updatableWrappersQueue.Add(lastQueuedUpdatableWrapper);
            }
            else
            {
                UpdatableWrapper animationWrapper = new UpdatableWrapper(updatableGetter, lastQueuedUpdatableWrapper.index);
                lastQueuedUpdatableWrapper.next = animationWrapper;
                lastQueuedUpdatableWrapper = animationWrapper;
            }
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
            InitAnimation(animation);

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
        /// Queues a delay in the current sequence.
        /// </summary>
        /// <param name="delay"></param>
        public Flow QueueDelay(float delay)
            => Queue(new Tween(delay));

        /// <summary>
        /// Queues an awaiter in the current sequence.
        /// </summary>
        /// <param name="flowAwaiter"></param>
        public Flow QueueAwaiter(AbstractFlowAwaiter flowAwaiter)
        {
            flowAwaiter.updateController = this;

            AddOrQueue(flowAwaiter);

            return this;
        }

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
            AbstractAnimation createAnimation()
            {
                AbstractAnimation animation = animationBuilder();

                InitAnimation(animation);

                return animation;
            }

            AddOrQueue(createAnimation);

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

            InitAnimation(animation);

            lastQueuedUpdatableWrapper = new UpdatableWrapper(animation, updatableWrappersQueue.Count, timeIndex);
            updatableWrappersQueue.Add(lastQueuedUpdatableWrapper);

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
            AbstractAnimation createAnimation()
            {
                AbstractAnimation animation = animationBuilder();

                if (timeIndex < 0)
                {
                    throw new ArgumentException($"Time index cannot be negative. Value: {timeIndex}");
                }

                InitAnimation(animation);

                return animation;
            }

            lastQueuedUpdatableWrapper = new UpdatableWrapper(createAnimation, updatableWrappersQueue.Count, timeIndex);
            updatableWrappersQueue.Add(lastQueuedUpdatableWrapper);

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
