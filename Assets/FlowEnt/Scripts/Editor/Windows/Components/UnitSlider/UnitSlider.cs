using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class UnitSlider : FriedSlider
    {
        [Preserve]
        public new class UxmlFactory : UxmlFactory<UnitSlider, UxmlTraits> { }
        [Preserve]
        public new class UxmlTraits : FriedSlider.UxmlTraits
        {
        }

        public override float Value
        {
            get => base.Value;
            set
            {
                base.Value = Mathf.Clamp01(value);
            }
        }
    }
}