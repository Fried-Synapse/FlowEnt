using System;
using System.Collections.Generic;
using System.Linq;
using FriedSynapse.FlowEnt.Reflection;
using UnityEditor;
using UnityEngine;
using ShaderPropertyType = UnityEditor.ShaderUtil.ShaderPropertyType;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(MaterialBuilder), true)]
    public class MaterialBuilderPropertyDrawer : PropertyDrawer
    {
        private enum FieldsEnum
        {
            type,
            predefinedMaterial,
            gameObjectWithInstance,
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => FlowEntConstants.SpacedSingleLineHeight + EditorGUIUtility.singleLineHeight;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = EditorGUIUtility.singleLineHeight;
            SerializedProperty typeProperty = property.FindPropertyRelative(FieldsEnum.type.ToString());
            EditorGUI.PropertyField(position, typeProperty);

            position.y += FlowEntConstants.SpacedSingleLineHeight;
            FieldsEnum itemFieldEnum = (MaterialBuilder.MaterialType)typeProperty.enumValueIndex switch
            {
                MaterialBuilder.MaterialType.Instance => FieldsEnum.gameObjectWithInstance,
                MaterialBuilder.MaterialType.Predefined => FieldsEnum.predefinedMaterial,
                _ => throw new ArgumentOutOfRangeException()
            };
            SerializedProperty itemProperty = property.FindPropertyRelative(itemFieldEnum.ToString());
            EditorGUI.PropertyField(position, itemProperty, label);
        }
    }

    [CustomPropertyDrawer(typeof(MaterialBuilderWithProperty), true)]
    public class MaterialBuilderWithPropertyPropertyDrawer : MaterialBuilderPropertyDrawer
    {
        private enum FieldsEnum
        {
            propertyId,
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => base.GetPropertyHeight(property, label) + FlowEntConstants.SpacedSingleLineHeight;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnGUI(position, property, label);

            position.height = EditorGUIUtility.singleLineHeight;
            MaterialBuilder materialBuilder = property.GetValue<MaterialBuilder>();
            position.y += base.GetPropertyHeight(property, label) + FlowEntConstants.DrawerSpacing;
            label = new GUIContent("Property");
            SerializedProperty propertyIdProperty = property.FindPropertyRelative(FieldsEnum.propertyId.ToString());
            ShaderPropertyType[] propertyTypes = GetPropertyType(materialBuilder);

            if (!TryGetMaterialProperties(materialBuilder.Build(), propertyTypes, out List<string> properties))
            {
                GUIContent warningContent = new(Icon.Warning)
                {
                    text = "Missing material or shader"
                };
                EditorGUI.LabelField(position, label, warningContent);
                return;
            }

            if (properties.Count == 0)
            {
                GUIContent warningContent = new(Icon.Warning)
                {
                    text =
                        $"No properties of type [{string.Join(", ", propertyTypes.Select(p => p.ToString()).ToArray())}]"
                };
                EditorGUI.LabelField(position, label, warningContent);
                return;
            }

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

        private ShaderPropertyType[] GetPropertyType(MaterialBuilder materialBuilder)
        {
            return materialBuilder switch
            {
                MaterialBuilderWithProperty<int> => new[] { ShaderPropertyType.Int },
                MaterialBuilderWithProperty<float> => new[] { ShaderPropertyType.Float, ShaderPropertyType.Range },
                MaterialBuilderWithProperty<Color> => new[] { ShaderPropertyType.Color },
                MaterialBuilderWithProperty<Vector2> => new[] { ShaderPropertyType.Vector },
                MaterialBuilderWithProperty<Vector4> => new[] { ShaderPropertyType.Vector },
                MaterialBuilderWithProperty<Texture> => new[] { ShaderPropertyType.TexEnv },
                MaterialBuilderWithProperty => (ShaderPropertyType[])Enum.GetValues(typeof(ShaderPropertyType)),
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