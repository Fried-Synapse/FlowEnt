using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.position" /> value.
    /// </summary>
    public class MoveVectorMotion : AbstractVector3Motion<Transform>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder, IGizmoDrawer
        {
            public override AbstractTweenMotion Build()
                => new MoveVectorMotion(item, value);

#if UNITY_EDITOR
            void IGizmoDrawer.OnGizmosDrawing()
            {
                if (item != null)
                {
                    FlowEntGizmos.DrawLine(item.position, item.position + value);
                }
            }
#endif
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder, IGizmoDrawer
        {
            public override AbstractTweenMotion Build()
                => new MoveVectorMotion(item, From, to);

#if UNITY_EDITOR
            void IGizmoDrawer.OnGizmosDrawing()
            {
                if (item != null)
                {
                    FlowEntGizmos.DrawLine(From ?? item.position, to);
                }
            }
#endif
        }

        public MoveVectorMotion(Transform item, Vector3 value) : base(item, value)
        {
        }

        public MoveVectorMotion(Transform item, Vector3? from, Vector3 to) : base(item, from, to)
        {
        }

        protected override Vector3 GetFrom() => item.position;
        protected override void SetValue(Vector3 value) => item.position = value;
    }
}