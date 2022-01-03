using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Inputs
{
    /// <summary>
    /// Provides a callback for when Input mouse button down is triggered.
    /// </summary>
    public class MouseButtonDownMotion : InputIntMotion
    {
        public MouseButtonDownMotion(int button, Action<float> callback) : base(button, callback)
        {
        }

        protected override Func<int, bool> InputCall => Input.GetMouseButtonDown;
    }
}