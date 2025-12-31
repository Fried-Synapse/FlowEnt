using System;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    [UxmlElement]
    internal partial class FriedSlider : VisualElement
    {
        public class EventData
        {
            public EventData(float oldValue, float newValue)
            {
                OldValue = oldValue;
                NewValue = newValue;
            }

            public float OldValue { get; }
            public float NewValue { get; }
        }

        public FriedSlider()
        {
            this.LoadUxml();
            Background = this.Query<VisualElement>("background").First();
            Fill = this.Query<VisualElement>("fill").First();
            ValueText = this.Query<TextField>("value").First();
            Bind();
        }

        protected VisualElement Background { get; }
        protected VisualElement Fill { get; }
        protected TextField ValueText { get; }

        protected float minValue;
        [UxmlAttribute("minValue")]
        public float MinValue
        {
            get => minValue;
            set
            {
                minValue = value;
                SetValue(this.value);
            }
        }

        protected float maxValue = 1;
        [UxmlAttribute("maxValue")]
        public float MaxValue
        {
            get => maxValue;
            set
            {
                maxValue = value;
                SetValue(this.value);
            }
        }

        protected float value;
        [UxmlAttribute("value")]
        public float Value
        {
            get => value;
            set => SetValue(value);
        }

        internal Action<EventData> OnValueChanging { get; set; }
        internal Action<EventData> OnValueChanged { get; set; }

        protected virtual void Bind()
        {
            Background.RegisterCallback<PointerDownEvent>(StartValueChange);
            ValueText.RegisterValueChangedCallback(e =>
            {
                if (!float.TryParse(e.newValue, out float newValue))
                {
                    e.StopPropagation();
                    return;
                }

                OnTextValueChaging(newValue);
                SetValue(newValue);
            });
        }

        protected virtual void OnTextValueChaging(float newValue)
        {
        }

        private void SetValue(float newValue)
        {
            float oldValue = value;
            OnValueChanging?.Invoke(new EventData(oldValue, newValue));
            SetValueWithoutNotify(newValue);
            OnValueChanged?.Invoke(new EventData(oldValue, value));
        }

        public void SetValueWithoutNotify(float newValue)
        {
            value = Mathf.Clamp(newValue, MinValue, MaxValue);
            Fill.style.width = new StyleLength(new Length(Mathf.InverseLerp(MinValue, MaxValue, value) * 100f, LengthUnit.Percent));
            ValueText.SetValueWithoutNotify(value.ToString("0.###"));
        }

        private void StartValueChange(PointerDownEvent evt)
        {
            OnPointerMove(evt);
            Background.CaptureMouse();
            Background.RegisterCallback<PointerMoveEvent>(OnPointerMove);
            Background.RegisterCallback<PointerUpEvent>(_ =>
            {
                Background.UnregisterCallback<PointerMoveEvent>(OnPointerMove);
                Background.ReleaseMouse();
            });
        }

        private void OnPointerMove(IPointerEvent evt)
        {
            float unitValue = Mathf.Clamp01((evt.position.x - Background.worldBound.xMin) / Background.worldBound.width);
            SetValue(Mathf.Lerp(MinValue, MaxValue, unitValue));
        }
    }
}