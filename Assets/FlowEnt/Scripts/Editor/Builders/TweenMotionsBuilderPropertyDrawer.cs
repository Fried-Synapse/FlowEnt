using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(TweenMotionsBuilder))]
    public class TweenMotionsBuilderPropertyDrawer : AbstractListBuilderPropertyDrawer<AbstractTweenMotionBuilder>
    {
        protected override void OnAdd(ReorderableList list, Rect buttonRect, SerializedProperty property)
        {
            MotionPickerWindow.Show<AbstractTweenMotionBuilder>(GetData(property).AddedItemTypes.Enqueue);
        }
    }
}