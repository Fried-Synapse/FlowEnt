using System;
using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public interface ISpline : ICurve
    {
    }

    public abstract class AbstractSpline : AbstractCurve, ISpline
    {
        private const string ErrorListIsNull = "List cannot be null";
        private const string ErrorArrayIsNull = "Array cannot be null";
        private const string ErrorNotEnoughElements = "Not enough points specified";

        protected AbstractSpline(List<Vector3> points)
        {
            if (points == null)
            {
                throw new ArgumentException(ErrorListIsNull);
            }
            if (points.Count < MinPoints)
            {
                throw new ArgumentException(ErrorNotEnoughElements);
            }
            this.points = points.ToArray();
        }

        protected AbstractSpline(params Vector3[] points)
        {
            if (points == null)
            {
                throw new ArgumentException(ErrorArrayIsNull);
            }
            if (points.Length < MinPoints)
            {
                throw new ArgumentException(ErrorNotEnoughElements);
            }
            this.points = points;
        }

        protected virtual int MinPoints => 2;

        protected readonly Vector3[] points;
    }
}
