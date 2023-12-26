using System.Collections.Generic;
using UnityEditor;

namespace FriedSynapse.FlowEnt
{
    //TODO these 2 interfaces should be moved into runtime once the feature works
    public interface IGizmoDrawer
    {
        public void OnGizmosDrawing();
    }

    public interface IGizmoDrawerTarget
    {
    }

    //TODO there is no way to remove the item from being drawn.
    public static class FlowEntGizmos
    {
        private static Dictionary<IGizmoDrawerTarget, List<IGizmoDrawer>> GizmoDrawers { get; } = new();

        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
        private static void OnDrawGizmos(IGizmoDrawerTarget target, GizmoType gizmoType)
        {
            if (GizmoDrawers.TryGetValue(target, out List<IGizmoDrawer> gizmoDrawers))
            {
                foreach (IGizmoDrawer gizmoDrawer in gizmoDrawers)
                {
                    gizmoDrawer.OnGizmosDrawing();
                }
            }
        }

        public static void Register(IGizmoDrawerTarget target, IGizmoDrawer gizmoDrawer)
        {
            if (!GizmoDrawers.TryGetValue(target, out List<IGizmoDrawer> gizmoDrawers))
            {
                gizmoDrawers = new List<IGizmoDrawer>();
                GizmoDrawers.Add(target, gizmoDrawers);
            }

            if (!gizmoDrawers.Contains(gizmoDrawer))
            {
                gizmoDrawers.Add(gizmoDrawer);
            }
        }
    }
}