using FriedSynapse.FlowEnt.Motions.Tween.Cameras;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public static class CameraMotionExtensions
    {
        #region BackgroundColor

        /// <summary>
        /// Applies a <see cref="BackgroundColorMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<Camera> BackgroundColor(this TweenMotionProxy<Camera> tweenMotion, Color value)
            => tweenMotion.Apply(new BackgroundColorMotion(tweenMotion.Item, value));

        /// <summary>
        /// Applies a <see cref="BackgroundColorMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Camera> BackgroundColorTo(this TweenMotionProxy<Camera> tweenMotion, Color to)
            => tweenMotion.Apply(new BackgroundColorMotion(tweenMotion.Item, null, to));

        /// <summary>
        /// Applies a <see cref="BackgroundColorMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Camera> BackgroundColorTo(this TweenMotionProxy<Camera> tweenMotion, Color from, Color to)
            => tweenMotion.Apply(new BackgroundColorMotion(tweenMotion.Item, from, to));

        /// <summary>
        /// Applies a <see cref="BackgroundColorGradientMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Camera> BackgroundColorTo(this TweenMotionProxy<Camera> tweenMotion, Gradient to)
            => tweenMotion.Apply(new BackgroundColorGradientMotion(tweenMotion.Item, to));

        #endregion

        #region OrthographicSize

        /// <summary>
        /// Applies a <see cref="OrthographicSizeMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<Camera> OrthographicSize(this TweenMotionProxy<Camera> tweenMotion, float value)
            => tweenMotion.Apply(new OrthographicSizeMotion(tweenMotion.Item, value));

        /// <summary>
        /// Applies a <see cref="OrthographicSizeMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Camera> OrthographicSizeTo(this TweenMotionProxy<Camera> tweenMotion, float to)
            => tweenMotion.Apply(new OrthographicSizeMotion(tweenMotion.Item, null, to));

        /// <summary>
        /// Applies a <see cref="OrthographicSizeMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Camera> OrthographicSizeTo(this TweenMotionProxy<Camera> tweenMotion, float from, float to)
            => tweenMotion.Apply(new OrthographicSizeMotion(tweenMotion.Item, from, to));

        #endregion

        #region FieldOfView

        /// <summary>
        /// Applies a <see cref="FieldOfViewMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<Camera> FieldOfView(this TweenMotionProxy<Camera> tweenMotion, float value)
            => tweenMotion.Apply(new FieldOfViewMotion(tweenMotion.Item, value));

        /// <summary>
        /// Applies a <see cref="FieldOfViewMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Camera> FieldOfViewTo(this TweenMotionProxy<Camera> tweenMotion, float to)
            => tweenMotion.Apply(new FieldOfViewMotion(tweenMotion.Item, null, to));

        /// <summary>
        /// Applies a <see cref="FieldOfViewMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Camera> FieldOfViewTo(this TweenMotionProxy<Camera> tweenMotion, float from, float to)
            => tweenMotion.Apply(new FieldOfViewMotion(tweenMotion.Item, from, to));

        #endregion

        #region NearClipPlane

        /// <summary>
        /// Applies a <see cref="NearClipPlaneMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<Camera> NearClipPlane(this TweenMotionProxy<Camera> tweenMotion, float value)
            => tweenMotion.Apply(new NearClipPlaneMotion(tweenMotion.Item, value));

        /// <summary>
        /// Applies a <see cref="NearClipPlaneMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Camera> NearClipPlaneTo(this TweenMotionProxy<Camera> tweenMotion, float to)
            => tweenMotion.Apply(new NearClipPlaneMotion(tweenMotion.Item, null, to));

        /// <summary>
        /// Applies a <see cref="NearClipPlaneMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Camera> NearClipPlaneTo(this TweenMotionProxy<Camera> tweenMotion, float from, float to)
            => tweenMotion.Apply(new NearClipPlaneMotion(tweenMotion.Item, from, to));

        #endregion

        #region FarClipPlane

        /// <summary>
        /// Applies a <see cref="FarClipPlaneMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<Camera> FarClipPlane(this TweenMotionProxy<Camera> tweenMotion, float value)
            => tweenMotion.Apply(new FarClipPlaneMotion(tweenMotion.Item, value));

        /// <summary>
        /// Applies a <see cref="FarClipPlaneMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Camera> FarClipPlaneTo(this TweenMotionProxy<Camera> tweenMotion, float to)
            => tweenMotion.Apply(new FarClipPlaneMotion(tweenMotion.Item, null, to));

        /// <summary>
        /// Applies a <see cref="FarClipPlaneMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Camera> FarClipPlaneTo(this TweenMotionProxy<Camera> tweenMotion, float from, float to)
            => tweenMotion.Apply(new FarClipPlaneMotion(tweenMotion.Item, from, to));

        #endregion
    }
}
