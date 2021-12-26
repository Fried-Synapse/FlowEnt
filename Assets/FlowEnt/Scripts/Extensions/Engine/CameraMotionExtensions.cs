using FriedSynapse.FlowEnt.Motions.Cameras;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public static class CameraMotionExtensions
    {
        #region BackgroundColor

        /// <summary>
        /// Applies a <see cref="BackgroundColorMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="value"></param>
        public static TweenMotion<Camera> BackgroundColor(this TweenMotion<Camera> motion, Color value)
            => motion.Apply(new BackgroundColorMotion(motion.Item, value));

        /// <summary>
        /// Applies a <see cref="BackgroundColorMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="to"></param>
        public static TweenMotion<Camera> BackgroundColorTo(this TweenMotion<Camera> motion, Color to)
            => motion.Apply(new BackgroundColorMotion(motion.Item, null, to));

        /// <summary>
        /// Applies a <see cref="BackgroundColorMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotion<Camera> BackgroundColorTo(this TweenMotion<Camera> motion, Color from, Color to)
            => motion.Apply(new BackgroundColorMotion(motion.Item, from, to));

        /// <summary>
        /// Applies a <see cref="BackgroundColorGradientMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="to"></param>
        public static TweenMotion<Camera> BackgroundColorTo(this TweenMotion<Camera> motion, Gradient to)
            => motion.Apply(new BackgroundColorGradientMotion(motion.Item, to));

        #endregion

        #region OrthographicSize

        /// <summary>
        /// Applies a <see cref="OrthographicSizeMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="value"></param>
        public static TweenMotion<Camera> OrthographicSize(this TweenMotion<Camera> motion, float value)
            => motion.Apply(new OrthographicSizeMotion(motion.Item, value));

        /// <summary>
        /// Applies a <see cref="OrthographicSizeMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="to"></param>
        public static TweenMotion<Camera> OrthographicSizeTo(this TweenMotion<Camera> motion, float to)
            => motion.Apply(new OrthographicSizeMotion(motion.Item, null, to));

        /// <summary>
        /// Applies a <see cref="OrthographicSizeMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotion<Camera> OrthographicSizeTo(this TweenMotion<Camera> motion, float from, float to)
            => motion.Apply(new OrthographicSizeMotion(motion.Item, from, to));

        #endregion

        #region FieldOfView

        /// <summary>
        /// Applies a <see cref="FieldOfViewMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="value"></param>
        public static TweenMotion<Camera> FieldOfView(this TweenMotion<Camera> motion, float value)
            => motion.Apply(new FieldOfViewMotion(motion.Item, value));

        /// <summary>
        /// Applies a <see cref="FieldOfViewMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="to"></param>
        public static TweenMotion<Camera> FieldOfViewTo(this TweenMotion<Camera> motion, float to)
            => motion.Apply(new FieldOfViewMotion(motion.Item, null, to));

        /// <summary>
        /// Applies a <see cref="FieldOfViewMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotion<Camera> FieldOfViewTo(this TweenMotion<Camera> motion, float from, float to)
            => motion.Apply(new FieldOfViewMotion(motion.Item, from, to));

        #endregion

        #region NearClipPlane

        /// <summary>
        /// Applies a <see cref="NearClipPlaneMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="value"></param>
        public static TweenMotion<Camera> NearClipPlane(this TweenMotion<Camera> motion, float value)
            => motion.Apply(new NearClipPlaneMotion(motion.Item, value));

        /// <summary>
        /// Applies a <see cref="NearClipPlaneMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="to"></param>
        public static TweenMotion<Camera> NearClipPlaneTo(this TweenMotion<Camera> motion, float to)
            => motion.Apply(new NearClipPlaneMotion(motion.Item, null, to));

        /// <summary>
        /// Applies a <see cref="NearClipPlaneMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotion<Camera> NearClipPlaneTo(this TweenMotion<Camera> motion, float from, float to)
            => motion.Apply(new NearClipPlaneMotion(motion.Item, from, to));

        #endregion

        #region FarClipPlane

        /// <summary>
        /// Applies a <see cref="FarClipPlaneMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="value"></param>
        public static TweenMotion<Camera> FarClipPlane(this TweenMotion<Camera> motion, float value)
            => motion.Apply(new FarClipPlaneMotion(motion.Item, value));

        /// <summary>
        /// Applies a <see cref="FarClipPlaneMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="to"></param>
        public static TweenMotion<Camera> FarClipPlaneTo(this TweenMotion<Camera> motion, float to)
            => motion.Apply(new FarClipPlaneMotion(motion.Item, null, to));

        /// <summary>
        /// Applies a <see cref="FarClipPlaneMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotion<Camera> FarClipPlaneTo(this TweenMotion<Camera> motion, float from, float to)
            => motion.Apply(new FarClipPlaneMotion(motion.Item, from, to));

        #endregion
    }
}
