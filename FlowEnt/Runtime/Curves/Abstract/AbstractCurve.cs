using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public interface ICurve
    {
        public Vector3 GetPoint(float t);
    }
    
    public abstract class AbstractCurve : ICurve
    {
        protected NormalisedCurve cachedNormalisedCurve;

        public abstract Vector3 GetPoint(float t);

        /// <summary>
        /// This will return a new curve, which is a normalised version of this curve.
        /// Go to <see cref="NormalisedCurve" /> for more info.
        /// </summary>
        /// <param name="cache"></param>
        public NormalisedCurve Normalise(bool cache = true)
        {
            if (cache)
            {
                return cachedNormalisedCurve ??= new NormalisedCurve(this);
            }

            return new NormalisedCurve(this);
        }
    }
}