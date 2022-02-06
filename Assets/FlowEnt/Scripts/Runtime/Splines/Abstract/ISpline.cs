using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public interface ISpline
    {
        public Vector3 GetPoint(float t);
    }
}