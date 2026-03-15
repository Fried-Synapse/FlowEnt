using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(FlowBuilder.QueueList))]
    public class FlowQueuesBuilderPropertyDrawer : AbstractListBuilderPropertyDrawer<FlowBuilder.QueueList.Queue>
    {
        protected override void OnAdd(Rect buttonRect, ReorderableList list)
        {
            list.Add(new FlowBuilder.QueueList.Queue());
        }
    }
}