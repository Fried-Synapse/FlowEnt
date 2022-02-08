using FriedSynapse.FlowEnt.Motions;
using FriedSynapse.FlowEnt.Motions.Echo.Rigidbodies;
using FriedSynapse.FlowEnt.Motions.Tween.Rigidbodies;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public static class RigidbodyMotionExtensions
    {
        #region Tween

        #region Move

        #region Move

        /// <summary>
        /// Applies a <see cref="MoveVectorMotion{TRigidbody}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TRigidbody"></typeparam>
        public static TweenMotionProxy<TRigidbody> Move<TRigidbody>(this TweenMotionProxy<TRigidbody> proxy, Vector3 value)
            where TRigidbody : Rigidbody
            => proxy.Apply(new MoveVectorMotion<TRigidbody>(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="MoveVectorMotion{TRigidbody}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRigidbody"></typeparam>
        public static TweenMotionProxy<TRigidbody> MoveTo<TRigidbody>(this TweenMotionProxy<TRigidbody> proxy, Vector3 to)
            where TRigidbody : Rigidbody
            => proxy.Apply(new MoveVectorMotion<TRigidbody>(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="MoveVectorMotion{TRigidbody}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRigidbody"></typeparam>
        public static TweenMotionProxy<TRigidbody> MoveTo<TRigidbody>(this TweenMotionProxy<TRigidbody> proxy, Vector3 from, Vector3 to)
            where TRigidbody : Rigidbody
            => proxy.Apply(new MoveVectorMotion<TRigidbody>(proxy.Item, from, to));

        #endregion

        #region MoveTo AnimationCurve3d

        /// <summary>
        /// Applies a <see cref="MoveAnimationCurve3dMotion{TRigidbody}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="animationCurve"></param>
        /// <typeparam name="TRigidbody"></typeparam>
        public static TweenMotionProxy<TRigidbody> MoveTo<TRigidbody>(this TweenMotionProxy<TRigidbody> proxy, AnimationCurve3d animationCurve)
            where TRigidbody : Rigidbody
            => proxy.Apply(new MoveAnimationCurve3dMotion<TRigidbody>(proxy.Item, animationCurve));

        #endregion

        #region Move Axis

        /// <summary>
        /// Applies a <see cref="MoveVectorMotion{TRigidbody}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TRigidbody"></typeparam>
        public static TweenMotionProxy<TRigidbody> MoveX<TRigidbody>(this TweenMotionProxy<TRigidbody> proxy, float value)
            where TRigidbody : Rigidbody
            => proxy.Apply(new MoveVectorMotion<TRigidbody>(proxy.Item, new Vector3(value, 0f, 0f)));

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TRigidbody}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRigidbody"></typeparam>
        public static TweenMotionProxy<TRigidbody> MoveXTo<TRigidbody>(this TweenMotionProxy<TRigidbody> proxy, float to)
            where TRigidbody : Rigidbody
            => proxy.Apply(new MoveAxisMotion<TRigidbody>(proxy.Item, Axis.X, default, to));

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TRigidbody}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRigidbody"></typeparam>
        public static TweenMotionProxy<TRigidbody> MoveXTo<TRigidbody>(this TweenMotionProxy<TRigidbody> proxy, float from, float to)
            where TRigidbody : Rigidbody
            => proxy.Apply(new MoveAxisMotion<TRigidbody>(proxy.Item, Axis.X, from, to));

        /// <summary>
        /// Applies a <see cref="MoveVectorMotion{TRigidbody}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TRigidbody"></typeparam>
        public static TweenMotionProxy<TRigidbody> MoveY<TRigidbody>(this TweenMotionProxy<TRigidbody> proxy, float value)
            where TRigidbody : Rigidbody
            => proxy.Apply(new MoveVectorMotion<TRigidbody>(proxy.Item, new Vector3(0f, value, 0f)));

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TRigidbody}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRigidbody"></typeparam>
        public static TweenMotionProxy<TRigidbody> MoveYTo<TRigidbody>(this TweenMotionProxy<TRigidbody> proxy, float to)
            where TRigidbody : Rigidbody
            => proxy.Apply(new MoveAxisMotion<TRigidbody>(proxy.Item, Axis.Y, default, to));

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TRigidbody}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRigidbody"></typeparam>
        public static TweenMotionProxy<TRigidbody> MoveYTo<TRigidbody>(this TweenMotionProxy<TRigidbody> proxy, float from, float to)
            where TRigidbody : Rigidbody
            => proxy.Apply(new MoveAxisMotion<TRigidbody>(proxy.Item, Axis.Y, from, to));

        /// <summary>
        /// Applies a <see cref="MoveVectorMotion{TRigidbody}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TRigidbody"></typeparam>
        public static TweenMotionProxy<TRigidbody> MoveZ<TRigidbody>(this TweenMotionProxy<TRigidbody> proxy, float value)
            where TRigidbody : Rigidbody
            => proxy.Apply(new MoveVectorMotion<TRigidbody>(proxy.Item, new Vector3(0f, 0f, value)));

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TRigidbody}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRigidbody"></typeparam>
        public static TweenMotionProxy<TRigidbody> MoveZTo<TRigidbody>(this TweenMotionProxy<TRigidbody> proxy, float to)
            where TRigidbody : Rigidbody
            => proxy.Apply(new MoveAxisMotion<TRigidbody>(proxy.Item, Axis.Z, default, to));

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TRigidbody}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRigidbody"></typeparam>
        public static TweenMotionProxy<TRigidbody> MoveZTo<TRigidbody>(this TweenMotionProxy<TRigidbody> proxy, float from, float to)
            where TRigidbody : Rigidbody
            => proxy.Apply(new MoveAxisMotion<TRigidbody>(proxy.Item, Axis.Z, from, to));

        #endregion

        #region MoveTo Spline

        /// <summary>
        /// Applies a <see cref="MoveSplineMotion{TRigidbody}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="spline"></param>
        /// <typeparam name="TRigidbody"></typeparam>
        public static TweenMotionProxy<TRigidbody> MoveTo<TRigidbody>(this TweenMotionProxy<TRigidbody> proxy, ISpline spline)
            where TRigidbody : Rigidbody
            => proxy.Apply(new MoveSplineMotion<TRigidbody>(proxy.Item, spline));

        #endregion

        #endregion

        #endregion

        #region Echo

        #region Input

        /// <summary>
        /// Applies a <see cref="MoveByInputMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        public static EchoMotionProxy<Rigidbody> MoveByInput(this EchoMotionProxy<Rigidbody> proxy, float speed = MoveByInputMotion.DefaultSpeed)
            => proxy.Apply(new MoveByInputMotion(proxy.Item, speed));

        /// <summary>
        /// Applies a <see cref="RotateByInputMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        public static EchoMotionProxy<Rigidbody> RotateByInput(this EchoMotionProxy<Rigidbody> proxy, Transform camera, float speed = RotateByInputMotion.DefaultSensitivity)
            => proxy.Apply(new RotateByInputMotion(proxy.Item, camera, speed));

        /// <summary>
        /// Applies a <see cref="JumpByInputMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="force"></param>
        public static EchoMotionProxy<Rigidbody> JumpByInput(this EchoMotionProxy<Rigidbody> proxy, float force = JumpByInputMotion.DefaultForce)
            => proxy.Apply(new JumpByInputMotion(proxy.Item, force));

        #endregion

        #endregion
    }
}