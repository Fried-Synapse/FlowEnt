using FriedSynapse.FlowEnt.Motions.Tween.UI.RectTransforms;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public static class RectTransformMotionExtensions
    {
        #region MoveAnchoredPosition

        #region MoveAnchoredPosition

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchoredPosition(this TweenMotionProxy<RectTransform> tweenMotion, Vector2 value)
            => tweenMotion.Apply(new MoveAnchoredPositionVectorMotion(tweenMotion.Item, value));

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchoredPositionTo(this TweenMotionProxy<RectTransform> tweenMotion, Vector2 to)
            => tweenMotion.Apply(new MoveAnchoredPositionVectorMotion(tweenMotion.Item, default, to));

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchoredPositionTo(this TweenMotionProxy<RectTransform> tweenMotion, Vector2 from, Vector2 to)
            => tweenMotion.Apply(new MoveAnchoredPositionVectorMotion(tweenMotion.Item, from, to));

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchoredPositionX(this TweenMotionProxy<RectTransform> tweenMotion, float value)
            => tweenMotion.Apply(new MoveAnchoredPositionVectorMotion(tweenMotion.Item, new Vector2(value, 0f)));

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchoredPositionXTo(this TweenMotionProxy<RectTransform> tweenMotion, float to)
            => tweenMotion.Apply(new MoveAnchoredPositionVectorMotion(tweenMotion.Item, default, new Vector2(to, tweenMotion.Item.anchoredPosition.y)));

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchoredPositionXTo(this TweenMotionProxy<RectTransform> tweenMotion, float from, float to)
            => tweenMotion.Apply(new MoveAnchoredPositionVectorMotion(tweenMotion.Item, new Vector2(from, tweenMotion.Item.anchoredPosition.y), new Vector2(to, tweenMotion.Item.anchoredPosition.y)));

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchoredPositionY(this TweenMotionProxy<RectTransform> tweenMotion, float value)
            => tweenMotion.Apply(new MoveAnchoredPositionVectorMotion(tweenMotion.Item, new Vector2(0f, value)));

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchoredPositionYTo(this TweenMotionProxy<RectTransform> tweenMotion, float to)
            => tweenMotion.Apply(new MoveAnchoredPositionVectorMotion(tweenMotion.Item, default, new Vector2(tweenMotion.Item.anchoredPosition.x, to)));

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchoredPositionYTo(this TweenMotionProxy<RectTransform> tweenMotion, float from, float to)
            => tweenMotion.Apply(new MoveAnchoredPositionVectorMotion(tweenMotion.Item, new Vector2(tweenMotion.Item.anchoredPosition.x, from), new Vector2(tweenMotion.Item.anchoredPosition.x, to)));

        #endregion

        #region MoveAnchoredPosition Spline

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionSplineMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="spline"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchoredPositionTo(this TweenMotionProxy<RectTransform> tweenMotion, ISpline spline)
            => tweenMotion.Apply(new MoveAnchoredPositionSplineMotion(tweenMotion.Item, spline));

        #endregion

        #endregion

        #region MoveAnchorTo 

        /// <summary>
        /// Applies a <see cref="MoveAnchorToMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="toMin"></param>
        /// <param name="toMax"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchorTo(this TweenMotionProxy<RectTransform> tweenMotion, Vector2 toMin, Vector2 toMax)
            => tweenMotion.Apply(new MoveAnchorToMotion(tweenMotion.Item, toMin, toMax));

        /// <summary>
        /// Applies a <see cref="MoveAnchorToMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="fromMin"></param>
        /// <param name="fromMax"></param>
        /// <param name="toMin"></param>
        /// <param name="toMax"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchorTo(this TweenMotionProxy<RectTransform> tweenMotion, Vector2 fromMin, Vector2 fromMax, Vector2 toMin, Vector2 toMax)
            => tweenMotion.Apply(new MoveAnchorToMotion(tweenMotion.Item, fromMin, fromMax, toMin, toMax));

        /// <summary>
        /// Applies a <see cref="MoveAnchorToMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchorTo(this TweenMotionProxy<RectTransform> tweenMotion, AnchorPreset to)
            => tweenMotion.Apply(new MoveAnchorToMotion(tweenMotion.Item, to));

        /// <summary>
        /// Applies a <see cref="MoveAnchorToMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchorTo(this TweenMotionProxy<RectTransform> tweenMotion, AnchorPreset from, AnchorPreset to)
            => tweenMotion.Apply(new MoveAnchorToMotion(tweenMotion.Item, from, to));

        #endregion

        #region MoveAnchorTo 

        /// <summary>
        /// Applies a <see cref="MovePivotToMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> MovePivotTo(this TweenMotionProxy<RectTransform> tweenMotion, Vector2 to)
            => tweenMotion.Apply(new MovePivotToMotion(tweenMotion.Item, to));

        /// <summary>
        /// Applies a <see cref="MovePivotToMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> MovePivotTo(this TweenMotionProxy<RectTransform> tweenMotion, Vector2 from, Vector2 to)
            => tweenMotion.Apply(new MovePivotToMotion(tweenMotion.Item, from, to));

        /// <summary>
        /// Applies a <see cref="MovePivotToMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> MovePivotTo(this TweenMotionProxy<RectTransform> tweenMotion, PivotPreset to)
            => tweenMotion.Apply(new MovePivotToMotion(tweenMotion.Item, to));

        /// <summary>
        /// Applies a <see cref="MovePivotToMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> MovePivotTo(this TweenMotionProxy<RectTransform> tweenMotion, PivotPreset from, PivotPreset to)
            => tweenMotion.Apply(new MovePivotToMotion(tweenMotion.Item, from, to));

        #endregion

        #region ScaleSizeDelta

        #region ScaleSizeDelta

        /// <summary>
        /// Applies a <see cref="ScaleSizeDeltaMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<RectTransform> ScaleSizeDelta(this TweenMotionProxy<RectTransform> tweenMotion, Vector2 value)
            => tweenMotion.Apply(new ScaleSizeDeltaMotion(tweenMotion.Item, value));

        /// <summary>
        /// Applies a <see cref="ScaleSizeDeltaMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> ScaleSizeDeltaTo(this TweenMotionProxy<RectTransform> tweenMotion, Vector2 to)
            => tweenMotion.Apply(new ScaleSizeDeltaMotion(tweenMotion.Item, default, to));

        /// <summary>
        /// Applies a <see cref="ScaleSizeDeltaMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> ScaleSizeDeltaTo(this TweenMotionProxy<RectTransform> tweenMotion, Vector2 from, Vector2 to)
            => tweenMotion.Apply(new ScaleSizeDeltaMotion(tweenMotion.Item, from, to));

        /// <summary>
        /// Applies a <see cref="ScaleSizeDeltaMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<RectTransform> ScaleSizeDeltaX(this TweenMotionProxy<RectTransform> tweenMotion, float value)
            => tweenMotion.Apply(new ScaleSizeDeltaMotion(tweenMotion.Item, new Vector2(value, 1f)));

        /// <summary>
        /// Applies a <see cref="ScaleSizeDeltaMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<RectTransform> ScaleSizeDeltaY(this TweenMotionProxy<RectTransform> tweenMotion, float value)
            => tweenMotion.Apply(new ScaleSizeDeltaMotion(tweenMotion.Item, new Vector2(1f, value)));

        #endregion

        #endregion
    }
}
