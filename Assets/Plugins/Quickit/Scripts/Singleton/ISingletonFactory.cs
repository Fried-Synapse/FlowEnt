using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.Quickit
{
    public interface ISingletonFactory<TInstance>
    {
        public TInstance CreateInstance();
    }
}
