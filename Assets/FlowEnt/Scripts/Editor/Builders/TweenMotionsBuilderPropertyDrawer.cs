using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(TweenMotionsBuilder))]
    public class TweenMotionsBuilderPropertyDrawer : AbstractListBuilderPropertyDrawer<AbstractTweenMotionBuilder>
    {
        protected override Rect Draw(Rect position, SerializedProperty property)
        {
            DrawButton(position, "Add motion", () => MotionPickerWindow.Show<AbstractTweenMotionBuilder>(AddItem));
            return position;
        }
    }
}
