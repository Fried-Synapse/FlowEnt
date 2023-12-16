using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FriedSynapse.FlowEnt.Reflection;
using UnityEditor;
using UnityEngine;
using ShaderPropertyType = UnityEditor.ShaderUtil.ShaderPropertyType;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(AbstractMaterialWithProperty), true)]
    public class MaterialWithPropertyPropertyDrawer : PropertyDrawer
    {
        private enum FieldsEnum
        {
            type,
            material,
            gameObject,
            propertyId,
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => FlowEntConstants.SpacedSingleLineHeight * 2 + EditorGUIUtility.singleLineHeight;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            AbstractMaterialWithProperty materialWithProperty = property.GetValue<AbstractMaterialWithProperty>();

            EditorGUI.BeginProperty(position, label, property);
            position.height = EditorGUIUtility.singleLineHeight;
            SerializedProperty typeProperty = property.FindPropertyRelative(FieldsEnum.type.ToString());
            EditorGUI.PropertyField(position, typeProperty);

            position.y += FlowEntConstants.SpacedSingleLineHeight;
            FieldsEnum itemFieldEnum = (AbstractMaterialWithProperty.MaterialType)typeProperty.enumValueIndex switch
            {
                AbstractMaterialWithProperty.MaterialType.Instance => FieldsEnum.gameObject,
                AbstractMaterialWithProperty.MaterialType.Predefined => FieldsEnum.material,
                _ => throw new ArgumentOutOfRangeException()
            };
            SerializedProperty itemProperty = property.FindPropertyRelative(itemFieldEnum.ToString());
            EditorGUI.PropertyField(position, itemProperty, label);

            position.y += FlowEntConstants.SpacedSingleLineHeight;
            label = new GUIContent("Property");
            SerializedProperty propertyIdProperty = property.FindPropertyRelative(FieldsEnum.propertyId.ToString());

            if (TryGetMaterialProperties(
                    materialWithProperty.InvokeMethod<AbstractMaterialWithProperty, Material>("GetMaterial"),
                    GetPropertyType(materialWithProperty),
                    out List<string> properties))
            {
                int index = properties.Select(Shader.PropertyToID).ToList()
                    .IndexOf(propertyIdProperty.intValue);
                if (index < 0 || index >= properties.Count)
                {
                    index = 0;
                }

                index = EditorGUI.Popup(position, label, index,
                    properties.Select(o => new GUIContent(o)).ToArray());
                propertyIdProperty.intValue = Shader.PropertyToID(properties[index]);
            }
            else
            {
                GUIContent warningContent = new GUIContent(Icon.Warning)
                {
                    text = "Missing material or shader"
                };
                EditorGUI.LabelField(position, label, warningContent);
            }

            EditorGUI.EndProperty();
        }

        private ShaderPropertyType[] GetPropertyType(AbstractMaterialWithProperty materialWithProperty)
        {
            return materialWithProperty switch
            {
                MaterialWithProperty<int> => new[] { ShaderPropertyType.Int },
                MaterialWithProperty<float> => new[] { ShaderPropertyType.Float, ShaderPropertyType.Range },
                MaterialWithProperty<Color> => new[] { ShaderPropertyType.Color },
                MaterialWithProperty<Vector2> => new[] { ShaderPropertyType.Vector },
                MaterialWithProperty<Vector4> => new[] { ShaderPropertyType.Vector },
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private bool TryGetMaterialProperties(Material material, ShaderPropertyType[] allowedPropertyType,
            out List<string> properties)
        {
            properties = new List<string>();
            if (material == null || material.shader == null)
            {
                return false;
            }

            int propertyCount = ShaderUtil.GetPropertyCount(material.shader);

            for (int i = 0; i < propertyCount; i++)
            {
                string propertyName = ShaderUtil.GetPropertyName(material.shader, i);
                ShaderPropertyType propertyType = ShaderUtil.GetPropertyType(material.shader, i);
                if (allowedPropertyType.Contains(propertyType))
                {
                    properties.Add(propertyName);
                }
            }

            return true;
        }
    }
}