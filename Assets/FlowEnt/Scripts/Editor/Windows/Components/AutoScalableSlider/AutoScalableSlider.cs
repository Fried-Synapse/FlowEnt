
using System;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class AutoScalableSlider : FriedSlider
    {
        [Preserve]
        public new class UxmlFactory : UxmlFactory<AutoScalableSlider, UxmlTraits> { }
        [Preserve]
        public new class UxmlTraits : FriedSlider.UxmlTraits
        {
        }

        public AutoScalableSlider()
        {
            ValueText = this.Query<TextField>("value").First();
            Init();
        }

        private TextField ValueText { get; }

        public override float Value
        {
            get => base.Value;
            set
            {
                base.Value = value;
                ValueText.value = base.Value.ToString("F3");
            }
        }

        private void Init()
        {
            ValueText.RegisterValueChangedCallback(e =>
            {
                if (float.TryParse(e.newValue, out float result))
                {
                    base.Value = result;
                }
                else
                {
                    e.PreventDefault();
                }
            });
        }
    }
}