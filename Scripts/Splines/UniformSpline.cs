using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public class UniformSpline<TUniformableSpline> : ISpline
        where TUniformableSpline : IUniformableSpline
    {
        public UniformSpline(TUniformableSpline uniformableSpline, float precision = 0.1f)
        {
            this.uniformableSpline = uniformableSpline;
            this.precision = precision;
            Init();
        }
        private TUniformableSpline uniformableSpline;
        private float precision;

        private void Init()
        {

        }

        public Vector3 GetPoint(float t)
        {
            return uniformableSpline.GetPoint(t);
        }
    }
}
