using System;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class InspectorControllableSection
    {
        public InspectorControllableSection(IControllable controllable)
        {
            Controllable = controllable;
        }

        protected const float SideSpacing = 5f;
        protected static GUILayoutOption ButtonWidth { get; } = GUILayout.Width(EditorGUIUtility.singleLineHeight);
        public IControllable Controllable { get; }
        protected bool IsBuilding => (Controllable.PlayState & PlayState.Building) == Controllable.PlayState;
        protected float? timeScale;
        protected float? maxTimeScale;
        protected float Indent => EditorGUI.indentLevel * EditorGUIUtility.singleLineHeight;
        protected float PreviewTime { get; set; }

        protected virtual void OnPrevFrame() => Controllable.ChangeFrame(-1);
        protected virtual void OnPlay() => Controllable.Resume();
        protected virtual void OnPause() => Controllable.Pause();
        protected virtual void OnReplay() => ((AbstractAnimation)Controllable).Restart();
        protected virtual void OnNextFrame() => Controllable.ChangeFrame(1);
        protected virtual void OnStop() => Controllable.Stop();
        protected virtual void ShowExtraControls() { }

        public virtual void Show()
        {
            ShowControls();
            ShowTimeScale();
        }

        private void ShowControls()
        {
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Space(Indent + 10);

                GUI.enabled = !IsBuilding;
                if (GUILayout.Button(Icon.PrevFrame, Icon.Style, ButtonWidth))
                {
                    OnPrevFrame();
                }
                GUI.enabled = true;

                switch (Controllable.PlayState)
                {
                    case PlayState.Playing:
                        if (GUILayout.Button(Icon.Pause, Icon.Style, ButtonWidth))
                        {
                            OnPause();
                        }
                        break;
                    case PlayState.Finished:
                        if (GUILayout.Button(Icon.Replay, Icon.Style, ButtonWidth))
                        {
                            OnReplay();
                        }
                        break;
                    default:
                        if (GUILayout.Button(Icon.Play, Icon.Style, ButtonWidth))
                        {
                            OnPlay();
                        }
                        break;
                }

                if (GUILayout.Button(Icon.NextFrame, Icon.Style, ButtonWidth))
                {
                    OnNextFrame();
                }

                GUI.enabled = !IsBuilding;
                if (GUILayout.Button(Icon.Stop, Icon.Style, ButtonWidth))
                {
                    OnStop();
                }
                GUI.enabled = true;

                ShowExtraControls();
            }
            EditorGUILayout.EndHorizontal();
        }

        protected void ShowTimeScale()
        {
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Space(Indent + 10);
                int indentLevel = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;

                if (timeScale == null)
                {
                    timeScale = Controllable.TimeScale;
                }
                if (maxTimeScale == null)
                {
                    maxTimeScale = timeScale * 2f;
                }
                EditorGUILayout.LabelField("Time scale", GUILayout.ExpandWidth(false), GUILayout.Width(70f));
                timeScale = EditorGUILayout.Slider(timeScale.Value, 0f, maxTimeScale.Value);
                EditorGUILayout.LabelField("Max", GUILayout.ExpandWidth(false), GUILayout.Width(30f));
                maxTimeScale = EditorGUILayout.FloatField(maxTimeScale.Value, GUILayout.ExpandWidth(false), GUILayout.Width(30f));
                Controllable.TimeScale = timeScale.Value;

                EditorGUILayout.Space(SideSpacing, false);
                EditorGUI.indentLevel = indentLevel;
            }
            EditorGUILayout.EndHorizontal();
        }
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

        protected override void OnPlay()
        {
            if (IsBuilding)
            {
                StartPreview();
            }
            else
            {
                animation.Resume();
            }
        }

        protected override void OnReplay()
        {
            PreviewController.Reset();
        }

        protected override void OnNextFrame()
        {
            if (IsBuilding)
            {
                StartPreview();
                animation.Pause();
            }
            base.OnNextFrame();
        }

        protected override void OnStop()
        {
            PreviewController.Stop();
        }

        private void StartPreview()
        {
            PreviewController.Start(new PreviewOptions(animation)
            {
                OnUpdate = () => PreviewTime = animation.GetTime(),
                OnStop = () => PreviewTime = 0
            });
        }

        public override void Show()
        {
            if (!IsBuilding)
            {
                //HACK this creates the red space
                Rect rect = GUILayoutUtility.GetRect(new GUIContent(""), GUIStyle.none, GUILayout.Height(0));
                rect.height = FlowEntConstants.RectLineHeight * 2;
                rect.xMin += Indent + SideSpacing;
                rect.xMax -= SideSpacing;
                ColorUtility.TryParseHtmlString(FlowEntConstants.Preview, out Color previewColour);
                EditorGUI.DrawRect(rect, previewColour);
            }
            else
            {
                //HACK this adds extra space because of the red space
                EditorGUILayout.Space(0, false);
            }

            base.Show();
        }

        protected override void ShowExtraControls()
        {
            if (Controllable is Tween tween)
            {
                EditorGUILayout.LabelField("Time elapsed", PreviewTime.ToString());
            }
            else
            {
                EditorGUILayout.LabelField("Time elapsed", PreviewTime.ToString());
            }
        }
    }
}
