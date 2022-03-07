using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(EchoMotionsBuilder))]
    public class EchoMotionsBuilderPropertyDrawer : AbstractListBuilderPropertyDrawer<AbstractEchoMotionBuilder>
    {
        protected override Rect Draw(Rect position, SerializedProperty property)
        {
            DrawButton(position, "Add motion", () => MotionPickerWindow.Show<AbstractEchoMotionBuilder>(AddItem));
            return position;
        }
    }
}
