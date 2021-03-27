using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FlowEnt
{
    public class Flow : FlowEntObject
    {
        private Flow()
        {
            Concurrent();
        }

        public PlayState PlayState { get; private set; }

        #region Reference Properties

        internal List<Thread> Threads { get; } = new List<Thread>();

        #endregion

        #region Events

        private Action OnStartCallback { get; set; }
        private Action OnCompleteCallback { get; set; }

        #endregion

        #region Settings Properties

        private int LoopCount { get; set; }
        private bool HasInfiniteTweenLoops
        {
            get
            {
                for (int i = 0; i < Threads.Count; i++)
                {
                    if (Threads[i].HasInfiniteTweenLoops)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        #endregion

        #region Internal Properties

        private Thread QueueingThread { get; set; }
        private List<Thread> PlayingThreads { get; set; }

        #endregion

        #region Build

        public static Flow Create()
            => new Flow();

        public Tween Enqueue(float time)
            => QueueingThread.Enqueue(time);

        public Flow Concurrent()
        {
            QueueingThread = new Thread(this);
            Threads.Add(QueueingThread);
            return this;
        }

        public Flow OnStart(Action callback)
        {
            OnStartCallback += callback;
            return this;
        }

        public Flow OnComplete(Action callback)
        {
            OnCompleteCallback += callback;
            return this;
        }

        #endregion

        #region Actions

        /// <summary>
        /// Plays the whole flow with the number of specified loops.
        /// </summary>
        /// <param name="loopCount">The number of loops to be played. If number smaller than 0 it'll loop forever.</param>
        /// <returns>The current flow.</returns>
        public Flow Play(int loopCount = 1)
        {
            if (PlayState == PlayState.Playing)
            {
                throw new FlowEntException(this, "Flow already playing.");
            }

            if (PlayState == PlayState.Finished)
            {
                throw new FlowEntException(this, "Flow already finished.");
            }

            if (loopCount == 0)
            {
                throw new FlowEntException(this, "Flow needs at least one loop to play.");
            }

            if (loopCount > 1 && HasInfiniteTweenLoops)
            {
                throw new FlowEntException(this, "Flow contains an infinite loop and won't be able to loop.");
            }

            PlayState = PlayState.Playing;
            LoopCount = loopCount;
            PlayingThreads = new List<Thread>(Threads);

            OnStartCallback?.Invoke();
            for (int i = 0; i < Threads.Count; i++)
            {
                Thread thread = Threads[i];
                thread.Init();
                thread.OnComplete += (overdraft) =>
                {
                    PlayingThreads.Remove(thread);

                    if (PlayingThreads.Count == 0)
                    {
                        LoopFinished(overdraft);
                    }
                };
            }

            Update(0);
            FlowEntController.Instance.SubscribeToUpdate(Update);
            return this;
        }

        public async Task<Flow> PlayAsync(int loopCount = 1)
        {
            Play(loopCount);
            await new AwaitableFlow(this);
            return this;
        }

        public Flow Pause()
        {
            if (PlayState != PlayState.Playing)
            {
                throw new FlowEntException(this, "Only playing flows can be paused.");
            }

            PlayState = PlayState.Paused;
            FlowEntController.Instance.UnsubscribeFromUpdate(Update);
            return this;
        }

        public Flow Resume()
        {
            if (PlayState != PlayState.Paused)
            {
                throw new FlowEntException(this, "Only paused flows can be resumed.");
            }

            PlayState = PlayState.Playing;
            FlowEntController.Instance.SubscribeToUpdate(Update);
            return this;
        }

        public Flow Stop()
        {
            if (PlayState == PlayState.Finished)
            {
                throw new FlowEntException(this, "Flow already finished.");
            }

            Finished();
            return this;
        }

        //TODO
        //public async Task WaitAsync()
        //{
        //    while (PlayState != PlayState.Finished)
        //    {
        //        Debug.Log($"Wait: {PlayState}");
        //        await Task.Delay((int)(Time.deltaTime * 1000));
        //    }
        //}

        #endregion

        #region Lifecycle

        private void Update(float deltaTime)
        {
            for (int i = 0; i < PlayingThreads.Count; i++)
            {
                PlayingThreads[i].Update(deltaTime);
            }
        }

        private void LoopFinished(float overdraft)
        {
            bool hasAnotherLoop;
            if (LoopCount < 0)
            {
                hasAnotherLoop = true;
            }
            else
            {
                LoopCount--;
                hasAnotherLoop = LoopCount != 0;
            }

            if (hasAnotherLoop)
            {
                ResetLoop();
                Update(overdraft);
            }
            else
            {
                Finished();
            }
        }

        private void ResetLoop()
        {
            PlayingThreads = new List<Thread>(Threads);
            for (int i = 0; i < PlayingThreads.Count; i++)
            {
                PlayingThreads[i].Reset();
                PlayingThreads[i].Init();
            }
        }

        private void Finished()
        {
            PlayState = PlayState.Finished;
            OnCompleteCallback?.Invoke();
            FlowEntController.Instance.UnsubscribeFromUpdate(Update);
        }

        #endregion
    }

    internal class AwaitableFlow
    {
        public AwaitableFlow(Flow flow)
        {
            Flow = flow;
        }

        public Flow Flow { get; }
        public FlowAwaiter GetAwaiter()
            => new FlowAwaiter(Flow);
    }

    internal class FlowAwaiter : INotifyCompletion
    {
        public FlowAwaiter(Flow flow)
        {
            Flow = flow;
            Flow.OnComplete(() => OnCompletedCallback.Invoke());
        }

        public Flow Flow { get; }
        public bool IsCompleted => Flow.PlayState == PlayState.Finished;
        private Action OnCompletedCallback { get; set; }

        public Flow GetResult()
            => Flow;

        public void OnCompleted(Action continuation)
        {
            OnCompletedCallback = continuation;
        }
    }
}