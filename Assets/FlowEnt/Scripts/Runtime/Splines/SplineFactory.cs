using System;
using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public static class SplineFactory
    {
        public enum SplineType
        {
            Linear,
            BSpline,
            CatmullRom,
            Cubic,
        }

        public static ISpline GetSpline(SplineType type, List<Vector3> points, bool normalise = false)
        {
            ISpline result = type switch
            {
                SplineType.Linear => new LinearSpline(points),
                SplineType.BSpline => new BSpline(points),
                SplineType.CatmullRom => new CatmullRomSpline(points),
                SplineType.Cubic => new CubicSpline(points),
                _ => throw new NotImplementedException(),
            };

            if (normalise)
            {
                result = new NormalisedSpline(result);
            }
            return result;
        }
    }
}
