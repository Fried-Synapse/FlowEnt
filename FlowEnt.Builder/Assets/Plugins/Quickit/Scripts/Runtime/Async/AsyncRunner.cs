using System;
using System.Threading.Tasks;

namespace FriedSynapse.Quickit
{
    internal class AsyncRunner<TTargetTask, TControlTask>
        where TTargetTask : Task
        where TControlTask : Task
    {
        internal AsyncRunner(TTargetTask targetTask, Func<TTargetTask, TControlTask> runnerCallback)
        {
            this.targetTask = targetTask;
            this.runnerCallback = runnerCallback;
        }
        private readonly TTargetTask targetTask;
        private readonly Func<TTargetTask, TControlTask> runnerCallback;
        internal void Run() => _ = RunParallel();
        private async Task RunParallel()
        {
            try
            {
                await runnerCallback?.Invoke(targetTask);
            }
            catch (Exception ex)
            {
                AsyncQuickitContext.Instance.ExceptionsToBePrinted.Enqueue(ex);
            }
        }
    }

    public static class AsyncRunnerExtensions
    {
        /// <summary>
        /// Runs the given task in parallel.
        /// </summary>
        /// <param name="task"></param>
        public static void RunParallel(this Task task)
            => new AsyncRunner<Task, Task>(task, async internalTask => await internalTask).Run();

        /// <summary>
        /// Runs the given task in parallel and provides a callback when that task is completed.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="onComplete"></param>
        public static void RunParallel(this Task task, Action onComplete)
            => new AsyncRunner<Task, Task>(task, async internalTask => { await internalTask; onComplete?.Invoke(); }).Run();

        /// <summary>
        /// Runs the given task in parallel and provides an async callback when that task is completed.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="onComplete"></param>
        public static void RunParallel(this Task task, Func<Task> onComplete)
            => new AsyncRunner<Task, Task>(task, async internalTask => { await internalTask; await onComplete.Invoke(); }).Run();

        /// <summary>
        /// Runs the given task in parallel and provides a context to run that task as you wish.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="runnerCallback"></param>
        public static void RunParallel(this Task task, Func<Task, Task> runnerCallback)
            => new AsyncRunner<Task, Task>(task, runnerCallback).Run();

        /// <summary>
        /// Runs the given task in parallel.
        /// </summary>
        /// <param name="task"></param>
        /// <typeparam name="TResult"></typeparam>
        public static void RunParallel<TResult>(this Task<TResult> task)
            => new AsyncRunner<Task<TResult>, Task>(task, async internalTask => await internalTask).Run();

        /// <summary>
        /// Runs the given task in parallel and provides a callback when that task is completed.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="onComplete"></param>
        /// <typeparam name="TResult"></typeparam>
        public static void RunParallel<TResult>(this Task<TResult> task, Action onComplete)
            => new AsyncRunner<Task<TResult>, Task>(task, async internalTask => { await internalTask; onComplete?.Invoke(); }).Run();

        /// <summary>
        /// Runs the given task in parallel and provides a callback with the result when that task is completed.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="onComplete"></param>
        /// <typeparam name="TResult"></typeparam>
        public static void RunParallel<TResult>(this Task<TResult> task, Action<TResult> onComplete)
            => new AsyncRunner<Task<TResult>, Task>(task, async internalTask => onComplete?.Invoke(await internalTask)).Run();

        /// <summary>
        /// Runs the given task in parallel and provides an async callback when that task is completed.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="onComplete"></param>
        /// <typeparam name="TResult"></typeparam>
        public static void RunParallel<TResult>(this Task<TResult> task, Func<Task> onComplete)
            => new AsyncRunner<Task<TResult>, Task>(task, async internalTask => { await internalTask; await onComplete?.Invoke(); }).Run();

        /// <summary>
        /// Runs the given task in parallel and provides an async callback with the result when that task is completed.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="onComplete"></param>
        /// <typeparam name="TResult"></typeparam>
        public static void RunParallel<TResult>(this Task<TResult> task, Func<TResult, Task> onComplete)
            => new AsyncRunner<Task<TResult>, Task>(task, async internalTask => await onComplete?.Invoke(await internalTask)).Run();

        /// <summary>
        /// Runs the given task in parallel and provides a context to run that task as you wish.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="runnerCallback"></param>
        /// <typeparam name="TResult"></typeparam>
        public static void RunParallel<TResult>(this Task<TResult> task, Func<Task<TResult>, Task> runnerCallback)
            => new AsyncRunner<Task<TResult>, Task>(task, runnerCallback).Run();
    }
}
