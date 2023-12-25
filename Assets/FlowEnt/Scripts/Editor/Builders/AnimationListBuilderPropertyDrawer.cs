using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(AnimationListBuilder))]
    public class AnimationListBuilderPropertyDrawer : AbstractListBuilderPropertyDrawer<IAbstractAnimationBuilder>,
        ICrudable<AnimationListBuilder>
    {
        private static AnimationListBuilder clipboard;

        public AnimationListBuilder Clipboard
        {
            get => clipboard;
            set => clipboard = value;
        }

        protected override void AddItemsToContextMenu(GenericMenu contextMenu, SerializedProperty property)
        {
            contextMenu.AddCrud(property, "List", this);
        }

        protected override void OnAdd(Rect buttonRect, ReorderableList list)
        {
            GenericMenu context = new GenericMenu();
            context.AddItem(new GUIContent("Flow"), false, () => list.Add(new FlowBuilder()));
            context.AddItem(new GUIContent("Tween"), false, () => list.Add(new TweenBuilder()));
            context.AddItem(new GUIContent("Echo"), false, () => list.Add(new EchoBuilder()));
            buttonRect.y += 3f;
            context.DropDown(buttonRect);
        }
    }
}