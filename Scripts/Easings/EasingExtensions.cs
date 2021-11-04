using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public static class EasingExtensions
    {
#if UNITY_EDITOR
        public static void DrawGizmo(this IEasing spline, Color color = default, float width = 1f, float step = 0.001f)
        {
            Vector3[] points = new Vector3[Mathf.CeilToInt(1f / step) + 2];
            float t = 0f;
            int i = 0;
            for (; t <= 1f; t += step, i++)
            {
                points[i] = new Vector3(t, spline.GetValue(t), 0);
            }
            for (; i < points.Length; i++)
            {
                points[i] = new Vector3(t, spline.GetValue(1f), 0);
            }

            Color initialColour = Handles.color;
            Handles.color = color;
            Handles.DrawAAPolyLine(width, points);
            Handles.color = initialColour;
        }
#endif
    }
}
