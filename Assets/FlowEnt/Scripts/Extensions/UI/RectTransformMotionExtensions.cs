using FriedSynapse.FlowEnt.Motions.UI.RectTransforms;
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
        public static TweenMotion<RectTransform> MoveAnchoredPosition(this TweenMotion<RectTransform> tweenMotion, Vector2 value)
            => tweenMotion.Apply(new MoveAnchoredPositionVectorMotion(tweenMotion.Item, value));

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        public static TweenMotion<RectTransform> MoveAnchoredPositionTo(this TweenMotion<RectTransform> tweenMotion, Vector2 to)
            => tweenMotion.Apply(new MoveAnchoredPositionVectorMotion(tweenMotion.Item, default, to));

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotion<RectTransform> MoveAnchoredPositionTo(this TweenMotion<RectTransform> tweenMotion, Vector2 from, Vector2 to)
            => tweenMotion.Apply(new MoveAnchoredPositionVectorMotion(tweenMotion.Item, from, to));

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="value"></param>
        public static TweenMotion<RectTransform> MoveAnchoredPositionX(this TweenMotion<RectTransform> tweenMotion, float value)
            => tweenMotion.Apply(new MoveAnchoredPositionVectorMotion(tweenMotion.Item, new Vector2(value, 0f)));

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        public static TweenMotion<RectTransform> MoveAnchoredPositionXTo(this TweenMotion<RectTransform> tweenMotion, float to)
            => tweenMotion.Apply(new MoveAnchoredPositionVectorMotion(tweenMotion.Item, default, new Vector2(to, tweenMotion.Item.anchoredPosition.y)));

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotion<RectTransform> MoveAnchoredPositionXTo(this TweenMotion<RectTransform> tweenMotion, float from, float to)
            => tweenMotion.Apply(new MoveAnchoredPositionVectorMotion(tweenMotion.Item, new Vector2(from, tweenMotion.Item.anchoredPosition.y), new Vector2(to, tweenMotion.Item.anchoredPosition.y)));

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="value"></param>
        public static TweenMotion<RectTransform> MoveAnchoredPositionY(this TweenMotion<RectTransform> tweenMotion, float value)
            => tweenMotion.Apply(new MoveAnchoredPositionVectorMotion(tweenMotion.Item, new Vector2(0f, value)));

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        public static TweenMotion<RectTransform> MoveAnchoredPositionYTo(this TweenMotion<RectTransform> tweenMotion, float to)
            => tweenMotion.Apply(new MoveAnchoredPositionVectorMotion(tweenMotion.Item, default, new Vector2(tweenMotion.Item.anchoredPosition.x, to)));

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotion<RectTransform> MoveAnchoredPositionYTo(this TweenMotion<RectTransform> tweenMotion, float from, float to)
            => tweenMotion.Apply(new MoveAnchoredPositionVectorMotion(tweenMotion.Item, new Vector2(tweenMotion.Item.anchoredPosition.x, from), new Vector2(tweenMotion.Item.anchoredPosition.x, to)));

        #endregion

        #region MoveAnchoredPosition Spline

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionSplineMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="spline"></param>
        public static TweenMotion<RectTransform> MoveAnchoredPositionTo(this TweenMotion<RectTransform> tweenMotion, ISpline spline)
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
        public static TweenMotion<RectTransform> MoveAnchorTo(this TweenMotion<RectTransform> tweenMotion, Vector2 toMin, Vector2 toMax)
            => tweenMotion.Apply(new MoveAnchorToMotion(tweenMotion.Item, toMin, toMax));

        /// <summary>
        /// Applies a <see cref="MoveAnchorToMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="fromMin"></param>
        /// <param name="fromMax"></param>
        /// <param name="toMin"></param>
        /// <param name="toMax"></param>
        public static TweenMotion<RectTransform> MoveAnchorTo(this TweenMotion<RectTransform> tweenMotion, Vector2 fromMin, Vector2 fromMax, Vector2 toMin, Vector2 toMax)
            => tweenMotion.Apply(new MoveAnchorToMotion(tweenMotion.Item, fromMin, fromMax, toMin, toMax));

        /// <summary>
        /// Applies a <see cref="MoveAnchorToMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        public static TweenMotion<RectTransform> MoveAnchorTo(this TweenMotion<RectTransform> tweenMotion, AnchorPreset to)
            => tweenMotion.Apply(new MoveAnchorToMotion(tweenMotion.Item, to));

        /// <summary>
        /// Applies a <see cref="MoveAnchorToMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotion<RectTransform> MoveAnchorTo(this TweenMotion<RectTransform> tweenMotion, AnchorPreset from, AnchorPreset to)
            => tweenMotion.Apply(new MoveAnchorToMotion(tweenMotion.Item, from, to));

        #endregion

        #region MoveAnchorTo 

        /// <summary>
        /// Applies a <see cref="MovePivotToMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        public static TweenMotion<RectTransform> MovePivotTo(this TweenMotion<RectTransform> tweenMotion, Vector2 to)
            => tweenMotion.Apply(new MovePivotToMotion(tweenMotion.Item, to));

        /// <summary>
        /// Applies a <see cref="MovePivotToMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotion<RectTransform> MovePivotTo(this TweenMotion<RectTransform> tweenMotion, Vector2 from, Vector2 to)
            => tweenMotion.Apply(new MovePivotToMotion(tweenMotion.Item, from, to));

        /// <summary>
        /// Applies a <see cref="MovePivotToMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        public static TweenMotion<RectTransform> MovePivotTo(this TweenMotion<RectTransform> tweenMotion, PivotPreset to)
            => tweenMotion.Apply(new MovePivotToMotion(tweenMotion.Item, to));

        /// <summary>
        /// Applies a <see cref="MovePivotToMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotion<RectTransform> MovePivotTo(this TweenMotion<RectTransform> tweenMotion, PivotPreset from, PivotPreset to)
            => tweenMotion.Apply(new MovePivotToMotion(tweenMotion.Item, from, to));

        #endregion

        #region ScaleSizeDelta

        #region ScaleSizeDelta

        /// <summary>
        /// Applies a <see cref="ScaleSizeDeltaMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="value"></param>
        public static TweenMotion<RectTransform> ScaleSizeDelta(this TweenMotion<RectTransform> tweenMotion, Vector2 value)
            => tweenMotion.Apply(new ScaleSizeDeltaMotion(tweenMotion.Item, value));

        /// <summary>
        /// Applies a <see cref="ScaleSizeDeltaMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        public static TweenMotion<RectTransform> ScaleSizeDeltaTo(this TweenMotion<RectTransform> tweenMotion, Vector2 to)
            => tweenMotion.Apply(new ScaleSizeDeltaMotion(tweenMotion.Item, default, to));

        /// <summary>
        /// Applies a <see cref="ScaleSizeDeltaMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotion<RectTransform> ScaleSizeDeltaTo(this TweenMotion<RectTransform> tweenMotion, Vector2 from, Vector2 to)
            => tweenMotion.Apply(new ScaleSizeDeltaMotion(tweenMotion.Item, from, to));

        /// <summary>
        /// Applies a <see cref="ScaleSizeDeltaMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="value"></param>
        public static TweenMotion<RectTransform> ScaleSizeDeltaX(this TweenMotion<RectTransform> tweenMotion, float value)
            => tweenMotion.Apply(new ScaleSizeDeltaMotion(tweenMotion.Item, new Vector2(value, 1f)));

        /// <summary>
        /// Applies a <see cref="ScaleSizeDeltaMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="value"></param>
        public static TweenMotion<RectTransform> ScaleSizeDeltaY(this TweenMotion<RectTransform> tweenMotion, float value)
            => tweenMotion.Apply(new ScaleSizeDeltaMotion(tweenMotion.Item, new Vector2(1f, value)));

        #endregion

        #endregion
    }
}
