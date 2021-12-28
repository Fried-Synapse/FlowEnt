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
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<Camera> BackgroundColor(this TweenMotionProxy<Camera> proxy, Color value)
            => proxy.Apply(new BackgroundColorMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="BackgroundColorMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Camera> BackgroundColorTo(this TweenMotionProxy<Camera> proxy, Color to)
            => proxy.Apply(new BackgroundColorMotion(proxy.Item, null, to));

        /// <summary>
        /// Applies a <see cref="BackgroundColorMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Camera> BackgroundColorTo(this TweenMotionProxy<Camera> proxy, Color from, Color to)
            => proxy.Apply(new BackgroundColorMotion(proxy.Item, from, to));

        /// <summary>
        /// Applies a <see cref="BackgroundColorGradientMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Camera> BackgroundColorTo(this TweenMotionProxy<Camera> proxy, Gradient to)
            => proxy.Apply(new BackgroundColorGradientMotion(proxy.Item, to));

        #endregion

        #region OrthographicSize

        /// <summary>
        /// Applies a <see cref="OrthographicSizeMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<Camera> OrthographicSize(this TweenMotionProxy<Camera> proxy, float value)
            => proxy.Apply(new OrthographicSizeMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="OrthographicSizeMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Camera> OrthographicSizeTo(this TweenMotionProxy<Camera> proxy, float to)
            => proxy.Apply(new OrthographicSizeMotion(proxy.Item, null, to));

        /// <summary>
        /// Applies a <see cref="OrthographicSizeMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Camera> OrthographicSizeTo(this TweenMotionProxy<Camera> proxy, float from, float to)
            => proxy.Apply(new OrthographicSizeMotion(proxy.Item, from, to));

        #endregion

        #region FieldOfView

        /// <summary>
        /// Applies a <see cref="FieldOfViewMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<Camera> FieldOfView(this TweenMotionProxy<Camera> proxy, float value)
            => proxy.Apply(new FieldOfViewMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="FieldOfViewMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Camera> FieldOfViewTo(this TweenMotionProxy<Camera> proxy, float to)
            => proxy.Apply(new FieldOfViewMotion(proxy.Item, null, to));

        /// <summary>
        /// Applies a <see cref="FieldOfViewMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Camera> FieldOfViewTo(this TweenMotionProxy<Camera> proxy, float from, float to)
            => proxy.Apply(new FieldOfViewMotion(proxy.Item, from, to));

        #endregion

        #region NearClipPlane

        /// <summary>
        /// Applies a <see cref="NearClipPlaneMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<Camera> NearClipPlane(this TweenMotionProxy<Camera> proxy, float value)
            => proxy.Apply(new NearClipPlaneMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="NearClipPlaneMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Camera> NearClipPlaneTo(this TweenMotionProxy<Camera> proxy, float to)
            => proxy.Apply(new NearClipPlaneMotion(proxy.Item, null, to));

        /// <summary>
        /// Applies a <see cref="NearClipPlaneMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Camera> NearClipPlaneTo(this TweenMotionProxy<Camera> proxy, float from, float to)
            => proxy.Apply(new NearClipPlaneMotion(proxy.Item, from, to));

        #endregion

        #region FarClipPlane

        /// <summary>
        /// Applies a <see cref="FarClipPlaneMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<Camera> FarClipPlane(this TweenMotionProxy<Camera> proxy, float value)
            => proxy.Apply(new FarClipPlaneMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="FarClipPlaneMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Camera> FarClipPlaneTo(this TweenMotionProxy<Camera> proxy, float to)
            => proxy.Apply(new FarClipPlaneMotion(proxy.Item, null, to));

        /// <summary>
        /// Applies a <see cref="FarClipPlaneMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Camera> FarClipPlaneTo(this TweenMotionProxy<Camera> proxy, float from, float to)
            => proxy.Apply(new FarClipPlaneMotion(proxy.Item, from, to));

        #endregion
    }
}
