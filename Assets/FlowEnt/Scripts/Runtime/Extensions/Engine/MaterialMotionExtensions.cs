using UnityEngine;
using FriedSynapse.FlowEnt.Motions.Tween.Materials;

namespace FriedSynapse.FlowEnt
{
    public static class MaterialMotionExtensions
    {
        #region Float

        /// <summary>
        /// Applies a <see cref="FloatPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> Float<TMaterial>(this TweenMotionProxy<TMaterial> proxy, string propertyName, float value)
            where TMaterial : Material
            => proxy.Apply(new FloatPropertyIdMotion(proxy.Item, Shader.PropertyToID(propertyName), value));

        /// <summary>
        /// Applies a <see cref="FloatPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> FloatTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, string propertyName, float to)
            where TMaterial : Material
            => proxy.Apply(new FloatPropertyIdMotion(proxy.Item, Shader.PropertyToID(propertyName), default, to));

        /// <summary>
        /// Applies a <see cref="FloatPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> FloatTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, string propertyName, float from, float to)
            where TMaterial : Material
            => proxy.Apply(new FloatPropertyIdMotion(proxy.Item, Shader.PropertyToID(propertyName), from, to));

        /// <summary>
        /// Applies a <see cref="FloatPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyId"></param>
        /// <param name="value"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> Float<TMaterial>(this TweenMotionProxy<TMaterial> proxy, int propertyId, float value)
            where TMaterial : Material
            => proxy.Apply(new FloatPropertyIdMotion(proxy.Item, propertyId, value));

        /// <summary>
        /// Applies a <see cref="FloatPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyId"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> FloatTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, int propertyId, float to)
            where TMaterial : Material
            => proxy.Apply(new FloatPropertyIdMotion(proxy.Item, propertyId, default, to));

        /// <summary>
        /// Applies a <see cref="FloatPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> FloatTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, int propertyId, float from, float to)
            where TMaterial : Material
            => proxy.Apply(new FloatPropertyIdMotion(proxy.Item, propertyId, from, to));

        #endregion

        #region Int

        /// <summary>
        /// Applies a <see cref="IntPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> Int<TMaterial>(this TweenMotionProxy<TMaterial> proxy, string propertyName, int value)
            where TMaterial : Material
            => proxy.Apply(new IntPropertyIdMotion(proxy.Item, Shader.PropertyToID(propertyName), value));

        /// <summary>
        /// Applies a <see cref="IntPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> IntTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, string propertyName, int to)
            where TMaterial : Material
            => proxy.Apply(new IntPropertyIdMotion(proxy.Item, Shader.PropertyToID(propertyName), default, to));

        /// <summary>
        /// Applies a <see cref="IntPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> IntTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, string propertyName, int from, int to)
            where TMaterial : Material
            => proxy.Apply(new IntPropertyIdMotion(proxy.Item, Shader.PropertyToID(propertyName), from, to));

        /// <summary>
        /// Applies a <see cref="IntPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyId"></param>
        /// <param name="value"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> Int<TMaterial>(this TweenMotionProxy<TMaterial> proxy, int propertyId, int value)
            where TMaterial : Material
            => proxy.Apply(new IntPropertyIdMotion(proxy.Item, propertyId, value));

        /// <summary>
        /// Applies a <see cref="IntPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyId"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> IntTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, int propertyId, int to)
            where TMaterial : Material
            => proxy.Apply(new IntPropertyIdMotion(proxy.Item, propertyId, default, to));

        /// <summary>
        /// Applies a <see cref="IntPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> IntTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, int propertyId, int from, int to)
            where TMaterial : Material
            => proxy.Apply(new IntPropertyIdMotion(proxy.Item, propertyId, from, to));

        #endregion

        #region Alpha

        /// <summary>
        /// Applies a <see cref="AlphaMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> Alpha<TMaterial>(this TweenMotionProxy<TMaterial> proxy, float value)
            where TMaterial : Material
            => proxy.Apply(new AlphaMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="AlphaMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> AlphaTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, float to)
            where TMaterial : Material
            => proxy.Apply(new AlphaMotion(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="AlphaMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> AlphaTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, float from, float to)
            where TMaterial : Material
            => proxy.Apply(new AlphaMotion(proxy.Item, from, to));

        /// <summary>
        /// Applies a <see cref="AlphaPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> Alpha<TMaterial>(this TweenMotionProxy<TMaterial> proxy, string propertyName, float value)
            where TMaterial : Material
            => proxy.Apply(new AlphaPropertyIdMotion(proxy.Item, Shader.PropertyToID(propertyName), value));

        /// <summary>
        /// Applies a <see cref="AlphaPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> AlphaTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, string propertyName, float to)
            where TMaterial : Material
            => proxy.Apply(new AlphaPropertyIdMotion(proxy.Item, Shader.PropertyToID(propertyName), default, to));

        /// <summary>
        /// Applies a <see cref="AlphaPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> AlphaTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, string propertyName, float from, float to)
            where TMaterial : Material
            => proxy.Apply(new AlphaPropertyIdMotion(proxy.Item, Shader.PropertyToID(propertyName), from, to));

        /// <summary>
        /// Applies a <see cref="AlphaPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyId"></param>
        /// <param name="value"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> Alpha<TMaterial>(this TweenMotionProxy<TMaterial> proxy, int propertyId, float value)
            where TMaterial : Material
            => proxy.Apply(new AlphaPropertyIdMotion(proxy.Item, propertyId, value));

        /// <summary>
        /// Applies a <see cref="AlphaPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyId"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> AlphaTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, int propertyId, float to)
            where TMaterial : Material
            => proxy.Apply(new AlphaPropertyIdMotion(proxy.Item, propertyId, default, to));

        /// <summary>
        /// Applies a <see cref="AlphaPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> AlphaTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, int propertyId, float from, float to)
            where TMaterial : Material
            => proxy.Apply(new AlphaPropertyIdMotion(proxy.Item, propertyId, from, to));

        #endregion

        #region Color

        /// <summary>
        /// Applies a <see cref="ColorMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> Color<TMaterial>(this TweenMotionProxy<TMaterial> proxy, Color value)
            where TMaterial : Material
            => proxy.Apply(new ColorMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="ColorMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> ColorTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, Color to)
            where TMaterial : Material
            => proxy.Apply(new ColorMotion(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="ColorMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> ColorTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, Color from, Color to)
            where TMaterial : Material
            => proxy.Apply(new ColorMotion(proxy.Item, from, to));

        /// <summary>
        /// Applies a <see cref="ColorGradientMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="gradient"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> ColorTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, Gradient gradient)
            where TMaterial : Material
            => proxy.Apply(new ColorGradientMotion(proxy.Item, gradient));

        /// <summary>
        /// Applies a <see cref="ColorPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> Color<TMaterial>(this TweenMotionProxy<TMaterial> proxy, string propertyName, Color value)
            where TMaterial : Material
            => proxy.Apply(new ColorPropertyIdMotion(proxy.Item, Shader.PropertyToID(propertyName), value));

        /// <summary>
        /// Applies a <see cref="ColorPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> ColorTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, string propertyName, Color to)
            where TMaterial : Material
            => proxy.Apply(new ColorPropertyIdMotion(proxy.Item, Shader.PropertyToID(propertyName), default, to));

        /// <summary>
        /// Applies a <see cref="ColorPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> ColorTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, string propertyName, Color from, Color to)
            where TMaterial : Material
            => proxy.Apply(new ColorPropertyIdMotion(proxy.Item, Shader.PropertyToID(propertyName), from, to));

        /// <summary>
        /// Applies a <see cref="ColorGradientPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="gradient"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> ColorTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, string propertyName, Gradient gradient)
            where TMaterial : Material
            => proxy.Apply(new ColorGradientPropertyIdMotion(proxy.Item, Shader.PropertyToID(propertyName), gradient));

        /// <summary>
        /// Applies a <see cref="ColorPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyId"></param>
        /// <param name="value"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> Color<TMaterial>(this TweenMotionProxy<TMaterial> proxy, int propertyId, Color value)
            where TMaterial : Material
            => proxy.Apply(new ColorPropertyIdMotion(proxy.Item, propertyId, value));

        /// <summary>
        /// Applies a <see cref="ColorPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyId"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> ColorTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, int propertyId, Color to)
            where TMaterial : Material
            => proxy.Apply(new ColorPropertyIdMotion(proxy.Item, propertyId, default, to));

        /// <summary>
        /// Applies a <see cref="ColorPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> ColorTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, int propertyId, Color from, Color to)
            where TMaterial : Material
            => proxy.Apply(new ColorPropertyIdMotion(proxy.Item, propertyId, from, to));

        /// <summary>
        /// Applies a <see cref="ColorGradientPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyId"></param>
        /// <param name="gradient"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> ColorTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, int propertyId, Gradient gradient)
            where TMaterial : Material
            => proxy.Apply(new ColorGradientPropertyIdMotion(proxy.Item, propertyId, gradient));

        #endregion

        #region TextureOffset

        /// <summary>
        /// Applies a <see cref="TextureOffsetPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> TextureOffset<TMaterial>(this TweenMotionProxy<TMaterial> proxy, string propertyName, Vector2 value)
            where TMaterial : Material
            => proxy.Apply(new TextureOffsetPropertyIdMotion(proxy.Item, Shader.PropertyToID(propertyName), value));

        /// <summary>
        /// Applies a <see cref="TextureOffsetPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> TextureOffsetTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, string propertyName, Vector2 to)
            where TMaterial : Material
            => proxy.Apply(new TextureOffsetPropertyIdMotion(proxy.Item, Shader.PropertyToID(propertyName), default, to));

        /// <summary>
        /// Applies a <see cref="TextureOffsetPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> TextureOffsetTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, string propertyName, Vector2 from, Vector2 to)
            where TMaterial : Material
            => proxy.Apply(new TextureOffsetPropertyIdMotion(proxy.Item, Shader.PropertyToID(propertyName), from, to));

        /// <summary>
        /// Applies a <see cref="TextureOffsetPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyId"></param>
        /// <param name="value"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> TextureOffset<TMaterial>(this TweenMotionProxy<TMaterial> proxy, int propertyId, Vector2 value)
            where TMaterial : Material
            => proxy.Apply(new TextureOffsetPropertyIdMotion(proxy.Item, propertyId, value));

        /// <summary>
        /// Applies a <see cref="TextureOffsetPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyId"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> TextureOffsetTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, int propertyId, Vector2 to)
            where TMaterial : Material
            => proxy.Apply(new TextureOffsetPropertyIdMotion(proxy.Item, propertyId, default, to));

        /// <summary>
        /// Applies a <see cref="TextureOffsetPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> TextureOffsetTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, int propertyId, Vector2 from, Vector2 to)
            where TMaterial : Material
            => proxy.Apply(new TextureOffsetPropertyIdMotion(proxy.Item, propertyId, from, to));

        #endregion

        #region TextureScale

        /// <summary>
        /// Applies a <see cref="TextureScalePropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> TextureScale<TMaterial>(this TweenMotionProxy<TMaterial> proxy, string propertyName, Vector2 value)
            where TMaterial : Material
            => proxy.Apply(new TextureScalePropertyIdMotion(proxy.Item, Shader.PropertyToID(propertyName), value));

        /// <summary>
        /// Applies a <see cref="TextureScalePropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> TextureScaleTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, string propertyName, Vector2 to)
            where TMaterial : Material
            => proxy.Apply(new TextureScalePropertyIdMotion(proxy.Item, Shader.PropertyToID(propertyName), default, to));

        /// <summary>
        /// Applies a <see cref="TextureScalePropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> TextureScaleTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, string propertyName, Vector2 from, Vector2 to)
            where TMaterial : Material
            => proxy.Apply(new TextureScalePropertyIdMotion(proxy.Item, Shader.PropertyToID(propertyName), from, to));

        /// <summary>
        /// Applies a <see cref="TextureScalePropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyId"></param>
        /// <param name="value"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> TextureScale<TMaterial>(this TweenMotionProxy<TMaterial> proxy, int propertyId, Vector2 value)
            where TMaterial : Material
            => proxy.Apply(new TextureScalePropertyIdMotion(proxy.Item, propertyId, value));

        /// <summary>
        /// Applies a <see cref="TextureScalePropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyId"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> TextureScaleTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, int propertyId, Vector2 to)
            where TMaterial : Material
            => proxy.Apply(new TextureScalePropertyIdMotion(proxy.Item, propertyId, default, to));

        /// <summary>
        /// Applies a <see cref="TextureScalePropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> TextureScaleTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, int propertyId, Vector2 from, Vector2 to)
            where TMaterial : Material
            => proxy.Apply(new TextureScalePropertyIdMotion(proxy.Item, propertyId, from, to));

        #endregion

        #region Vector

        /// <summary>
        /// Applies a <see cref="VectorPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> Vector<TMaterial>(this TweenMotionProxy<TMaterial> proxy, string propertyName, Vector4 value)
            where TMaterial : Material
            => proxy.Apply(new VectorPropertyIdMotion(proxy.Item, Shader.PropertyToID(propertyName), value));

        /// <summary>
        /// Applies a <see cref="VectorPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> VectorTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, string propertyName, Vector4 to)
            where TMaterial : Material
            => proxy.Apply(new VectorPropertyIdMotion(proxy.Item, Shader.PropertyToID(propertyName), default, to));

        /// <summary>
        /// Applies a <see cref="VectorPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> VectorTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, string propertyName, Vector4 from, Vector4 to)
            where TMaterial : Material
            => proxy.Apply(new VectorPropertyIdMotion(proxy.Item, Shader.PropertyToID(propertyName), from, to));

        /// <summary>
        /// Applies a <see cref="VectorPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyId"></param>
        /// <param name="value"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> Vector<TMaterial>(this TweenMotionProxy<TMaterial> proxy, int propertyId, Vector4 value)
            where TMaterial : Material
            => proxy.Apply(new VectorPropertyIdMotion(proxy.Item, propertyId, value));

        /// <summary>
        /// Applies a <see cref="VectorPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyId"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> VectorTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, int propertyId, Vector4 to)
            where TMaterial : Material
            => proxy.Apply(new VectorPropertyIdMotion(proxy.Item, propertyId, default, to));

        /// <summary>
        /// Applies a <see cref="VectorPropertyIdMotion{TMaterial}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TMaterial"></typeparam>
        public static TweenMotionProxy<TMaterial> VectorTo<TMaterial>(this TweenMotionProxy<TMaterial> proxy, int propertyId, Vector4 from, Vector4 to)
            where TMaterial : Material
            => proxy.Apply(new VectorPropertyIdMotion(proxy.Item, propertyId, from, to));

        #endregion
    }
}