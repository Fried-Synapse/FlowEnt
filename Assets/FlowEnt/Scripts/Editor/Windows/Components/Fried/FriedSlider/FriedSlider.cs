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
            private readonly UxmlFloatAttributeDescription value = new UxmlFloatAttributeDescription { name = "value", };

            public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
            {
                base.Init(visualElement, bag, context);
                FriedSlider slider = visualElement as FriedSlider;
                slider.Value = value.GetValueFromBag(bag, context);
            }
        }

        public FriedSlider()
        {
            this.LoadUxml();
            Background = this.Query<VisualElement>("background").First();
            Fill = this.Query<VisualElement>("fill").First();
            Init();
            Bind();
        }

        private VisualElement Background { get; }
        private VisualElement Fill { get; }
        private float value;
        public virtual float Value
        {
            get => value;
            set
            {
                this.value = value;
                Fill.style.width = new StyleLength(new Length(this.value * 100f, LengthUnit.Percent));
                OnValueChanged?.Invoke();
            }
        }

        public Action OnValueChanged { get; set; }

        private void Init() { }

        private void Bind()
        {
            Background.RegisterCallback<PointerDownEvent>(StartValueChange);
        }

        private void StartValueChange(PointerDownEvent evt)
        {
            OnPointerMove(evt);
            this.CaptureMouse();
            Background.RegisterCallback<PointerMoveEvent>(OnPointerMove);
            Background.RegisterCallback<PointerUpEvent>((_) =>
            {
                this.ReleaseMouse();
                Background.UnregisterCallback<PointerMoveEvent>(OnPointerMove);
            });
        }

        private void OnPointerMove(IPointerEvent evt)
        {
            Value = (evt.position.x - Background.worldBound.xMin) / Background.worldBound.width;
        }
    }
}