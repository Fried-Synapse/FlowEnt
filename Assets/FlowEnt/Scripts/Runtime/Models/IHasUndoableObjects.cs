using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public interface IHasUndoableObjects
    {
#if UNITY_EDITOR
        public List<Object> GetUndoableObjects();
#endif
    }
}