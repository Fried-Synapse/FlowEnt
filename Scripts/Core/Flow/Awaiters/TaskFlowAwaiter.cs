using System.Threading.Tasks;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
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
