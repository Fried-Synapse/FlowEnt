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

        protected override void Bind()
        {
            base.Bind();
            OnValueChanging += data => maxValue = data.NewValue * 2;
        }
    }
}