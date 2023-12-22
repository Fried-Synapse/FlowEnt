using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Cameras
{
    /// <summary>
    /// Lerps the <see cref="Camera.backgroundColor" /> value.
    /// </summary>
    public class BackgroundColorMotion : AbstractColorMotion<Camera>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override ITweenMotion Build()
                => new BackgroundColorMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override ITweenMotion Build()
                => new BackgroundColorMotion(item, From, to);
        }

        public BackgroundColorMotion(Camera item, Color value) : base(item, value)
        {
        }

        public BackgroundColorMotion(Camera item, Color? from, Color to) : base(item, from, to)
        {
        }

        protected override Color GetFrom() => item.backgroundColor;
        protected override void SetValue(Color value) => item.backgroundColor = value;
    }
}