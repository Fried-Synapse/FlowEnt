using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Inputs
{
    /// <summary>
    /// Provides a callback for when Input mouse button is triggered.
    /// </summary>
    public class MouseButtonMotion : InputIntMotion
    {
        public MouseButtonMotion(int button, Action<float> callback) : base(button, callback)
        {
        }

        protected override Func<int, bool> InputCall => Input.GetMouseButton;
    }
}