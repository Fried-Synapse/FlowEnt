using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Rigidbodies
{
    /// <summary>
    /// Lerps the <see cref="Rigidbody.position" /> value by axis.
    /// </summary>
    public class MoveAxisMotion : AbstractAxisMotion<Rigidbody>
    {
        [Serializable]
        public class ValueBuilder : AbstractAxisValueBuilder
        {
            public override AbstractTweenMotion Build()
                => new MoveAxisMotion(item, axis, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractAxisFromToBuilder
        {
            public override AbstractTweenMotion Build()
                => new MoveAxisMotion(item, axis, From, to);
        }

        public MoveAxisMotion(Rigidbody item, Axis axis, float value) : base(item, axis, value)
        {
        }

        public MoveAxisMotion(Rigidbody item, Axis axis, float? from, float to) : base(item, axis, from, to,
            item == null ? null : item.position)
        {
        }

        protected override Vector3 Target { get => item.position; set => item.position = value; }

        protected override Vector3 GetFrom() => item.position;
    }
}