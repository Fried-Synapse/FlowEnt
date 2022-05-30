using System;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class FriedSlider : VisualElement
    {
        [Preserve]
        public new class UxmlFactory : UxmlFactory<FriedSlider, UxmlTraits> { }
        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlFloatAttributeDescription minValue = new UxmlFloatAttributeDescription { name = "minValue", };
            private readonly UxmlFloatAttributeDescription maxValue = new UxmlFloatAttributeDescription { name = "maxValue", defaultValue = 1 };
            private readonly UxmlFloatAttributeDescription value = new UxmlFloatAttributeDescription { name = "value", defaultValue = 0 };

            public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
            {
                base.Init(visualElement, bag, context);
                FriedSlider slider = visualElement as FriedSlider;
                slider.MinValue = minValue.GetValueFromBag(bag, context);
                slider.MaxValue = maxValue.GetValueFromBag(bag, context);
                slider.Value = value.GetValueFromBag(bag, context);
            }
        }

        public enum EventType
        {
            UserInput,
            Script
        }

        public class EventData
        {
            public EventData(float oldValue, float newValue, EventType type)
            {
                OldValue = oldValue;
                NewValue = newValue;
                Type = type;
            }

            public float OldValue { get; }
            public float NewValue { get; }
            public EventType Type { get; }
        }

        public FriedSlider()
        {
            this.LoadUxml();
            Background = this.Query<VisualElement>("background").First();
            Fill = this.Query<VisualElement>("fill").First();
            ValueText = this.Query<TextField>("value").First();
            Bind();
        }

        private bool wasValueTextInternallyChanged;
        protected VisualElement Background { get; }
        protected VisualElement Fill { get; }
        protected TextField ValueText { get; }

        protected float minValue;
        internal float MinValue
        {
            get => minValue;
            set
            {
                minValue = value;
                SetValue(this.value, EventType.Script);
            }
        }

        protected float maxValue;
        internal float MaxValue
        {
            get => maxValue;
            set
            {
                maxValue = value;
                SetValue(this.value, EventType.Script);
            }
        }

        protected float value;
        internal float Value
        {
            get => value;
            set => SetValue(value, EventType.Script);
        }

        internal Action<EventData> OnValueChanging { get; set; }
        internal Action<EventData> OnValueChanged { get; set; }

        protected virtual void Bind()
        {
            Background.RegisterCallback<PointerDownEvent>(StartValueChange);
            ValueText.RegisterValueChangedCallback(e =>
            {
                if (wasValueTextInternallyChanged)
                {
                    wasValueTextInternallyChanged = false;
                    e.StopPropagation();
                    return;
                }

                if (!float.TryParse(e.newValue, out float newValue))
                {
                    e.StopPropagation();
                    return;
                }

                SetValue(newValue, EventType.UserInput);
            });
        }

        private void SetValue(float newValue, EventType type)
        {
            float oldValue = value;
            OnValueChanging?.Invoke(new EventData(oldValue, newValue, type));
            value = Mathf.Clamp(newValue, MinValue, MaxValue);
            Fill.style.width = new StyleLength(new Length(Mathf.InverseLerp(MinValue, MaxValue, value) * 100f, LengthUnit.Percent));
            wasValueTextInternallyChanged = true;
            ValueText.value = value.ToString("0.###");
            OnValueChanged?.Invoke(new EventData(oldValue, value, type));
        }

        private void StartValueChange(PointerDownEvent evt)
        {
            OnPointerMove(evt);
            Background.CaptureMouse();
            Background.RegisterCallback<PointerMoveEvent>(OnPointerMove);
            Background.RegisterCallback<PointerUpEvent>((_) =>
            {
                Background.UnregisterCallback<PointerMoveEvent>(OnPointerMove);
                Background.ReleaseMouse();
            });
        }

        private void OnPointerMove(IPointerEvent evt)
        {
            float unitValue = Mathf.Clamp01((evt.position.x - Background.worldBound.xMin) / Background.worldBound.width);
            SetValue(Mathf.Lerp(MinValue, MaxValue, unitValue), EventType.UserInput);
        }
    }
}