using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public abstract class AbstractIfAttributeDrawer : PropertyDrawer
    {
        protected bool HasValue(SerializedProperty property)
        {
            AbstractIfAttribute ifAttribute = (AbstractIfAttribute)attribute;
            SerializedProperty possibleParentArray = property.GetParent();
            if (possibleParentArray?.isArray == true)
            {
                property = possibleParentArray.GetParent();
            }

            SerializedProperty controlProperty = property.GetParent().FindPropertyRelative(ifAttribute.Field);

            if (controlProperty == null)
            {
                Debug.LogWarning(
                    $"[{ifAttribute.GetType().Name}] Cannot find control field with name \"{ifAttribute.Field}\" for \"{property.name}\".",
                    property.serializedObject.targetObject);
            }
            else
            {
                if (ifAttribute.HasValue(controlProperty.GetValue<object>()))
                {
                    return true;
                }
            }

            return false;
        }
    }
}