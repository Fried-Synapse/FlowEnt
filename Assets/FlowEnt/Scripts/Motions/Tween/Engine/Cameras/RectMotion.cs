using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Cameras
{
    /// <summary>
    /// Lerps the <see cref="Camera.rect" /> value.
    /// </summary>
    public class RectMotion : AbstractRectMotion<Camera>
    {
        public RectMotion(Camera item, Rect value) : base(item, value)
        {
        }

        public RectMotion(Camera item, Rect? from, Rect to) : base(item, from, to)
        {
        }

        protected override Rect GetFrom() => item.rect;
        protected override void SetValue(Rect value) => item.rect = value;
    }
}