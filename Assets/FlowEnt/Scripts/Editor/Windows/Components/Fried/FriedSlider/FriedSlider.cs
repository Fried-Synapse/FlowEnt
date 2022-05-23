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

        public FriedSlider()
        {
            this.LoadUxml();
            Background = this.Query<VisualElement>("background").First();
            Fill = this.Query<VisualElement>("fill").First();
            ValueText = this.Query<TextField>("value").First();
            Init();
            Bind();
        }

        private VisualElement Background { get; }
        private VisualElement Fill { get; }
        private TextField ValueText { get; }

        private float minValue;
        public float MinValue
        {
            get => minValue;
            set
            {
                minValue = value;
                SetValue(this.value);
            }
        }

        private float maxValue;
        public float MaxValue
        {
            get => maxValue;
            set
            {
                maxValue = value;
                SetValue(this.value);
            }
        }

        private float value;
        public virtual float Value
        {
            get => value;
            set => SetValue(value);
        }

        public Action OnValueChanged { get; set; }

        private void Init() { }

        private void Bind()
        {
            Background.RegisterCallback<PointerDownEvent>(StartValueChange);
            ValueText.RegisterValueChangedCallback(e =>
            {
                if (float.TryParse(e.newValue, out float result))
                {
                    Value = result;
                }
                else
                {
                    e.PreventDefault();
                }
            });
        }

        private void SetValue(float newValue)
        {
            value = Mathf.Clamp(newValue, MinValue, MaxValue);
            Fill.style.width = new StyleLength(new Length(Mathf.InverseLerp(MinValue, MaxValue, value) * 100f, LengthUnit.Percent));
            ValueText.value = value.ToString("0.###");
            OnValueChanged?.Invoke();
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
            Value = Mathf.Lerp(MinValue, MaxValue, unitValue);
        }
    }
}