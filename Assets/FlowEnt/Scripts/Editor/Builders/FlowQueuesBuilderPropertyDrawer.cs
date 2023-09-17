using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(FlowBuilder.QueueListBuilder))]
    public class FlowQueuesBuilderPropertyDrawer : AbstractListBuilderPropertyDrawer<FlowBuilder.QueueListBuilder.QueueBuilder>
    {
        protected override Rect Draw(Rect position, SerializedProperty property)
        {
            DrawButton(position, "Add queue", () => GetData(property).AddedItemTypes.Enqueue(new FlowBuilder.QueueListBuilder.QueueBuilder()));
            return position;
        }
    }
}
