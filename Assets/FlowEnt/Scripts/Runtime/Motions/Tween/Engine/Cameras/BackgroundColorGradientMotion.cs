using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Cameras
{
    /// <summary>
    /// Lerps the <see cref="Camera.backgroundColor" /> value using a gradient.
    /// </summary>
    public class BackgroundColorGradientMotion : AbstractColorGradientMotion<Camera>
    {
        [Serializable]
        public class Builder : AbstractGradientBuilder
        {
            public override AbstractTweenMotion Build()
                => new BackgroundColorGradientMotion(item, gradient);
        }

        public BackgroundColorGradientMotion(Camera item, Gradient gradient) : base(item, gradient)
        {
        }

        public override void OnUpdate(float t)
        {
            item.backgroundColor = gradient.Evaluate(t);
        }
    }
}