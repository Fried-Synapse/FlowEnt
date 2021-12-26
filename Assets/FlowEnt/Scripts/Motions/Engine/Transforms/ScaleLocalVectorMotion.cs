using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.localScale" /> value.
    /// </summary>
    /// <typeparam name="TTransform"></typeparam>
    public class ScaleLocalVectorMotion<TTransform> : AbstractVector3Motion<TTransform>
        where TTransform : Transform
    {
        public ScaleLocalVectorMotion(TTransform item, Vector3 value) : base(item, value)
        {
        }

        public ScaleLocalVectorMotion(TTransform item, Vector3? from, Vector3 to) : base(item, from, to)
        {
        }

        protected override Vector3 GetFrom() => item.localScale;
        protected override Vector3 GetTo(Vector3 from, Vector3 value) => Vector3.Scale(from, value);
        protected override void SetValue(Vector3 value) => item.localScale = value;
    }
}