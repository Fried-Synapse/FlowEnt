using System;
using System.Collections.Generic;
using UnityEngine;

namespace FlowEnt
{
    public abstract class AbstractSpline : ISpline
    {
        protected AbstractSpline(List<Vector3> points) : this(points?.ToArray())
        {

        }

        protected AbstractSpline(params Vector3[] points)
        {
            if (points == null)
            {
                throw new ArgumentException("List cannot be null");
            }
            if (points.Length < 2)
            {
                throw new ArgumentException("Not enough points specified");
            }
            this.points = points;
            Init();
        }

        protected Vector3[] points;

        protected virtual void Init() { }
        public abstract Vector3 GetPoint(float t);

#if UNITY_EDITOR
        public void DrawGizmo(Color color = default, float width = 1f, float step = 0.01f)
        {
            Vector3[] points = new Vector3[Mathf.CeilToInt(1f / step) + 2];
            float t = 0f;
            int i = 0;
            for (; t < 1f; t += step, i++)
            {
                points[i] = GetPoint(t);
            }
            for (; i < points.Length; i++)
            {
                points[i] = GetPoint(1f);
            }

            Color initialColour = UnityEditor.Handles.color;
            UnityEditor.Handles.color = color;
            UnityEditor.Handles.DrawAAPolyLine(width, points);
            UnityEditor.Handles.color = initialColour;
        }
#endif
    }
}
