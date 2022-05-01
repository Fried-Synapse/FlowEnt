using System;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public class ControllableSection
    {
        public ControllableSection(IControllable controllable)
        {
            Controllable = controllable;
        }

        private static GUILayoutOption ButtonWidth { get; } = GUILayout.Width(EditorGUIUtility.singleLineHeight);
        public IControllable Controllable { get; }
        private float? timeScale;
        private float? maxTimeScale;

        internal void ShowControls(bool isPreview = false)
        {
            float indent = EditorGUI.indentLevel * EditorGUIUtility.singleLineHeight;
            Rect rect = EditorGUILayout.BeginHorizontal();
            if (isPreview)
            {
                if (!(Controllable is AbstractAnimation animation))
                {
                    throw new ArgumentException("Preview can only be applied to animations.");
                }
                isPreview = animation.PlayState == PlayState.Waiting
                    || animation.PlayState == PlayState.Playing
                    || animation.PlayState == PlayState.Paused;

                rect.height = FlowEntConstants.RectLineHeight;
                rect.xMin += indent;
                ColorUtility.TryParseHtmlString(FlowEntConstants.Preview, out Color previewColour);
                EditorGUI.DrawRect(rect, previewColour);
            }

            {
                GUILayout.Space(indent + 10);
                //TODO maybe disable the buttons if they can't be used?

                if (GUILayout.Button(Icon.PrevFrame, Icon.Style, ButtonWidth))
                {
                    Controllable.ChangeFrame(-1);
                }

                if (Controllable.PlayState == PlayState.Playing)
                {
                    if (GUILayout.Button(Icon.Pause, Icon.Style, ButtonWidth))
                    {
                        Controllable.Pause();
                    }
                }
                else
                {
                    if (GUILayout.Button(Icon.Play, Icon.Style, ButtonWidth))
                    {
                        void play()
                        {
                            if (!isPreview)
                            {
                                animation.Start();
                                return;
                            }
                            Controllable.Resume();
                        }
                        play();
                    }
                }

                if (GUILayout.Button(Icon.NextFrame, Icon.Style, ButtonWidth))
                {
                    Controllable.ChangeFrame(1);
                }

                GUI.enabled = isPreview;
                if (GUILayout.Button(Icon.Stop, Icon.Style, ButtonWidth))
                {
                    Controllable.Stop();
                }
                GUI.enabled = true;

                if (timeScale == null)
                {
                    timeScale = Controllable.TimeScale;
                }
                if (maxTimeScale == null)
                {
                    maxTimeScale = timeScale * 2f;
                }
                int indentLevel = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;
                EditorGUILayout.LabelField("Time scale", GUILayout.ExpandWidth(false), GUILayout.Width(70f));
                timeScale = EditorGUILayout.Slider(timeScale.Value, 0f, maxTimeScale.Value);
                EditorGUILayout.LabelField("Max", GUILayout.ExpandWidth(false), GUILayout.Width(30f));
                maxTimeScale = EditorGUILayout.FloatField(maxTimeScale.Value, GUILayout.ExpandWidth(false), GUILayout.Width(30f));
                Controllable.TimeScale = timeScale.Value;
                EditorGUILayout.Space(5);
                EditorGUI.indentLevel = indentLevel;
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
