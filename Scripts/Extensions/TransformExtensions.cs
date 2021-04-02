using UnityEngine;

namespace FlowEnt
{
    public static class TransformExtensions
    {
        #region Move

        public static Flow MoveTo(this Transform transform, Vector3 to, float time)
            => Flow.Create().Enqueue(time).For(transform).MoveTo(to).Play();

        public static Flow MoveLocalTo(this Transform transform, Vector3 to, float time)
            => Flow.Create().Enqueue(time).For(transform).MoveLocalTo(to).Play();

        #endregion
    }
}
