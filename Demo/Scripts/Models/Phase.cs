using System;
using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Demo
{
    [Serializable]
    public class Phase
    {
        [SerializeField]
        private List<Transform> objects;
        public List<Transform> Objects => objects;
    }
}
