using System;
using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt
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
        }

        protected Vector3[] points;

        public abstract Vector3 GetPoint(float t);
    }
}
