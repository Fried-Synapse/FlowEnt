using FlowEnt.Motions.RectTransformMotions;
using UnityEngine;

namespace FlowEnt
{
    public static class RectTransformMotionExtensions
    {
        #region Move

        #region Move

        public static TweenMotion<RectTransform> MoveAnchoredPosition(this TweenMotion<RectTransform> motionWrapper, Vector2 value)
            => motionWrapper.Apply(new MoveAnchoredPositionVectorMotion(motionWrapper.Item, value));

        public static TweenMotion<RectTransform> MoveAnchoredPositionX(this TweenMotion<RectTransform> motionWrapper, float x)
            => motionWrapper.Apply(new MoveAnchoredPositionVectorMotion(motionWrapper.Item, new Vector2(x, 0)));

        public static TweenMotion<RectTransform> MoveAnchoredPositionY(this TweenMotion<RectTransform> motionWrapper, float y)
            => motionWrapper.Apply(new MoveAnchoredPositionVectorMotion(motionWrapper.Item, new Vector2(0, y)));

        #endregion

        #region MoveTo

        public static TweenMotion<RectTransform> MoveAnchoredPositionTo(this TweenMotion<RectTransform> motionWrapper, Vector2 to)
            => motionWrapper.Apply(new MoveAnchoredPositionToVectorMotion(motionWrapper.Item, to));

        public static TweenMotion<RectTransform> MoveAnchoredPositionTo(this TweenMotion<RectTransform> motionWrapper, Vector2 from, Vector2 to)
            => motionWrapper.Apply(new MoveAnchoredPositionToVectorMotion(motionWrapper.Item, from, to));

        #endregion

        #region Move Spline

        public static TweenMotion<RectTransform> MoveAnchoredPositionTo(this TweenMotion<RectTransform> motionWrapper, ISpline spline)
            => motionWrapper.Apply(new MoveAnchoredPositionSplineMotion(motionWrapper.Item, spline));

        #endregion

        #endregion
    }
}
