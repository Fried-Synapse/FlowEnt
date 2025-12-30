using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Inputs
{
    /// <summary>
    /// Provides a callback for when Input button is triggered.
    /// </summary>
    public class ButtonMotion : InputStringMotion
    {
        public ButtonMotion(string buttonName, Action<float> callback) : base(buttonName, callback)
        {
        }

        protected override Func<string, bool> InputCall => Input.GetButton;
    }
}