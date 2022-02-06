using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Inputs
{
    /// <summary>
    /// Provides a callback for when Input button up is triggered.
    /// </summary>
    public class ButtonUpMotion : InputStringMotion
    {
        public ButtonUpMotion(string buttonName, Action<float> callback) : base(buttonName, callback)
        {
        }

        protected override Func<string, bool> InputCall => Input.GetButtonUp;
    }
}