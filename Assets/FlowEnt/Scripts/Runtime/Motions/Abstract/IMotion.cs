using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Abstract
{
    public interface IMotion
    {
    }

    public interface IObjectMotion
    {
        public Object Object { get; }
    }
}