using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Cameras
{
    /// <summary>
    /// Lerps the <see cref="Camera.backgroundColor" /> value.
    /// </summary>
    public class BackgroundColorMotion : AbstractColorMotion<Camera>
    {
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