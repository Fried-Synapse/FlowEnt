using System;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;

namespace FriedSynapse.FlowEnt.Motions.Echo.Inputs
{
    public abstract class InputIntMotion : AbstractEchoMotion
    {
        protected InputIntMotion(int button, Action<float> callback)
        {
            this.button = button;
            this.callback = callback;
        }

        private readonly int button;
        private readonly Action<float> callback;
        protected abstract Func<int, bool> InputCall { get; }

        public override void OnUpdate(float deltaTime)
        {
            if (InputCall(button))
            {
                callback?.Invoke(deltaTime);
            }
        }
    }
}