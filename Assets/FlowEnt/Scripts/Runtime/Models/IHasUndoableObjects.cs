using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public interface IHasUndoableObjects
    {
        public List<Object> GetUndoableObjects();
    }
}