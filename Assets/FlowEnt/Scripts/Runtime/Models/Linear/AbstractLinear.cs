using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class AbstractLinear<TValue>
    {
        protected AbstractLinear(TValue start, TValue end)
        {
            this.start = start;
            this.end = end;
        }

        [SerializeField]
        protected TValue start;
        public TValue Start { get => start; set => start = value; }
        [SerializeField]
        protected TValue end;
        public TValue End { get => end; set => end = value; }
    }
}
