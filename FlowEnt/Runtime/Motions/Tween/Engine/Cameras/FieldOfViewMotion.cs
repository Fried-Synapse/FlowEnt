using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Cameras
{
    /// <summary>
    /// Lerps the <see cref="Camera.fieldOfView" /> value.
    /// </summary>
    public class FieldOfViewMotion : AbstractFloatMotion<Camera>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override AbstractTweenMotion Build()
                => new FieldOfViewMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override AbstractTweenMotion Build()
                => new FieldOfViewMotion(item, From, to);
        }

        public FieldOfViewMotion(Camera item, float value) : base(item, value)
        {
        }

        public FieldOfViewMotion(Camera item, float? from, float to) : base(item, from, to)
        {
        }

        protected override float GetFrom() => item.fieldOfView;
        protected override void SetValue(float value) => item.fieldOfView = value;
    }
}