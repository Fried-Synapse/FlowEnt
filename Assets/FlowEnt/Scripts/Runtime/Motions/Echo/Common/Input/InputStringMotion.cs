using System;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;

namespace FriedSynapse.FlowEnt.Motions.Echo.Inputs
{
    public abstract class InputStringMotion : AbstractEchoMotion
    {
        protected InputStringMotion(string name, Action<float> callback)
        {
            this.name = name;
            this.callback = callback;
        }

        private readonly string name;
        private readonly Action<float> callback;
        protected abstract Func<string, bool> InputCall { get; }

        public override void OnUpdate(float deltaTime)
        {
            if (InputCall(name))
            {
                callback?.Invoke(deltaTime);
            }
        }
    }
}