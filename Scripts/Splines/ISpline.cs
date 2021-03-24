using UnityEngine;

namespace FlowEnt
{
    public interface ISpline
    {
        public Vector3 GetPoint(float t);
    }
}