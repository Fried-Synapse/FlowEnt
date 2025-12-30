using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractVector3MotionWithGizmo<TItem> : AbstractVector3Motion<TItem>
    {
        [Serializable]
        public abstract class AbstractVector3ValueBuilderWithGizmo : AbstractValueBuilder, IGizmoDrawer
        {
#if UNITY_EDITOR
            private protected abstract (Vector3 Start, Vector3 End) GizmoLine { get; }

            void IGizmoDrawer.OnGizmosDrawing(GizmoOptions options)
            {
                if (item == null)
                {
                    return;
                }

                FlowEntGizmos.DrawLine(
                    GizmoLine.Start,
                    GizmoLine.End,
                    options ?? GizmoOptions);
            }
#endif
        }

        [Serializable]
        public abstract class AbstractVector3FromToBuilderWithGizmo : AbstractFromToBuilder, IGizmoDrawer
        {
#if UNITY_EDITOR
            private protected abstract (Vector3 Start, Vector3 End) GizmoLine { get; }

            void IGizmoDrawer.OnGizmosDrawing(GizmoOptions options)
            {
                if (item == null)
                {
                    return;
                }

                FlowEntGizmos.DrawLine(
                    GizmoLine.Start,
                    GizmoLine.End,
                    options ?? GizmoOptions);
            }
#endif
        }

        protected AbstractVector3MotionWithGizmo(TItem item, Vector3 value) : base(item, value)
        {
        }

        protected AbstractVector3MotionWithGizmo(TItem item, Vector3? from, Vector3 to) :
            base(item, from, to)
        {
        }
    }
}