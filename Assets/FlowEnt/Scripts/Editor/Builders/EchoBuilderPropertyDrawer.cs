using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(EchoBuilder))]
    public class EchoBuilderPropertyDrawer : AbstractAnimationBuilderPropertyDrawer
    {
        private float previewTime;
        private Echo previewEcho;
        protected override void DrawControls(Rect position, SerializedProperty property)
        {
            float playButtonWidth = EditorGUIUtility.singleLineHeight;
            Rect playButtonPosition = position;
            playButtonPosition.width = playButtonWidth;
            if (previewEcho?.PlayState == PlayState.Playing)
            {
                if (GUI.Button(playButtonPosition, Icon.Pause, Icon.Style))
                {
                    previewEcho.Pause();
                }
            }
            else
            {
                if (GUI.Button(playButtonPosition, Icon.Play, Icon.Style))
                {
                    previewEcho = property.GetValue<EchoBuilder>().Build(FlowEntEditorController.Instance);
                    previewEcho.OnUpdating(t =>
                    {
                        previewTime += t;
                        EditorUtility.SetDirty(property.serializedObject.targetObject);
                    });
                    previewEcho.SetFieldValue("updateController", FlowEntEditorController.Instance);
                    previewEcho.Start();
                }
            }

            Rect progressPosition = position;
            progressPosition.width -= playButtonWidth;
            progressPosition.x += playButtonWidth;
            EditorGUI.LabelField(progressPosition, "Time elapsed", previewTime.ToString());
        }

        protected override void OnScopeChanged()
        {
            previewEcho = null;
            previewTime = 0;
        }
    }
}
