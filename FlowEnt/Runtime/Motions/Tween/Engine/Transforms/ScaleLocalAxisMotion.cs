using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.localScale" /> value by axis.
    /// </summary>
    public class ScaleLocalAxisMotion : AbstractAxisMotion<Transform>
    {
        [Serializable]
        public class ValueBuilder : AbstractAxisValueBuilder
        {
            public override AbstractTweenMotion Build()
                => new ScaleLocalAxisMotion(item, axis, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractAxisFromToBuilder
        {
            public override AbstractTweenMotion Build()
                => new ScaleLocalAxisMotion(item, axis, From, to);
        }

        public ScaleLocalAxisMotion(Transform item, Axis axis, float value) : base(item, axis, value)
        {
        }

        public ScaleLocalAxisMotion(Transform item, Axis axis, float? from, float to) : base(item, axis, from, to,
            item == null ? null : item.localScale)
        {
        }

        protected override Vector3 Target { get => item.localScale; set => item.localScale = value; }

        protected override Vector3 GetFrom() => item.localScale;
    }
}