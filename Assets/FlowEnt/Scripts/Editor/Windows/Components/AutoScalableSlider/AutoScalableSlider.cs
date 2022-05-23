
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

        public override float Value
        {
            get => base.Value;
            set
            {
                if (value > MaxValue)
                {
                    MaxValue = value * 2;
                }
                base.Value = value;
            }
        }
    }
}