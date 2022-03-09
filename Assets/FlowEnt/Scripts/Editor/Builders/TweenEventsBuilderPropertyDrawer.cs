using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(TweenEventsBuilder))]
    public class TweenEventsBuilderPropertyDrawer : AbstractEventsBuilderPropertyDrawer<TweenEventsBuilderPropertyDrawer.PropertiesEnum>
    {
        public enum PropertiesEnum
        {
            onStarting,
            onStarted,
            onUpdating,
            onUpdated,
            onLoopCompleted,
            onCompleting,
            onCompleted,
        }
    }
}
