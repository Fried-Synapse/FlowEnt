using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the texture offset for the specified shader property.
    /// </summary>
    public class TextureOffsetPropertyMotion : AbstractVector2Motion<MaterialBuilderWithProperty<Texture>>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override AbstractTweenMotion Build()
                => new TextureOffsetPropertyMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override AbstractTweenMotion Build()
                => new TextureOffsetPropertyMotion(item, From, to);
        }

        private TextureOffsetPropertyMotion(MaterialBuilderWithProperty<Texture> item, Vector2 value)
            : base(item, value)
        {
        }

        private TextureOffsetPropertyMotion(MaterialBuilderWithProperty<Texture> item, Vector2? from, Vector2 to)
            : base(item, from, to)
        {
        }

        public TextureOffsetPropertyMotion(Material item, int propertyId, Vector2 value)
            : this(new MaterialBuilderWithProperty<Texture>(item, propertyId), value)
        {
        }

        public TextureOffsetPropertyMotion(Material item, int propertyId, Vector2? from, Vector2 to)
            : this(new MaterialBuilderWithProperty<Texture>(item, propertyId), from, to)
        {
        }

        public TextureOffsetPropertyMotion(Material item, string propertyName, Vector2 value)
            : this(item, Shader.PropertyToID(propertyName), value)
        {
        }

        public TextureOffsetPropertyMotion(Material item, string propertyName, Vector2? from, Vector2 to)
            : this(item, Shader.PropertyToID(propertyName), from, to)
        {
        }

        protected override Vector2 GetFrom() => item.BuiltMaterial.GetTextureOffset(item.PropertyId);
        protected override void SetValue(Vector2 value) => item.BuiltMaterial.SetTextureOffset(item.PropertyId, value);
    }
}