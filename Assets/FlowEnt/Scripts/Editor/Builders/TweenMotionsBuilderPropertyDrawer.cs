using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(TweenMotionsBuilder))]
    public class TweenMotionsBuilderPropertyDrawer : PropertyDrawer
    {
        private List<AbstractTweenMotionBuilder> NewBuilders { get; } = new List<AbstractTweenMotionBuilder>();
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => 100;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.isExpanded = EditorGUI.Foldout(FlowEntEditorGUILayout.GetRect(position, 0), property.isExpanded, label);

            if (!property.isExpanded)
            {
                return;
            }

            EditorGUI.indentLevel++;
            DrawMotions(position, property);
            EditorGUI.indentLevel--;
        }

        private void DrawMotions(Rect position, SerializedProperty property)
        {
            SerializedProperty motionsSerialisedProperty = property.FindPropertyRelative("motionsSerialised");
            List<AbstractTweenMotionBuilder> motions;
            try
            {
                motions = JsonConvert.DeserializeObject<List<AbstractTweenMotionBuilder>>(motionsSerialisedProperty.stringValue, JsonSettings.FullyTyped);
            }
            catch
            {
                //TODO don't overwrite the value is the deserialisation fails. Show a warning and give the option to overwrite
                motions = new List<AbstractTweenMotionBuilder>();
            }

            Rect buttonPosition = FlowEntEditorGUILayout.GetRect(position, 1);
            if (GUI.Button(buttonPosition, "Add motion"))
            {
                MotionPickerWindow.Show(AddMotion);
            }

            foreach (AbstractTweenMotionBuilder motion in NewBuilders)
            {
                motions.Add(motion);
            }
            NewBuilders.Clear();

            for (int i = 0; i < motions.Count; i++)
            {
                Rect propertyPosition = FlowEntEditorGUILayout.GetRect(position, i + 2);
                EditorGUI.LabelField(propertyPosition, motions[i].GetType().Name);
            }
            motionsSerialisedProperty.stringValue = JsonConvert.SerializeObject(motions, JsonSettings.FullyTyped);
        }

        private void AddMotion(AbstractTweenMotionBuilder builder)
            => NewBuilders.Add(builder);
    }
}
