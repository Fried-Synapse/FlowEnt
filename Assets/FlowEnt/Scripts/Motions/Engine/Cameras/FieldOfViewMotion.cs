using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Cameras
{
    /// <summary>
    /// Lerps the <see cref="Camera.fieldOfView" /> value.
    /// </summary>
    public class FieldOfViewMotion : AbstractFloatMotion<Camera>
    {
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