using System;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class ControlBar : VisualElement
    {

        [Preserve]
        public new class UxmlFactory : UxmlFactory<ControlBar, UxmlTraits> { }
        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlFloatAttributeDescription value = new UxmlFloatAttributeDescription { name = "value", };

            public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
            {
                base.Init(visualElement, bag, context);
                ControlBar bar = visualElement as ControlBar;
                bar.Value = value.GetValueFromBag(bag, context);
            }
        }

        public ControlBar()
        {
            this.LoadUxml();
            this.LoadUss();
            Fill = this.Query<VisualElement>("fill").First();
            Init();
            Bind();
        }

        private VisualElement Fill { get; }
        private float value;
        public float Value
        {
            get => value;
            set
            {
                this.value = Mathf.Clamp01(value);
                Fill.style.width = new StyleLength(new Length(this.value * 100f, LengthUnit.Percent));
            }
        }
        private void Init() { }

        private void Bind()
        {
            RegisterCallback<PointerDownEvent>(StartValueChange);
        }

        private void StartValueChange(PointerDownEvent evt)
        {
            Value = (evt.position.x - worldBound.xMin) / worldBound.width;
            this.CaptureMouse();
            RegisterCallback<PointerMoveEvent>(OnValueChanged);
            RegisterCallback<PointerUpEvent>((_) =>
            {
                this.ReleaseMouse();
                UnregisterCallback<PointerMoveEvent>(OnValueChanged);
            });
        }

        private void OnValueChanged(PointerMoveEvent evt)
        {
            Value = (evt.position.x - worldBound.xMin) / worldBound.width;
        }
    }
}