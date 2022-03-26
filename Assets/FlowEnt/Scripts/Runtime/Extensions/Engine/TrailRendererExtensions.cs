using FriedSynapse.FlowEnt.Motions.Tween.TrailRenderers;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public static class TrailRendererExtensions
    {
        #region Color Linear

        /// <summary>
        /// Applies a <see cref="ColorLinearMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<TrailRenderer> ColorLinear(this TweenMotionProxy<TrailRenderer> proxy, LinearColor value)
            => proxy.Apply(new ColorLinearMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="ColorLinearMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="valueStart"></param>
        /// <param name="valueEnd"></param>
        public static TweenMotionProxy<TrailRenderer> ColorLinear(this TweenMotionProxy<TrailRenderer> proxy, Color valueStart, Color valueEnd)
            => proxy.Apply(new ColorLinearMotion(proxy.Item, new LinearColor(valueStart, valueEnd)));

        /// <summary>
        /// Applies a <see cref="ColorLinearMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<TrailRenderer> ColorLinearTo(this TweenMotionProxy<TrailRenderer> proxy, LinearColor to)
            => proxy.Apply(new ColorLinearMotion(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="ColorLinearMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="toStart"></param>
        /// <param name="toEnd"></param>
        public static TweenMotionProxy<TrailRenderer> ColorLinearTo(this TweenMotionProxy<TrailRenderer> proxy, Color toStart, Color toEnd)
            => proxy.Apply(new ColorLinearMotion(proxy.Item, default, new LinearColor(toStart, toEnd)));

        /// <summary>
        /// Applies a <see cref="ColorLinearMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<TrailRenderer> ColorLinearTo(this TweenMotionProxy<TrailRenderer> proxy, LinearColor from, LinearColor to)
            => proxy.Apply(new ColorLinearMotion(proxy.Item, from, to));

        /// <summary>
        /// Applies a <see cref="ColorLinearMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="fromStart"></param>
        /// <param name="fromEnd"></param>
        /// <param name="toStart"></param>
        /// <param name="toEnd"></param>
        public static TweenMotionProxy<TrailRenderer> ColorLinearTo(this TweenMotionProxy<TrailRenderer> proxy, Color fromStart, Color fromEnd, Color toStart, Color toEnd)
            => proxy.Apply(new ColorLinearMotion(proxy.Item, new LinearColor(fromStart, fromEnd), new LinearColor(toStart, toEnd)));

        #endregion

        #region Gradient

        /// <summary>
        /// Applies a <see cref="GradientMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<TrailRenderer> Gradient(this TweenMotionProxy<TrailRenderer> proxy, Gradient value)
            => proxy.Apply(new GradientMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="GradientMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<TrailRenderer> GradientTo(this TweenMotionProxy<TrailRenderer> proxy, Gradient to)
            => proxy.Apply(new GradientMotion(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="GradientMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<TrailRenderer> GradientTo(this TweenMotionProxy<TrailRenderer> proxy, Gradient from, Gradient to)
            => proxy.Apply(new GradientMotion(proxy.Item, from, to));

        #endregion

        #region Move Vertex Vector

        /// <summary>
        /// Applies a <see cref="MoveVertexVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<TrailRenderer> MoveVertex(this TweenMotionProxy<TrailRenderer> proxy, int index, Vector3 value)
            => proxy.Apply(new MoveVertexVectorMotion(proxy.Item, index, value));

        /// <summary>
        /// Applies a <see cref="MoveVertexVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<TrailRenderer> MoveVertexTo(this TweenMotionProxy<TrailRenderer> proxy, int index, Vector3 to)
            => proxy.Apply(new MoveVertexVectorMotion(proxy.Item, index, default, to));

        /// <summary>
        /// Applies a <see cref="MoveVertexVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<TrailRenderer> MoveVertexTo(this TweenMotionProxy<TrailRenderer> proxy, int index, Vector3 from, Vector3 to)
            => proxy.Apply(new MoveVertexVectorMotion(proxy.Item, index, from, to));

        #endregion

        #region Width Linear

        /// <summary>
        /// Applies a <see cref="WidthLinearMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<TrailRenderer> WidthLinear(this TweenMotionProxy<TrailRenderer> proxy, LinearFloat value)
            => proxy.Apply(new WidthLinearMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="WidthLinearMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="valueStart"></param>
        /// <param name="valueEnd"></param>
        public static TweenMotionProxy<TrailRenderer> WidthLinear(this TweenMotionProxy<TrailRenderer> proxy, float valueStart, float valueEnd)
            => proxy.Apply(new WidthLinearMotion(proxy.Item, new LinearFloat(valueStart, valueEnd)));

        /// <summary>
        /// Applies a <see cref="WidthLinearMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<TrailRenderer> WidthLinearTo(this TweenMotionProxy<TrailRenderer> proxy, LinearFloat to)
            => proxy.Apply(new WidthLinearMotion(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="WidthLinearMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="toStart"></param>
        /// <param name="toEnd"></param>
        public static TweenMotionProxy<TrailRenderer> WidthLinearTo(this TweenMotionProxy<TrailRenderer> proxy, float toStart, float toEnd)
            => proxy.Apply(new WidthLinearMotion(proxy.Item, default, new LinearFloat(toStart, toEnd)));

        /// <summary>
        /// Applies a <see cref="WidthLinearMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<TrailRenderer> WidthLinearTo(this TweenMotionProxy<TrailRenderer> proxy, LinearFloat from, LinearFloat to)
            => proxy.Apply(new WidthLinearMotion(proxy.Item, from, to));

        /// <summary>
        /// Applies a <see cref="WidthLinearMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="fromStart"></param>
        /// <param name="fromEnd"></param>
        /// <param name="toStart"></param>
        /// <param name="toEnd"></param>
        public static TweenMotionProxy<TrailRenderer> WidthLinearTo(this TweenMotionProxy<TrailRenderer> proxy, float fromStart, float fromEnd, float toStart, float toEnd)
            => proxy.Apply(new WidthLinearMotion(proxy.Item, new LinearFloat(fromStart, fromEnd), new LinearFloat(toStart, toEnd)));

        #endregion
    }
}
