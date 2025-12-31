using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    [UxmlElement]
    internal partial class AutoScalableSlider : FriedSlider
    {
        protected override void OnTextValueChaging(float newValue)
        {
            maxValue = Mathf.Max(1f, newValue * 2f);
        }
    }
}