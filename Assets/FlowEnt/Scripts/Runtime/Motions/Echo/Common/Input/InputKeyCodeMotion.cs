using System;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Inputs
{
    public abstract class InputKeyCodeMotion : AbstractEchoMotion
    {
        protected InputKeyCodeMotion(KeyCode keyCode, Action<float> callback)
        {
            this.keyCode = keyCode;
            this.callback = callback;
        }

        private readonly KeyCode keyCode;
        private readonly Action<float> callback;
        protected abstract Func<KeyCode, bool> InputCall { get; }

        public override void OnUpdate(float deltaTime)
        {
            if (InputCall(keyCode))
            {
                callback?.Invoke(deltaTime);
            }
        }
    }
}