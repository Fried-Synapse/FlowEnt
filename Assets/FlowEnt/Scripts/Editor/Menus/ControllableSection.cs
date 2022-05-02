using System;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    internal abstract class AbstractControllableSection
    {
        protected AbstractControllableSection(IControllable controllable)
        {
            Controllable = controllable;
        }

        protected static GUILayoutOption ButtonWidth { get; } = GUILayout.Width(EditorGUIUtility.singleLineHeight);
        public IControllable Controllable { get; }
        protected bool IsBuilding => (Controllable.PlayState & PlayState.Building) == Controllable.PlayState;
        protected float? timeScale;
        protected float? maxTimeScale;
        protected float Indent => EditorGUI.indentLevel * EditorGUIUtility.singleLineHeight;

        protected virtual void OnPrevFrame() => Controllable.ChangeFrame(-1);
        protected virtual void OnPlay() => Controllable.Resume();
        protected virtual void OnPause() => Controllable.Pause();
        protected virtual void OnNextFrame() => Controllable.ChangeFrame(1);
        protected virtual void OnStop() => Controllable.Stop();
    }

    internal class PreviewControllableSection : InspectorControllableSection
    {
        public PreviewControllableSection(IControllable controllable) : base(controllable)
        {
            if (!(controllable is AbstractAnimation animation))
            {
                throw new ArgumentException("Preview can only be applied to animations.");
            }
            this.animation = animation;
        }

        protected readonly AbstractAnimation animation;

        internal override void Show()
        {
            if (!IsBuilding)
            {
                Rect rect = GUILayoutUtility.GetRect(new GUIContent(""), GUIStyle.none, GUILayout.Height(0));
                rect.height = FlowEntConstants.RectLineHeight;
                rect.xMin += Indent;
                ColorUtility.TryParseHtmlString(FlowEntConstants.Preview, out Color previewColour);
                EditorGUI.DrawRect(rect, previewColour);
            }

            base.Show();

            //TODO add controls for tween and info for others
        }

        protected override void OnPlay()
        {
            if (!IsBuilding)
            {
                animation.Resume();
            }
            else
            {
                PreviewController.Start(new PreviewOptions(animation));
            }
        }
        protected override void OnStop()
        {
            PreviewController.Stop();
        }
    }

    internal class InspectorControllableSection : AbstractControllableSection
    {
        public InspectorControllableSection(IControllable controllable) : base(controllable)
        {
        }

        internal virtual void Show()
        {
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Space(Indent + 10);
                //TODO maybe disable the buttons if they can't be used?

                GUI.enabled = !IsBuilding;
                if (GUILayout.Button(Icon.PrevFrame, Icon.Style, ButtonWidth))
                {
                    OnPrevFrame();
                }
                GUI.enabled = true;

                if (Controllable.PlayState == PlayState.Playing)
                {
                    if (GUILayout.Button(Icon.Pause, Icon.Style, ButtonWidth))
                    {
                        OnPause();
                    }
                }
                else
                {
                    if (GUILayout.Button(Icon.Play, Icon.Style, ButtonWidth))
                    {
                        OnPlay();
                    }
                }

                GUI.enabled = !IsBuilding;
                if (GUILayout.Button(Icon.NextFrame, Icon.Style, ButtonWidth))
                {
                    OnNextFrame();
                }

                if (GUILayout.Button(Icon.Stop, Icon.Style, ButtonWidth))
                {
                    OnStop();
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
