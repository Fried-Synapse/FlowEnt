using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Easings
{
    public static class EasingExtensions
    {
        /// <summary>
        /// Creates a <see cref="ReverseEasing"/> usign the specified easing.
        /// </summary>
        /// <param name="easing"></param>
        public static IEasing Reverse(this IEasing easing)
            => new ReverseEasing(easing);

#if UNITY_EDITOR
        public static void DrawGizmo(this IEasing easing, Vector3 origin = default, Color color = default, float width = 1f, float step = 0.001f)
        {
            Vector3[] points = new Vector3[Mathf.CeilToInt(1f / step) + 2];
            float t = 0f;
            int i = 0;
            if (color == default)
            {
                color = Color.white;
            }
            for (; t <= 1f; t += step, i++)
            {
                points[i] = origin + new Vector3(t, easing.GetValue(t), 0);
            }
            for (; i < points.Length; i++)
            {
                points[i] = origin + new Vector3(t, easing.GetValue(1f), 0);
            }

            Color initialColour = Handles.color;
            Handles.color = color;
            Handles.DrawAAPolyLine(width, points);
            Handles.color = initialColour;
        }
#endif
    }
}
