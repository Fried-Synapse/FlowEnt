using System;
using System.Collections.Generic;
using System.Linq;
using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public abstract class AbstractAnimationBuilderPropertyDrawer<TAnimation, TMotion> : PropertyDrawer
        where TAnimation : AbstractAnimation
        where TMotion : IMotion
    {
        protected static class Icon
        {
            public static GUIContent Play = EditorGUIUtility.IconContent("PlayButton@2x", "Play");
            public static GUIContent Pause = EditorGUIUtility.IconContent("PauseButton@2x", "Pause");
            public static GUIContent Stop = EditorGUIUtility.IconContent("PreMatQuad@2x", "Pause");
            public static GUIStyle Style = new GUIStyle(EditorStyles.miniButton) { padding = new RectOffset(2, 2, 2, 2) };
        }

        private readonly List<string> visibleProperties = new List<string>{
            "options",
            "events",
            "motions",
        };

        private int? undoGroupId;
        protected TAnimation previewAnimation;
        protected abstract void DrawControls(Rect position, SerializedProperty property);
        protected abstract TAnimation Build(SerializedProperty property);
        protected abstract void OnAnimationUpdated(float t);
        protected virtual void Reset()
        {
            previewAnimation?.Stop();
            previewAnimation = null;
            if (undoGroupId != null)
            {
                Undo.RevertAllDownToGroup(undoGroupId.Value);
                undoGroupId = null;
            }
        }

        protected void RecordUndo()
        {
            if (undoGroupId != null)
            {
                return;
            }
            UnityEngine.Object[] objects = GetObjects(previewAnimation);
            if (objects.Length > 0)
            {
                undoGroupId = Undo.GetCurrentGroup();
                Undo.IncrementCurrentGroup();
                Undo.RecordObjects(objects, "Animation Preview");
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded)
            {
                return EditorGUIUtility.singleLineHeight;
            }

            float height = FlowEntConstants.SpacedSingleLineHeight * 2;
            ForEachVisibleProperty(property, p => height += EditorGUI.GetPropertyHeight(p, true));
            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.isExpanded = EditorGUI.Foldout(FlowEntEditorGUILayout.GetRect(position, 0), property.isExpanded, label);

            if (!property.isExpanded)
            {
                return;
            }

            EditorGUI.indentLevel++;
            DrawControls(FlowEntEditorGUILayout.GetRect(position, 1), property);
            using (EditorGUI.ChangeCheckScope check = new EditorGUI.ChangeCheckScope())
            {
                position.y += FlowEntConstants.SpacedSingleLineHeight * 2;
                ForEachVisibleProperty(property, p =>
                {
                    float height = EditorGUI.GetPropertyHeight(p, true) + FlowEntConstants.DrawerSpacing;
                    position.height = height;
                    EditorGUI.PropertyField(position, p, true);
                    position.y += height;
                });

                if (check.changed)
                {
                    Reset();
                }
            }
            EditorGUI.indentLevel--;
        }

        protected void DrawButtons(Rect position, SerializedProperty property)
        {
            position.width = EditorGUIUtility.singleLineHeight;
            position.x += 5;
            if (previewAnimation?.PlayState == PlayState.Playing)
            {
                if (GUI.Button(position, Icon.Pause, Icon.Style))
                {
                    previewAnimation.Pause();
                }
            }
            else
            {
                if (GUI.Button(position, Icon.Play, Icon.Style))
                {
                    if (previewAnimation?.PlayState == PlayState.Paused)
                    {
                        previewAnimation.Resume();
                    }
                    else
                    {
                        previewAnimation = Build(property);
                        previewAnimation.OnUpdated(t =>
                        {
                            OnAnimationUpdated(t);
                            EditorUtility.SetDirty(property.serializedObject.targetObject);
                        });
                        RecordUndo();
                        previewAnimation.Start();
                    }
                }
            }
            position.x += EditorGUIUtility.singleLineHeight;
            if (GUI.Button(position, Icon.Stop, Icon.Style))
            {
                Reset();
            }
        }

        private void ForEachVisibleProperty(SerializedProperty property, Action<SerializedProperty> predicate)
        {
            FlowEntEditorGUILayout.ForEachVisibleProperty(property, p =>
            {
                if (visibleProperties.Contains(p.name))
                {
                    predicate(p);
                }
            });
        }

        private UnityEngine.Object[] GetObjects(TAnimation animation)
        {
            TMotion[] motions = animation.GetFieldValue<TMotion[]>("motions");
            List<UnityEngine.Object> result = new List<UnityEngine.Object>();
            Type type = typeof(UnityEngine.Object);
            foreach (TMotion motion in motions)
            {
                List<UnityEngine.Object> allObjects = motion
                    .GetType()
                    .GetFields()
                    .Where(fi => type.IsAssignableFrom(fi.FieldType))
                    .Select(fi => (UnityEngine.Object)fi.GetValue(motion))
                    .ToList();

                allObjects.AddRange(motion
                    .GetType()
                    .GetProperties()
                    .Where(pi => type.IsAssignableFrom(pi.PropertyType))
                    .Select(pi => (UnityEngine.Object)pi.GetValue(motion)));

                IEnumerable<UnityEngine.Object> objects = allObjects
                    .Distinct()
                    .Where(o => o != null);

                result.AddRange(objects);
            }
            return result.ToArray();
        }
    }
}
