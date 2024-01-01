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
        private enum FieldsEnum
        {
            displayName,
            startTime,
        }

        private static Queue clipboard;
        public Queue Clipboard { get => clipboard; set => clipboard = value; }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = base.GetPropertyHeight(property, label);
            if (!property.isExpanded)
            {
                return height;
            }

            height += FlowEntConstants.SpacedSingleLineHeight;

            height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative(FieldsEnum.displayName.ToString()), true);
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
            contextMenu.AddListCrud(parentProperty, parentProperty.GetArrayElementIndex(property), "Queue", this);
            contextMenu.AddSeparator(string.Empty);
            contextMenu.AddShowDisplayName(property);
        }

        protected override void Draw(ref Rect position, SerializedProperty property)
        {
            FlowEntEditorGUILayout.PropertyField(ref position, property.FindPropertyRelative(FieldsEnum.displayName.ToString()));
            FlowEntEditorGUILayout.PropertyField(ref position, property.FindPropertyRelative(FieldsEnum.startTime.ToString()));
        }

        protected override void OnAdd(Rect buttonRect, ReorderableList list)
        {
            GenericMenu context = new();
            context.AddItem(new GUIContent("Flow"), false, () => list.Add(new FlowBuilder()));
            context.AddItem(new GUIContent("Tween"), false, () => list.Add(new TweenBuilder()));
            context.AddItem(new GUIContent("Echo"), false, () => list.Add(new EchoBuilder()));
            buttonRect.y += 3f;
            context.DropDown(buttonRect);
        }
    }
}