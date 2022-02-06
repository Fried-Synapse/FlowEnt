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
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchoredPosition(this TweenMotionProxy<RectTransform> proxy, Vector2 value)
            => proxy.Apply(new MoveAnchoredPositionVectorMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchoredPositionTo(this TweenMotionProxy<RectTransform> proxy, Vector2 to)
            => proxy.Apply(new MoveAnchoredPositionVectorMotion(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchoredPositionTo(this TweenMotionProxy<RectTransform> proxy, Vector2 from, Vector2 to)
            => proxy.Apply(new MoveAnchoredPositionVectorMotion(proxy.Item, from, to));

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchoredPositionX(this TweenMotionProxy<RectTransform> proxy, float value)
            => proxy.Apply(new MoveAnchoredPositionVectorMotion(proxy.Item, new Vector2(value, 0f)));

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchoredPositionXTo(this TweenMotionProxy<RectTransform> proxy, float to)
            => proxy.Apply(new MoveAnchoredPositionVectorMotion(proxy.Item, default, new Vector2(to, proxy.Item.anchoredPosition.y)));

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchoredPositionXTo(this TweenMotionProxy<RectTransform> proxy, float from, float to)
            => proxy.Apply(new MoveAnchoredPositionVectorMotion(proxy.Item, new Vector2(from, proxy.Item.anchoredPosition.y), new Vector2(to, proxy.Item.anchoredPosition.y)));

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchoredPositionY(this TweenMotionProxy<RectTransform> proxy, float value)
            => proxy.Apply(new MoveAnchoredPositionVectorMotion(proxy.Item, new Vector2(0f, value)));

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchoredPositionYTo(this TweenMotionProxy<RectTransform> proxy, float to)
            => proxy.Apply(new MoveAnchoredPositionVectorMotion(proxy.Item, default, new Vector2(proxy.Item.anchoredPosition.x, to)));

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchoredPositionYTo(this TweenMotionProxy<RectTransform> proxy, float from, float to)
            => proxy.Apply(new MoveAnchoredPositionVectorMotion(proxy.Item, new Vector2(proxy.Item.anchoredPosition.x, from), new Vector2(proxy.Item.anchoredPosition.x, to)));

        #endregion

        #region MoveAnchoredPosition Spline

        /// <summary>
        /// Applies a <see cref="MoveAnchoredPositionSplineMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="spline"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchoredPositionTo(this TweenMotionProxy<RectTransform> proxy, ISpline spline)
            => proxy.Apply(new MoveAnchoredPositionSplineMotion(proxy.Item, spline));

        #endregion

        #endregion

        #region MoveAnchorTo 

        /// <summary>
        /// Applies a <see cref="MoveAnchorToMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="toMin"></param>
        /// <param name="toMax"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchorTo(this TweenMotionProxy<RectTransform> proxy, Vector2 toMin, Vector2 toMax)
            => proxy.Apply(new MoveAnchorToMotion(proxy.Item, toMin, toMax));

        /// <summary>
        /// Applies a <see cref="MoveAnchorToMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="fromMin"></param>
        /// <param name="fromMax"></param>
        /// <param name="toMin"></param>
        /// <param name="toMax"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchorTo(this TweenMotionProxy<RectTransform> proxy, Vector2 fromMin, Vector2 fromMax, Vector2 toMin, Vector2 toMax)
            => proxy.Apply(new MoveAnchorToMotion(proxy.Item, fromMin, fromMax, toMin, toMax));

        /// <summary>
        /// Applies a <see cref="MoveAnchorToMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchorTo(this TweenMotionProxy<RectTransform> proxy, AnchorPreset to)
            => proxy.Apply(new MoveAnchorToMotion(proxy.Item, to));

        /// <summary>
        /// Applies a <see cref="MoveAnchorToMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> MoveAnchorTo(this TweenMotionProxy<RectTransform> proxy, AnchorPreset from, AnchorPreset to)
            => proxy.Apply(new MoveAnchorToMotion(proxy.Item, from, to));

        #endregion

        #region MoveAnchorTo 

        /// <summary>
        /// Applies a <see cref="MovePivotToMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> MovePivotTo(this TweenMotionProxy<RectTransform> proxy, Vector2 to)
            => proxy.Apply(new MovePivotToMotion(proxy.Item, to));

        /// <summary>
        /// Applies a <see cref="MovePivotToMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> MovePivotTo(this TweenMotionProxy<RectTransform> proxy, Vector2 from, Vector2 to)
            => proxy.Apply(new MovePivotToMotion(proxy.Item, from, to));

        /// <summary>
        /// Applies a <see cref="MovePivotToMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> MovePivotTo(this TweenMotionProxy<RectTransform> proxy, PivotPreset to)
            => proxy.Apply(new MovePivotToMotion(proxy.Item, to));

        /// <summary>
        /// Applies a <see cref="MovePivotToMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> MovePivotTo(this TweenMotionProxy<RectTransform> proxy, PivotPreset from, PivotPreset to)
            => proxy.Apply(new MovePivotToMotion(proxy.Item, from, to));

        #endregion

        #region ScaleSizeDelta

        #region ScaleSizeDelta

        /// <summary>
        /// Applies a <see cref="ScaleSizeDeltaMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<RectTransform> ScaleSizeDelta(this TweenMotionProxy<RectTransform> proxy, Vector2 value)
            => proxy.Apply(new ScaleSizeDeltaMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="ScaleSizeDeltaMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> ScaleSizeDeltaTo(this TweenMotionProxy<RectTransform> proxy, Vector2 to)
            => proxy.Apply(new ScaleSizeDeltaMotion(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="ScaleSizeDeltaMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<RectTransform> ScaleSizeDeltaTo(this TweenMotionProxy<RectTransform> proxy, Vector2 from, Vector2 to)
            => proxy.Apply(new ScaleSizeDeltaMotion(proxy.Item, from, to));

        /// <summary>
        /// Applies a <see cref="ScaleSizeDeltaMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<RectTransform> ScaleSizeDeltaX(this TweenMotionProxy<RectTransform> proxy, float value)
            => proxy.Apply(new ScaleSizeDeltaMotion(proxy.Item, new Vector2(value, 1f)));

        /// <summary>
        /// Applies a <see cref="ScaleSizeDeltaMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<RectTransform> ScaleSizeDeltaY(this TweenMotionProxy<RectTransform> proxy, float value)
            => proxy.Apply(new ScaleSizeDeltaMotion(proxy.Item, new Vector2(1f, value)));

        #endregion

        #endregion
    }
}
