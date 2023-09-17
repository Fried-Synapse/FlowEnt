using UnityEditor;
using UnityEngine;
using Queue = FriedSynapse.FlowEnt.FlowBuilder.QueueList.Queue;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(Queue))]
    public class FlowQueueAnimationBuilderPropertyDrawer : AbstractListBuilderPropertyDrawer<IAbstractAnimationBuilder>, ICrudable<Queue>
    {
        private static Queue clipboard;
        public Queue Clipboard { get => clipboard; set => clipboard = value; }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = base.GetPropertyHeight(property, label);
            if (!property.isExpanded)
            {
                return height;
            }

            if (property.FindPropertyRelative(IdentifiableBuilderFields.IsDisplayNameEnabled).boolValue)
            {
                height += FlowEntConstants.SpacedSingleLineHeight;
            }
            return height;
        }
        protected override GUIContent GetLabel(SerializedProperty property, GUIContent label)
        {
            int index = int.Parse(label.text.Split(' ')[1]);
            Queue queue = property.GetValue<Queue>();
            label.text = $"Queue {index}{(string.IsNullOrEmpty(queue.DisplayName) ? "" : $" - {queue.DisplayName}")}";
            return label;
        }

        protected override void DrawMenu(Rect position, SerializedProperty property)
        {
            Rect menuPosition = position;
            const float menuWidth = 20f;
            menuPosition.x = position.xMax - (menuWidth / 2f) - 10;
            menuPosition.width = menuWidth;
            menuPosition.height = EditorGUIUtility.singleLineHeight;
            if (GUI.Button(menuPosition, Icon.Menu, Icon.Style))
            {
                SerializedProperty parentProperty = property.GetParentArray();

                GenericMenu context = new GenericMenu();
                FlowEntEditorGUILayout.ShowListCrud(context, parentProperty, parentProperty.GetArrayElementIndex(property), "Queue", this);
                context.AddSeparator(string.Empty);
                SerializedProperty isNameEnabledProperty = property.FindPropertyRelative(IdentifiableBuilderFields.IsDisplayNameEnabled);
                void showRename()
                {
                    isNameEnabledProperty.boolValue = !isNameEnabledProperty.boolValue;
                    isNameEnabledProperty.serializedObject.ApplyModifiedProperties();
                }
                context.AddItem(new GUIContent("Show Rename"), showRename, false, isNameEnabledProperty.boolValue);
                context.ShowAsContext();
            }
        }

        protected override Rect Draw(Rect position, SerializedProperty property)
        {
            if (property.FindPropertyRelative(IdentifiableBuilderFields.IsDisplayNameEnabled).boolValue)
            {
                position.height = EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(position, property.FindPropertyRelative(IdentifiableBuilderFields.DisplayName));
                position.y += FlowEntConstants.SpacedSingleLineHeight;
            }
            DrawButton(position, "Add animation", () => ShowAddAnimation(EditorGUI.IndentedRect(position), property));
            return position;
        }

        private void ShowAddAnimation(Rect position, SerializedProperty property)
        {
            Queue queue = property.GetValue<Queue>();
            GenericMenu context = new GenericMenu();
            context.AddItem(new GUIContent("Flow"), false, () => queue.Items.Add(new FlowBuilder()));
            context.AddItem(new GUIContent("Tween"), false, () =>
            queue.Items.Add(new TweenBuilder()));
            context.AddItem(new GUIContent("Echo"), false, () => queue.Items.Add(new EchoBuilder()));
            position.y += 3f;
            context.DropDown(position);
        }
    }
}
