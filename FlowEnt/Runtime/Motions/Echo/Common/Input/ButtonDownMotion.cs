using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Inputs
{
    /// <summary>
    /// Provides a callback for when Input button down is triggered.
    /// </summary>
    public class ButtonDownMotion : InputStringMotion
    {
        public ButtonDownMotion(string buttonName, Action<float> callback) : base(buttonName, callback)
        {
        }

        protected override Func<string, bool> InputCall => Input.GetButtonDown;
    }
}