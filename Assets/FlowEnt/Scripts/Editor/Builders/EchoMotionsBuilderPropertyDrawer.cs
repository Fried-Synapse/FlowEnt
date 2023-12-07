using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(EchoMotionsBuilder))]
    public class EchoMotionsBuilderPropertyDrawer : AbstractListBuilderPropertyDrawer<AbstractEchoMotionBuilder>
    {
        protected override void OnAdd(Rect buttonRect, ReorderableList list)
        {
            MotionPickerWindow.Show<AbstractEchoMotionBuilder>(list.Add);
        }
    }
}