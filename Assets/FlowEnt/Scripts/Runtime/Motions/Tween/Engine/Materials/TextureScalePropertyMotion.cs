using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the texture scale for the specified shader property.
    /// </summary>
    public class TextureScalePropertyMotion : AbstractVector2Motion<MaterialBuilderWithProperty<Texture>>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override ITweenMotion Build()
                => new TextureScalePropertyMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override ITweenMotion Build()
                => new TextureScalePropertyMotion(item, From, to);
        }

        private TextureScalePropertyMotion(MaterialBuilderWithProperty<Texture> item, Vector2 value)
            : base(item, value)
        {
        }

        private TextureScalePropertyMotion(MaterialBuilderWithProperty<Texture> item, Vector2? from, Vector2 to)
            : base(item, from, to)
        {
        }

        public TextureScalePropertyMotion(Material item, int propertyId, Vector2 value)
            : this(new MaterialBuilderWithProperty<Texture>(item, propertyId), value)
        {
        }

        public TextureScalePropertyMotion(Material item, int propertyId, Vector2? from, Vector2 to)
            : this(new MaterialBuilderWithProperty<Texture>(item, propertyId), from, to)
        {
        }

        public TextureScalePropertyMotion(Material item, string propertyName, Vector2 value)
            : this(item, Shader.PropertyToID(propertyName), value)
        {
        }

        public TextureScalePropertyMotion(Material item, string propertyName, Vector2? from, Vector2 to)
            : this(item, Shader.PropertyToID(propertyName), from, to)
        {
        }

        protected override Vector2 GetFrom() => item.BuiltMaterial.GetTextureScale(item.PropertyId);
        protected override void SetValue(Vector2 value) => item.BuiltMaterial.SetTextureScale(item.PropertyId, value);
    }
}