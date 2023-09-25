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
            FlowEntEditorGUILayout.ShowCrud(contextMenu, property, "List", this);
        }

        protected override void OnAdd(ReorderableList list, Rect buttonRect, SerializedProperty property)
        {
            AnimationListBuilder container = property.GetValue<AnimationListBuilder>();
            GenericMenu context = new GenericMenu();
            context.AddItem(new GUIContent("Flow"), false, () => container.Items.Add(new FlowBuilder()));
            context.AddItem(new GUIContent("Tween"), false, () => container.Items.Add(new TweenBuilder()));
            context.AddItem(new GUIContent("Echo"), false, () => container.Items.Add(new EchoBuilder()));
            buttonRect.y += 3f;
            context.DropDown(buttonRect);
        }
    }
}