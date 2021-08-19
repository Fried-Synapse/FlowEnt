using FriedSynapse.FlowEnt.Motions.RectTransforms;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public static class RectTransformMotionExtensions
    {
        #region MoveAnchoredPosition

        #region MoveAnchoredPosition

        public static TweenMotion<RectTransform> MoveAnchoredPosition(this TweenMotion<RectTransform> motionWrapper, Vector2 value)
            => motionWrapper.Apply(new MoveAnchoredPositionVectorMotion(motionWrapper.Item, value));

        public static TweenMotion<RectTransform> MoveAnchoredPositionX(this TweenMotion<RectTransform> motionWrapper, float x)
            => motionWrapper.Apply(new MoveAnchoredPositionVectorMotion(motionWrapper.Item, new Vector2(x, 0f)));

        public static TweenMotion<RectTransform> MoveAnchoredPositionY(this TweenMotion<RectTransform> motionWrapper, float y)
            => motionWrapper.Apply(new MoveAnchoredPositionVectorMotion(motionWrapper.Item, new Vector2(0f, y)));

        #endregion

        #region MoveAnchoredPositionTo

        public static TweenMotion<RectTransform> MoveAnchoredPositionTo(this TweenMotion<RectTransform> motionWrapper, Vector2 to)
            => motionWrapper.Apply(new MoveAnchoredPositionToVectorMotion(motionWrapper.Item, to));

        public static TweenMotion<RectTransform> MoveAnchoredPositionXTo(this TweenMotion<RectTransform> motionWrapper, float to)
            => motionWrapper.Apply(new MoveAnchoredPositionToVectorMotion(motionWrapper.Item, new Vector2(to, motionWrapper.Item.anchoredPosition.y)));

        public static TweenMotion<RectTransform> MoveAnchoredPositionYTo(this TweenMotion<RectTransform> motionWrapper, float to)
            => motionWrapper.Apply(new MoveAnchoredPositionToVectorMotion(motionWrapper.Item, new Vector2(motionWrapper.Item.anchoredPosition.x, to)));

        public static TweenMotion<RectTransform> MoveAnchoredPositionTo(this TweenMotion<RectTransform> motionWrapper, Vector2 from, Vector2 to)
            => motionWrapper.Apply(new MoveAnchoredPositionToVectorMotion(motionWrapper.Item, from, to));

        public static TweenMotion<RectTransform> MoveAnchoredPositionXTo(this TweenMotion<RectTransform> motionWrapper, float from, float to)
            => motionWrapper.Apply(new MoveAnchoredPositionToVectorMotion(motionWrapper.Item, new Vector2(from, motionWrapper.Item.anchoredPosition.y), new Vector2(to, motionWrapper.Item.anchoredPosition.y)));

        public static TweenMotion<RectTransform> MoveAnchoredPositionYTo(this TweenMotion<RectTransform> motionWrapper, float from, float to)
            => motionWrapper.Apply(new MoveAnchoredPositionToVectorMotion(motionWrapper.Item, new Vector2(motionWrapper.Item.anchoredPosition.x, from), new Vector2(motionWrapper.Item.anchoredPosition.x, to)));

        #endregion

        #region MoveAnchoredPosition Spline

        public static TweenMotion<RectTransform> MoveAnchoredPositionTo(this TweenMotion<RectTransform> motionWrapper, ISpline spline)
            => motionWrapper.Apply(new MoveAnchoredPositionSplineMotion(motionWrapper.Item, spline));

        #endregion

        #endregion

        #region MoveAnchorTo 

        public static TweenMotion<RectTransform> MoveAnchorTo(this TweenMotion<RectTransform> motionWrapper, Vector2 toMin, Vector2 toMax)
            => motionWrapper.Apply(new MoveAnchorToMotion(motionWrapper.Item, toMin, toMax));

        public static TweenMotion<RectTransform> MoveAnchorTo(this TweenMotion<RectTransform> motionWrapper, Vector2 fromMin, Vector2 fromMax, Vector2 toMin, Vector2 toMax)
            => motionWrapper.Apply(new MoveAnchorToMotion(motionWrapper.Item, fromMin, fromMax, toMin, toMax));

        public static TweenMotion<RectTransform> MoveAnchorTo(this TweenMotion<RectTransform> motionWrapper, AnchorPreset to)
            => motionWrapper.Apply(new MoveAnchorToMotion(motionWrapper.Item, to));

        public static TweenMotion<RectTransform> MoveAnchorTo(this TweenMotion<RectTransform> motionWrapper, AnchorPreset from, AnchorPreset to)
            => motionWrapper.Apply(new MoveAnchorToMotion(motionWrapper.Item, from, to));

        #endregion

        #region MoveAnchorTo 

        public static TweenMotion<RectTransform> MovePivotTo(this TweenMotion<RectTransform> motionWrapper, Vector2 to)
            => motionWrapper.Apply(new MovePivotToMotion(motionWrapper.Item, to));

        public static TweenMotion<RectTransform> MovePivotTo(this TweenMotion<RectTransform> motionWrapper, Vector2 from, Vector2 to)
            => motionWrapper.Apply(new MovePivotToMotion(motionWrapper.Item, from, to));

        public static TweenMotion<RectTransform> MovePivotTo(this TweenMotion<RectTransform> motionWrapper, PivotPreset to)
            => motionWrapper.Apply(new MovePivotToMotion(motionWrapper.Item, to));

        public static TweenMotion<RectTransform> MovePivotTo(this TweenMotion<RectTransform> motionWrapper, PivotPreset from, PivotPreset to)
            => motionWrapper.Apply(new MovePivotToMotion(motionWrapper.Item, from, to));

        #endregion

        #region ScaleSizeDelta

        #region ScaleSizeDelta

        public static TweenMotion<RectTransform> ScaleSizeDelta(this TweenMotion<RectTransform> motionWrapper, Vector2 to)
            => motionWrapper.Apply(new ScaleSizeDeltaMotion(motionWrapper.Item, to));

        public static TweenMotion<RectTransform> ScaleSizeDeltaX(this TweenMotion<RectTransform> motionWrapper, float x)
            => motionWrapper.Apply(new ScaleSizeDeltaMotion(motionWrapper.Item, new Vector2(x, 1f)));

        public static TweenMotion<RectTransform> ScaleSizeDeltaY(this TweenMotion<RectTransform> motionWrapper, float y)
            => motionWrapper.Apply(new ScaleSizeDeltaMotion(motionWrapper.Item, new Vector2(1f, y)));

        #endregion

        #region ScaleSizeDeltaTo

        public static TweenMotion<RectTransform> ScaleSizeDeltaTo(this TweenMotion<RectTransform> motionWrapper, Vector2 to)
            => motionWrapper.Apply(new ScaleSizeDeltaToMotion(motionWrapper.Item, to));

        public static TweenMotion<RectTransform> ScaleSizeDeltaTo(this TweenMotion<RectTransform> motionWrapper, Vector2 from, Vector2 to)
            => motionWrapper.Apply(new ScaleSizeDeltaToMotion(motionWrapper.Item, from, to));

        #endregion

        #endregion
    }
}
