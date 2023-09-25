using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(EchoMotionsBuilder))]
    public class EchoMotionsBuilderPropertyDrawer : AbstractListBuilderPropertyDrawer<AbstractEchoMotionBuilder>
    {
        protected override void OnAdd(ReorderableList list, Rect buttonRect, SerializedProperty property)
        {
            MotionPickerWindow.Show<AbstractEchoMotionBuilder>(GetData(property).AddedItemTypes.Enqueue);
        }
    }
}