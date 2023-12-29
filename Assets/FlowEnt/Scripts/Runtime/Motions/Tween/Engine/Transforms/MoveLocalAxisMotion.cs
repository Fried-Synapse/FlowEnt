using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.localPosition" /> value by axis.
    /// </summary>
    public class MoveLocalAxisMotion : AbstractAxisMotion<Transform>
    {
        [Serializable]
        public class ValueBuilder : AbstractAxisValueBuilder
        {
            public override AbstractTweenMotion Build()
                => new MoveLocalAxisMotion(item, axis, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractAxisFromToBuilder
        {
            public override AbstractTweenMotion Build()
                => new MoveLocalAxisMotion(item, axis, from, to);
        }

        public MoveLocalAxisMotion(Transform item, Axis axis, float value) : base(item, axis, value)
        {
        }

        public MoveLocalAxisMotion(Transform item, Axis axis, float? from, float to) : base(item, axis, from, to,
            item == null ? null : item.localPosition )
        {
        }

        protected override Vector3 Target { get => item.localPosition; set => item.localPosition = value; }

        protected override Vector3 GetFrom() => item.localPosition;
    }
}