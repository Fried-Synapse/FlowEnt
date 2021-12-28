using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Cameras
{
    /// <summary>
    /// Lerps the <see cref="Camera.orthographicSize" /> value.
    /// </summary>
    public class OrthographicSizeMotion : AbstractFloatMotion<Camera>
    {
        public OrthographicSizeMotion(Camera item, float value) : base(item, value)
        {
        }

        public OrthographicSizeMotion(Camera item, float? from, float to) : base(item, from, to)
        {
        }

        protected override float GetFrom() => item.orthographicSize;
        protected override void SetValue(float value) => item.orthographicSize = value;
    }
}