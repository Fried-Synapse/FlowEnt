using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Cameras
{
    /// <summary>
    /// Lerps the <see cref="Camera.farClipPlane" /> value.
    /// </summary>
    public class FarClipPlaneMotion : AbstractFloatMotion<Camera>
    {
        public FarClipPlaneMotion(Camera item, float value) : base(item, value)
        {
        }

        public FarClipPlaneMotion(Camera item, float? from, float to) : base(item, from, to)
        {
        }

        protected override float GetFrom() => item.farClipPlane;
        protected override void SetValue(float value) => item.farClipPlane = value;
    }
}