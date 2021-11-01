using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    //TODO under development
    [Serializable]
    public class TweenBuilder
    {
        [SerializeField]
        private TweenOptions options;
        private TweenOptions Options => options;

        //TODO add events
        // [SerializeField]
        // private TweenEventsBuilder events;
        // private TweenEventsBuilder Events => events;

        public Tween Build()
        {
            throw new NotImplementedException();
        }
    }
}
