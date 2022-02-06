using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Inputs
{
    /// <summary>
    /// Provides a callback for when Input key down is triggered.
    /// </summary>
    public class KeyDownMotion : InputKeyCodeMotion
    {
        public KeyDownMotion(KeyCode keyCode, Action<float> callback) : base(keyCode, callback)
        {
        }

        protected override Func<KeyCode, bool> InputCall => Input.GetKeyDown;
    }
}