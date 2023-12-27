using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(TweenEventsBuilder))]
    public class TweenEventsBuilderPropertyDrawer : AbstractEventsBuilderPropertyDrawer<TweenEventsBuilderPropertyDrawer.FieldsEnum>
    {
        public enum FieldsEnum
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
