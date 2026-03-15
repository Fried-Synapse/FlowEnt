#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public static class GizmosCallbacks
    {
        private static void DrawGizmo(IGizmoDrawer drawer)
        {
            if (drawer is MonoBehaviour monoBehaviour && !monoBehaviour.enabled)
            {
                return;
            }

            drawer.OnGizmosDrawing(null);
        }

        [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
        public static void DrawGizmo(TweenAuthoring drawer, GizmoType gizmoType)
            => DrawGizmo(drawer);

        [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
        public static void DrawGizmo(EchoAuthoring drawer, GizmoType gizmoType)
            => DrawGizmo(drawer);

        [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
        public static void DrawGizmo(FlowAuthoring drawer, GizmoType gizmoType)
            => DrawGizmo(drawer);

        [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
        public static void DrawGizmo(AnimationsAuthoring drawer, GizmoType gizmoType)
            => DrawGizmo(drawer);
    }
}
#endif