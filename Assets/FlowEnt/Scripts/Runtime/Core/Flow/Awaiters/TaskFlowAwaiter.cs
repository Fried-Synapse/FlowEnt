using System;
using System.Threading.Tasks;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Flow awaiter that waits for a task to finish(regardless if it was successful or not).
    /// </summary>
    public class TaskFlowAwaiter : AbstractFlowAwaiter
    {
        public TaskFlowAwaiter(Func<Task> getTask)
        {
            this.getTask = getTask;
        }

        private Task task;
        private Func<Task> getTask;

        internal override void StartInternal(float deltaTime = 0)
        {
            if (getTask != null)
            {
                task = getTask();
            }

            base.StartInternal(deltaTime);
        }

        protected override bool ShouldWait(float deltaTime) => !(task.IsCompleted || task.IsCanceled || task.IsFaulted);
    }
}