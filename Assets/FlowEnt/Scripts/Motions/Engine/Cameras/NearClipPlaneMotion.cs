using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Cameras
{
    /// <summary>
    /// Lerps the <see cref="Camera.nearClipPlane" /> value.
    /// </summary>
    public class NearClipPlaneMotion : AbstractFloatMotion<Camera>
    {
        public NearClipPlaneMotion(Camera item, float value) : base(item, value)
        {
        }

        public NearClipPlaneMotion(Camera item, float? from, float to) : base(item, from, to)
        {
        }

        protected override float GetFrom() => item.nearClipPlane;
        protected override void SetValue(float value) => item.nearClipPlane = value;
    }
}