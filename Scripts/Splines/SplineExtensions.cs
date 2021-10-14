using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public static class SplineExtensions
    {
#if UNITY_EDITOR
        public static void DrawGizmo(this ISpline spline, Color color = default, float width = 1f, float step = 0.001f)
        {
            Vector3[] points = new Vector3[Mathf.CeilToInt(1f / step) + 2];
            float t = 0f;
            int i = 0;
            for (; t < 1f; t += step, i++)
            {
                points[i] = spline.GetPoint(t);
            }
            for (; i < points.Length; i++)
            {
                points[i] = spline.GetPoint(1f);
            }

            Color initialColour = UnityEditor.Handles.color;
            UnityEditor.Handles.color = color;
            UnityEditor.Handles.DrawAAPolyLine(width, points);
            UnityEditor.Handles.color = initialColour;
        }
#endif
    }
}
