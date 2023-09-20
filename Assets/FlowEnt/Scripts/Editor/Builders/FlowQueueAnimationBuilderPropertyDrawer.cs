using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Queue = FriedSynapse.FlowEnt.FlowBuilder.QueueList.Queue;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(Queue))]
    public class FlowQueueAnimationBuilderPropertyDrawer : AbstractListBuilderPropertyDrawer<IAbstractAnimationBuilder>,
        ICrudable<Queue>
    {
        private static Queue clipboard;

        public Queue Clipboard
        {
            get => clipboard;
            set => clipboard = value;
        }

        private const string StartTimeName = "startTime";

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = base.GetPropertyHeight(property, label);
            if (!property.isExpanded)
            {
                return height;
            }

            height += FlowEntConstants.SpacedSingleLineHeight;
            height += IdentifiableBuilderFields.GetDisplayNameHeight(property);
            return height;
        }

        protected override GUIContent GetLabel(SerializedProperty property, GUIContent label)
        {
            int index = int.Parse(label.text.Split(' ')[1]);
            Queue queue = property.GetValue<Queue>();
            label.text = $"Queue {index}{(string.IsNullOrEmpty(queue.DisplayName) ? "" : $" - {queue.DisplayName}")}";
            return label;
        }

        protected override void AddItemsToContextMenu(GenericMenu contextMenu, SerializedProperty property)
        {
            SerializedProperty parentProperty = property.GetParentArray();
            FlowEntEditorGUILayout.ShowListCrud(contextMenu, parentProperty,
                parentProperty.GetArrayElementIndex(property), "Queue", this);
            contextMenu.AddSeparator(string.Empty);
            IdentifiableBuilderFields.DrawShowRename(property, contextMenu);
        }

        protected override void Draw(ref Rect position, SerializedProperty property)
        {
            IdentifiableBuilderFields.DrawDisplayName(ref position, property);
            FlowEntEditorGUILayout.PropertyFieldSingleLine(ref position, property.FindPropertyRelative(StartTimeName));
        }


        protected override void OnAdd(ReorderableList list, Rect buttonRect, SerializedProperty property)
        {
            Queue queue = property.GetValue<Queue>();
            GenericMenu context = new GenericMenu();
            context.AddItem(new GUIContent("Flow"), false, () => queue.Items.Add(new FlowBuilder()));
            context.AddItem(new GUIContent("Tween"), false, () => queue.Items.Add(new TweenBuilder()));
            context.AddItem(new GUIContent("Echo"), false, () => queue.Items.Add(new EchoBuilder()));
            buttonRect.y += 3f;
            context.DropDown(buttonRect);
        }
    }
}