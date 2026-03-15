using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Inputs
{
    /// <summary>
    /// Provides a callback for when Input key is triggered.
    /// </summary>
    public class KeyMotion : InputKeyCodeMotion
    {
        public KeyMotion(KeyCode keyCode, Action<float> callback) : base(keyCode, callback)
        {
        }

        protected override Func<KeyCode, bool> InputCall => Input.GetKey;
    }
}