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
        /// Applies a <see cref="MoveVectorMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> Move<TTransform>(this EchoMotionProxy<TTransform> proxy, Vector3 speed)
            where TTransform : Transform
            => proxy.Apply(new MoveVectorMotion(proxy.Item, speed));

        /// <summary>
        /// Applies a <see cref="MoveVectorMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> MoveX<TTransform>(this EchoMotionProxy<TTransform> proxy, float speed)
            where TTransform : Transform
            => proxy.Apply(new MoveVectorMotion(proxy.Item, new Vector3(speed, 0f, 0f)));

        /// <summary>
        /// Applies a <see cref="MoveVectorMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> MoveY<TTransform>(this EchoMotionProxy<TTransform> proxy, float speed)
            where TTransform : Transform
            => proxy.Apply(new MoveVectorMotion(proxy.Item, new Vector3(0f, speed, 0f)));

        /// <summary>
        /// Applies a <see cref="MoveVectorMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> MoveZ<TTransform>(this EchoMotionProxy<TTransform> proxy, float speed)
            where TTransform : Transform
            => proxy.Apply(new MoveVectorMotion(proxy.Item, new Vector3(0f, 0f, speed)));

        /// <summary>
        /// Applies a <see cref="MoveToVectorMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="target"></param>
        /// <param name="speed"></param>
        /// <param name="speedType"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> MoveTo<TTransform>(this EchoMotionProxy<TTransform> proxy, Vector3 target, float speed = MoveToVectorMotion.DefaultSpeed, SpeedType speedType = MoveToVectorMotion.DefaultSpeedType)
            where TTransform : Transform
            => proxy.Apply(new MoveToVectorMotion(proxy.Item, target, speed, speedType));

        /// <summary>
        /// Applies a <see cref="MoveToTransformMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="target"></param>
        /// <param name="speed"></param>
        /// <param name="speedType"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> MoveTo<TTransform>(this EchoMotionProxy<TTransform> proxy, Transform target, float speed = MoveToVectorMotion.DefaultSpeed, SpeedType speedType = MoveToVectorMotion.DefaultSpeedType)
            where TTransform : Transform
            => proxy.Apply(new MoveToTransformMotion(proxy.Item, target, speed, speedType));

        #endregion

        #endregion

        #region Rotate

        #region Rotate

        /// <summary>
        /// Applies a <see cref="RotateVectorMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> Rotate<TTransform>(this EchoMotionProxy<TTransform> proxy, Quaternion speed)
            where TTransform : Transform
            => proxy.Apply(new RotateVectorMotion(proxy.Item, speed.eulerAngles));

        /// <summary>
        /// Applies a <see cref="RotateVectorMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> Rotate<TTransform>(this EchoMotionProxy<TTransform> proxy, Vector3 speed)
            where TTransform : Transform
            => proxy.Apply(new RotateVectorMotion(proxy.Item, speed));

        /// <summary>
        /// Applies a <see cref="RotateVectorMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> RotateX<TTransform>(this EchoMotionProxy<TTransform> proxy, float speed)
            where TTransform : Transform
            => proxy.Apply(new RotateVectorMotion(proxy.Item, new Vector3(speed, 0f, 0f)));

        /// <summary>
        /// Applies a <see cref="RotateVectorMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> RotateY<TTransform>(this EchoMotionProxy<TTransform> proxy, float speed)
            where TTransform : Transform
            => proxy.Apply(new RotateVectorMotion(proxy.Item, new Vector3(0f, speed, 0f)));

        /// <summary>
        /// Applies a <see cref="RotateVectorMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> RotateZ<TTransform>(this EchoMotionProxy<TTransform> proxy, float speed)
            where TTransform : Transform
            => proxy.Apply(new RotateVectorMotion(proxy.Item, new Vector3(0f, 0f, speed)));

        /// <summary>
        /// Applies a <see cref="RotateVectorMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="target"></param>
        /// <param name="speed"></param>
        /// <param name="speedType"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> RotateTo<TTransform>(this EchoMotionProxy<TTransform> proxy, Quaternion target, float speed = RotateToTransformMotion.DefaultSpeed, SpeedType speedType = RotateToTransformMotion.DefaultSpeedType)
            where TTransform : Transform
            => proxy.Apply(new RotateToQuaternionMotion(proxy.Item, target, speed, speedType));

        /// <summary>
        /// Applies a <see cref="RotateVectorMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="target"></param>
        /// <param name="speed"></param>
        /// <param name="speedType"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> RotateTo<TTransform>(this EchoMotionProxy<TTransform> proxy, Vector3 target, float speed = RotateToQuaternionMotion.DefaultSpeed, SpeedType speedType = RotateToQuaternionMotion.DefaultSpeedType)
            where TTransform : Transform
            => proxy.Apply(new RotateToQuaternionMotion(proxy.Item, Quaternion.Euler(target), speed, speedType));

        /// <summary>
        /// Applies a <see cref="RotateToTransformMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="target"></param>
        /// <param name="speed"></param>
        /// <param name="speedType"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> RotateTo<TTransform>(this EchoMotionProxy<TTransform> proxy, Transform target, float speed = RotateToQuaternionMotion.DefaultSpeed, SpeedType speedType = RotateToQuaternionMotion.DefaultSpeedType)
            where TTransform : Transform
            => proxy.Apply(new RotateToTransformMotion(proxy.Item, target, speed, speedType));

        #endregion

        #region RotateAround

        /// <summary>
        /// Applies a <see cref="RotateAroundTransformMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="point"></param>
        /// <param name="axis"></param>
        /// <param name="speed"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> RotateAround<TTransform>(this EchoMotionProxy<TTransform> proxy, Transform point, Vector3 axis, float speed)
            where TTransform : Transform
            => proxy.Apply(new RotateAroundTransformMotion(proxy.Item, point, axis, speed));

        /// <summary>
        /// Applies a <see cref="RotateAroundVectorMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="point"></param>
        /// <param name="axis"></param>
        /// <param name="speed"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> RotateAround<TTransform>(this EchoMotionProxy<TTransform> proxy, Vector3 point, Vector3 axis, float speed)
            where TTransform : Transform
            => proxy.Apply(new RotateAroundVectorMotion(proxy.Item, point, axis, speed));

        #endregion

        #region LookAt

        /// <summary>
        /// Applies a <see cref="LookAtTransformMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="target"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> LookAt<TTransform>(this EchoMotionProxy<TTransform> proxy, Transform target)
            where TTransform : Transform
            => proxy.Apply(new LookAtTransformMotion(proxy.Item, target));

        /// <summary>
        /// Applies a <see cref="LookAtVector3Motion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="target"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> LookAt<TTransform>(this EchoMotionProxy<TTransform> proxy, Vector3 target)
            where TTransform : Transform
            => proxy.Apply(new LookAtVector3Motion(proxy.Item, target));

        #endregion

        #endregion

        #region Scale

        #region Scale

        /// <summary>
        /// Applies a <see cref="ScaleVectorMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> Scale<TTransform>(this EchoMotionProxy<TTransform> proxy, Vector3 speed)
            where TTransform : Transform
            => proxy.Apply(new ScaleVectorMotion(proxy.Item, speed));

        /// <summary>
        /// Applies a <see cref="ScaleVectorMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> ScaleX<TTransform>(this EchoMotionProxy<TTransform> proxy, float speed)
            where TTransform : Transform
            => proxy.Apply(new ScaleVectorMotion(proxy.Item, new Vector3(speed, 1f, 1f)));

        /// <summary>
        /// Applies a <see cref="ScaleVectorMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> ScaleY<TTransform>(this EchoMotionProxy<TTransform> proxy, float speed)
            where TTransform : Transform
            => proxy.Apply(new ScaleVectorMotion(proxy.Item, new Vector3(1f, speed, 1f)));

        /// <summary>
        /// Applies a <see cref="ScaleVectorMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> ScaleZ<TTransform>(this EchoMotionProxy<TTransform> proxy, float speed)
            where TTransform : Transform
            => proxy.Apply(new ScaleVectorMotion(proxy.Item, new Vector3(1f, 1f, speed)));

        /// <summary>
        /// Applies a <see cref="ScaleToVectorMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="target"></param>
        /// <param name="speed"></param>
        /// <param name="speedType"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> ScaleTo<TTransform>(this EchoMotionProxy<TTransform> proxy, Vector3 target, float speed = ScaleToVectorMotion.DefaultSpeed, SpeedType speedType = ScaleToVectorMotion.DefaultSpeedType)
            where TTransform : Transform
            => proxy.Apply(new ScaleToVectorMotion(proxy.Item, target, speed, speedType));

        /// <summary>
        /// Applies a <see cref="ScaleToTransformMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="target"></param>
        /// <param name="speed"></param>
        /// <param name="speedType"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> ScaleTo<TTransform>(this EchoMotionProxy<TTransform> proxy, Transform target, float speed = ScaleToVectorMotion.DefaultSpeed, SpeedType speedType = ScaleToVectorMotion.DefaultSpeedType)
            where TTransform : Transform
            => proxy.Apply(new ScaleToTransformMotion(proxy.Item, target, speed, speedType));

        #endregion

        #endregion
    }
}