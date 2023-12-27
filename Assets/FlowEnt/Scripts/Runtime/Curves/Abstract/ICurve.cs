using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public interface ICurve
    {
        public Vector3 GetPoint(float t);
    }
}