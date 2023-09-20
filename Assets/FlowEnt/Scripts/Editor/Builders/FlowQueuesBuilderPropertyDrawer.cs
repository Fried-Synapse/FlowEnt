using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(FlowBuilder.QueueList))]
    public class FlowQueuesBuilderPropertyDrawer : AbstractListBuilderPropertyDrawer<FlowBuilder.QueueList.Queue>
    {
        protected override void OnAdd(ReorderableList list, Rect buttonRect, SerializedProperty property)
        {
            GetData(property).AddedItemTypes.Enqueue(new FlowBuilder.QueueList.Queue());
        }
    }
}