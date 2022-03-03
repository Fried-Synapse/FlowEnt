using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the texture offset for the specified shader property.
    /// </summary>
    public class TextureOffsetPropertyIdMotion : AbstractVector2Motion<Material>
    {
        public TextureOffsetPropertyIdMotion(Material item, int propertyId, Vector2 value) : base(item, value)
        {
            this.propertyId = propertyId;
        }

        public TextureOffsetPropertyIdMotion(Material item, int propertyId, Vector2? from, Vector2 to) : base(item, from, to)
        {
            this.propertyId = propertyId;
        }

        private readonly int propertyId;
        protected override Vector2 GetFrom() => item.GetTextureOffset(propertyId);
        protected override void SetValue(Vector2 value) => item.SetTextureOffset(propertyId, value);
    }
}