using System.Threading.Tasks;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Flow awaiter that waits for a task to finish(regardless if it was successful or not).
    /// </summary>
    public class TaskFlowAwaiter : AbstractFlowAwaiter
    {
        public TaskFlowAwaiter(Task task)
        {
            this.task = task;
        }
        private readonly Task task;

        protected override bool ShouldWait(float deltaTime) => !(task.IsCompleted || task.IsCanceled || task.IsFaulted);
    }
}
