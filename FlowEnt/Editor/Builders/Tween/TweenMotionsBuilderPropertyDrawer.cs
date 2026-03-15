using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(TweenMotionsBuilder))]
    public class TweenMotionsBuilderPropertyDrawer : AbstractListBuilderPropertyDrawer<AbstractTweenMotionBuilder>
    {
        protected override void OnAdd(Rect buttonRect, ReorderableList list)
        {
            MotionPickerWindow.Show<AbstractTweenMotionBuilder>(list.Add);
        }
    }
}