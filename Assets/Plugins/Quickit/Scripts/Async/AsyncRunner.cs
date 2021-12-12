using System;
using System.Threading.Tasks;

namespace FriedSynapse.Quickit
{
    internal class AsyncRunner<TTask>
        where TTask : Task
    {
        internal AsyncRunner(TTask task, Func<TTask, TTask> runnerCallback)
        {
            this.task = task;
            this.runnerCallback = runnerCallback;
        }
        private readonly TTask task;
        private readonly Func<TTask, TTask> runnerCallback;
        internal void Run() => _ = RunAsync();
        private async Task RunAsync()
        {
            try
            {
                await runnerCallback?.Invoke(task);
            }
            catch (Exception ex)
            {
                AsyncQuickitContext.Instance.ExceptionsToBePrinted.Enqueue(ex);
            }
        }
    }

    public static class AsyncRunnerExtensions
    {
#pragma warning disable RCS1047

        public static void RunAsync(this Task task)
            => new AsyncRunner<Task>(task, async internalTask => await internalTask).Run();

        public static void RunAsync<T>(this Task<T> task)
            => new AsyncRunner<Task<T>>(task, async internalTask => await internalTask).Run();

        public static void RunAsync(this Task task, Func<Task, Task> runnerCallback)
            => new AsyncRunner<Task>(task, runnerCallback).Run();

        public static void RunAsync<T>(this Task<T> task, Func<Task<T>, Task<T>> runnerCallback)
            => new AsyncRunner<Task<T>>(task, runnerCallback).Run();

#pragma warning restore RCS1047
    }
}
