using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Inputs
{
    /// <summary>
    /// Provides a callback for when Input mouse button up is triggered.
    /// </summary>
    public class MouseButtonUpMotion : InputIntMotion
    {
        public MouseButtonUpMotion(int button, Action<float> callback) : base(button, callback)
        {
        }

        protected override Func<int, bool> InputCall => Input.GetMouseButtonUp;
    }
}