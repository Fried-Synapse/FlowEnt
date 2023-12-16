using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the color for the specified shader property.
    /// </summary>
    public class ColorPropertyMotion : AbstractColorMotion<Material>
    {
        public ColorPropertyMotion(Material item, string propertyName, Color value)
            : this(item, Shader.PropertyToID(propertyName), value)
        {
        }

        public ColorPropertyMotion(Material item, string propertyName, Color? from, Color to)
            : this(item, Shader.PropertyToID(propertyName), from, to)
        {
        }

        public ColorPropertyMotion(Material item, int propertyId, Color value) : base(item, value)
        {
            this.propertyId = propertyId;
        }

        public ColorPropertyMotion(Material item, int propertyId, Color? from, Color to) : base(item, from, to)
        {
            this.propertyId = propertyId;
        }

        private readonly int propertyId;
        protected override Color GetFrom() => item.GetColor(propertyId);
        protected override void SetValue(Color value) => item.SetColor(propertyId, value);
    }
}