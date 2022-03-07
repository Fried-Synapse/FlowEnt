using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(FlowBuilder.QueueList))]
    public class FlowQueuesBuilderPropertyDrawer : AbstractListBuilderPropertyDrawer<FlowBuilder.QueueList.Queue>
    {
        protected override Rect Draw(Rect position, SerializedProperty property)
        {
            DrawButton(position, "Add queue", () => AddItem(new FlowBuilder.QueueList.Queue()));
            return position;
        }
    }
}
