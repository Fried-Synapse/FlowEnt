#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public static class FlowEntGizmos
    {
        public static void DrawCurve(ICurve curve, GizmoOptions options = default)
        {
            options ??= new GizmoOptions(width: 2f);
            Vector3[] points = new Vector3[Mathf.CeilToInt(1f / options.Step) + 2];
            float t = 0f;
            int i = 0;

            for (; t <= 1f; t += options.Step, i++)
            {
                points[i] = curve.GetPoint(t);
            }

            for (; i < points.Length; i++)
            {
                points[i] = curve.GetPoint(1f);
            }

            Color initialColour = Handles.color;
            Handles.color = options.Colour;
            Handles.DrawAAPolyLine(options.Width, points);
            Handles.color = initialColour;
        }

        public static void DrawLine(Vector3 start, Vector3 end, GizmoOptions options = default)
        {
            options ??= new GizmoOptions();
            Color initialColour = Handles.color;
            Handles.color = options.Colour;
            Handles.DrawLine(start, end, options.Width);
            Handles.color = initialColour;
        }
    }
}
#endif