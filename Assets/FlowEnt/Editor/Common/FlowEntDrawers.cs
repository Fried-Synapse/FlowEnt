using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    internal static class FlowEntDrawers
    {
        internal static Rect GetRect(Rect position, int index)
            => new Rect(position.x, position.y + (index * FlowEntConstants.SpacedSingleLineHeight), position.width, FlowEntConstants.SingleLineHeight);

        internal static Rect GetRect(Rect position, float y, SerializedProperty property, out float yMax)
            => GetRect(position, y, EditorGUI.GetPropertyHeight(property, true), out yMax);

        internal static Rect GetRect(Rect position, out float yMax)
            => GetRect(position, position.y, FlowEntConstants.SpacedSingleLineHeight, out yMax);

        internal static Rect GetRect(Rect position, float y, out float yMax)
            => GetRect(position, y, FlowEntConstants.SpacedSingleLineHeight, out yMax);

        private static Rect GetRect(Rect position, float y, float height, out float yMax)
        {
            Rect rect = new Rect(position.x, y, position.width, height);
            yMax = rect.yMax;
            return rect;
        }
    }
}
