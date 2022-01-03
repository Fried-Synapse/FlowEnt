using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Inputs
{
    /// <summary>
    /// Provides a callback for when Input key up is triggered.
    /// </summary>
    public class KeyUpMotion : InputKeyCodeMotion
    {
        public KeyUpMotion(KeyCode keyCode, Action<float> callback) : base(keyCode, callback)
        {
        }

        protected override Func<KeyCode, bool> InputCall => Input.GetKeyUp;
    }
}