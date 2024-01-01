using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.localPosition" /> value.
    /// </summary>
    public class MoveLocalVectorMotion : AbstractVector3Motion<Transform>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder, IGizmoDrawer
        {
            public override AbstractTweenMotion Build()
                => new MoveLocalVectorMotion(item, value);

#if UNITY_EDITOR
            void IGizmoDrawer.OnGizmosDrawing(GizmoOptions options)
            {
                if (item != null)
                {
                    FlowEntGizmos.DrawLine(item.localPosition, item.localPosition + value, options ?? GizmoOptions);
                }
            }
#endif
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder, IGizmoDrawer
        {
            public override AbstractTweenMotion Build()
                => new MoveLocalVectorMotion(item, From, to);

#if UNITY_EDITOR
            void IGizmoDrawer.OnGizmosDrawing(GizmoOptions options)
            {
                if (item != null)
                {
                    FlowEntGizmos.DrawLine(From ?? item.localPosition, to, options ?? GizmoOptions);
                }
            }
#endif
        }

        public MoveLocalVectorMotion(Transform item, Vector3 value) : base(item, value)
        {
        }

        public MoveLocalVectorMotion(Transform item, Vector3? from, Vector3 to) : base(item, from, to)
        {
        }

        protected override Vector3 GetFrom() => item.localPosition;
        protected override void SetValue(Vector3 value) => item.localPosition = value;
    }
}