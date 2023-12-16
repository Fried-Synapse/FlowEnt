using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the texture scale for the specified shader property.
    /// </summary>
    public class TextureScalePropertyMotion : AbstractVector2Motion<Material>
    {
        public TextureScalePropertyMotion(Material item, string propertyName, Vector2 value)
            : this(item, Shader.PropertyToID(propertyName), value)
        {
        }

        public TextureScalePropertyMotion(Material item, string propertyName, Vector2? from, Vector2 to)
            : this(item, Shader.PropertyToID(propertyName), from, to)
        {
        }

        public TextureScalePropertyMotion(Material item, int propertyId, Vector2 value) : base(item, value)
        {
            this.propertyId = propertyId;
        }

        public TextureScalePropertyMotion(Material item, int propertyId, Vector2? from, Vector2 to) : base(item, from,
            to)
        {
            this.propertyId = propertyId;
        }

        private readonly int propertyId;
        protected override Vector2 GetFrom() => item.GetTextureScale(propertyId);
        protected override void SetValue(Vector2 value) => item.SetTextureScale(propertyId, value);
    }
}