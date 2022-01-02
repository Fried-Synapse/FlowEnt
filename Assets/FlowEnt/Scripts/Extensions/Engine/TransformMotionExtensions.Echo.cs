using UnityEngine;
using FriedSynapse.FlowEnt.Motions.Echo.Transforms;
using FriedSynapse.FlowEnt.Motions.Echo;

namespace FriedSynapse.FlowEnt
{
    public static partial class TransformMotionExtensions
    {
        #region Move

        #region Move

        /// <summary>
        /// Applies a <see cref="MoveVectorMotion{TTransform}" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> Move<TTransform>(this EchoMotionProxy<TTransform> proxy, Vector3 speed)
            where TTransform : Transform
            => proxy.Apply(new MoveVectorMotion<TTransform>(proxy.Item, speed));

        /// <summary>
        /// Applies a <see cref="MoveVectorMotion{TTransform}" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> MoveX<TTransform>(this EchoMotionProxy<TTransform> proxy, float speed)
            where TTransform : Transform
            => proxy.Apply(new MoveVectorMotion<TTransform>(proxy.Item, new Vector3(speed, 0f, 0f)));

        /// <summary>
        /// Applies a <see cref="MoveVectorMotion{TTransform}" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> MoveY<TTransform>(this EchoMotionProxy<TTransform> proxy, float speed)
            where TTransform : Transform
            => proxy.Apply(new MoveVectorMotion<TTransform>(proxy.Item, new Vector3(0f, speed, 0f)));

        /// <summary>
        /// Applies a <see cref="MoveVectorMotion{TTransform}" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> MoveZ<TTransform>(this EchoMotionProxy<TTransform> proxy, float speed)
            where TTransform : Transform
            => proxy.Apply(new MoveVectorMotion<TTransform>(proxy.Item, new Vector3(0f, 0f, speed)));

        /// <summary>
        /// Applies a <see cref="MoveToVectorMotion{TTransform}" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="target"></param>
        /// <param name="speed"></param>
        /// <param name="speedType"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> MoveTo<TTransform>(this EchoMotionProxy<TTransform> proxy, Vector3 target, float speed = MoveToTransformMotion<TTransform>.DefaultSpeed, SpeedType speedType = MoveToTransformMotion<TTransform>.DefaultSpeedType)
            where TTransform : Transform
            => proxy.Apply(new MoveToVectorMotion<TTransform>(proxy.Item, target, speed, speedType));

        /// <summary>
        /// Applies a <see cref="MoveToTransformMotion{TTransform}" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="target"></param>
        /// <param name="speed"></param>
        /// <param name="speedType"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> MoveTo<TTransform>(this EchoMotionProxy<TTransform> proxy, Transform target, float speed = MoveToTransformMotion<TTransform>.DefaultSpeed, SpeedType speedType = MoveToTransformMotion<TTransform>.DefaultSpeedType)
            where TTransform : Transform
            => proxy.Apply(new MoveToTransformMotion<TTransform>(proxy.Item, target, speed, speedType));

        #endregion

        #endregion

        #region Rotate

        #region LookAt

        /// <summary>
        /// Applies a <see cref="LookAtTransformMotion{TTransform}" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="target"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> LookAt<TTransform>(this EchoMotionProxy<TTransform> proxy, Transform target)
            where TTransform : Transform
            => proxy.Apply(new LookAtTransformMotion<TTransform>(proxy.Item, target));

        /// <summary>
        /// Applies a <see cref="LookAtVector3Motion{TTransform}" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="target"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> LookAt<TTransform>(this EchoMotionProxy<TTransform> proxy, Vector3 target)
            where TTransform : Transform
            => proxy.Apply(new LookAtVector3Motion<TTransform>(proxy.Item, target));

        #endregion

        #endregion
        #region Move

        #region Move

        /// <summary>
        /// Applies a <see cref="ScaleVectorMotion{TTransform}" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> Scale<TTransform>(this EchoMotionProxy<TTransform> proxy, Vector3 speed)
            where TTransform : Transform
            => proxy.Apply(new ScaleVectorMotion<TTransform>(proxy.Item, speed));

        /// <summary>
        /// Applies a <see cref="ScaleVectorMotion{TTransform}" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> ScaleX<TTransform>(this EchoMotionProxy<TTransform> proxy, float speed)
            where TTransform : Transform
            => proxy.Apply(new ScaleVectorMotion<TTransform>(proxy.Item, new Vector3(speed, 1f, 1f)));

        /// <summary>
        /// Applies a <see cref="ScaleVectorMotion{TTransform}" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> ScaleY<TTransform>(this EchoMotionProxy<TTransform> proxy, float speed)
            where TTransform : Transform
            => proxy.Apply(new ScaleVectorMotion<TTransform>(proxy.Item, new Vector3(1f, speed, 1f)));

        /// <summary>
        /// Applies a <see cref="ScaleVectorMotion{TTransform}" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> ScaleZ<TTransform>(this EchoMotionProxy<TTransform> proxy, float speed)
            where TTransform : Transform
            => proxy.Apply(new ScaleVectorMotion<TTransform>(proxy.Item, new Vector3(1f, 1f, speed)));

        /// <summary>
        /// Applies a <see cref="ScaleToVectorMotion{TTransform}" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="target"></param>
        /// <param name="speed"></param>
        /// <param name="speedType"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> ScaleTo<TTransform>(this EchoMotionProxy<TTransform> proxy, Vector3 target, float speed = ScaleToTransformMotion<TTransform>.DefaultSpeed, SpeedType speedType = ScaleToTransformMotion<TTransform>.DefaultSpeedType)
            where TTransform : Transform
            => proxy.Apply(new ScaleToVectorMotion<TTransform>(proxy.Item, target, speed, speedType));

        /// <summary>
        /// Applies a <see cref="ScaleToTransformMotion{TTransform}" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="target"></param>
        /// <param name="speed"></param>
        /// <param name="speedType"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> ScaleTo<TTransform>(this EchoMotionProxy<TTransform> proxy, Transform target, float speed = ScaleToTransformMotion<TTransform>.DefaultSpeed, SpeedType speedType = ScaleToTransformMotion<TTransform>.DefaultSpeedType)
            where TTransform : Transform
            => proxy.Apply(new ScaleToTransformMotion<TTransform>(proxy.Item, target, speed, speedType));

        #endregion

        #endregion
    }
}