using System;
using UnityEditor;
using UnityEngine;
using CurveType = FriedSynapse.FlowEnt.CurveBuilder.CurveType;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(CurveBuilder))]
    public class CurveBuilderPropertyDrawer : PropertyDrawer
    {
        public enum FieldsEnum
        {
            type,
            bezierPoints,
            splinePoints,
            normalise,
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => property.isExpanded
                ? (CurveType)property.FindPropertyRelative(FieldsEnum.type.ToString()).enumValueIndex switch
                {
                    CurveType.BezierCurve => FlowEntConstants.SpacedSingleLineHeight * 7,
                    CurveType.LinearSpline or
                        CurveType.BSpline or
                        CurveType.CatmullRomSpline or
                        CurveType.CubicSpline
                        => EditorGUI.GetPropertyHeight(property.FindPropertyRelative(FieldsEnum.splinePoints.ToString()), true)
                           + FlowEntConstants.SpacedSingleLineHeight * 3,
                    _ => throw new ArgumentOutOfRangeException()
                }
                : EditorGUIUtility.singleLineHeight;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.isExpanded = EditorGUI.Foldout(FlowEntEditorGUILayout.GetRect(position, 0), property.isExpanded,
                label, true, EditorStyles.foldoutHeader);

            if (!property.isExpanded)
            {
                return;
            }

            EditorGUI.indentLevel++;

            position.y += FlowEntConstants.SpacedSingleLineHeight;

            SerializedProperty typeProperty = property.FindPropertyRelative(FieldsEnum.type.ToString());
            FlowEntEditorGUILayout.PropertyField(ref position, typeProperty);

            FlowEntEditorGUILayout.PropertyField(ref position, property.FindPropertyRelative(FieldsEnum.normalise.ToString()));

            switch ((CurveType)typeProperty.enumValueIndex)
            {
                case CurveType.BezierCurve:
                    FlowEntEditorGUILayout.ForEachVisibleProperty(property.FindPropertyRelative(FieldsEnum.bezierPoints.ToString()), p =>
                    {
                        FlowEntEditorGUILayout.PropertyField(ref position, p);
                    });
                    break;
                case CurveType.LinearSpline:
                case CurveType.BSpline:
                case CurveType.CatmullRomSpline:
                case CurveType.CubicSpline:
                    FlowEntEditorGUILayout.PropertyField(ref position, property.FindPropertyRelative(FieldsEnum.splinePoints.ToString()));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            EditorGUI.indentLevel--;
        }
    }
}