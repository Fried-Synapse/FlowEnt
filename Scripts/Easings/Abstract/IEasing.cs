namespace FriedSynapse.FlowEnt
{
    public interface IEasing
    {
        public float GetValue(float t);
    }

    /// <summary>
    /// Easing list based on https://easings.net/ and some custom ones.
    /// </summary>
    public enum Easing
    {
        Linear,
        EaseInSine,
        EaseOutSine,
        EaseInOutSine,
        EaseInQuad,
        EaseOutQuad,
        EaseInOutQuad,
        EaseInCubic,
        EaseOutCubic,
        EaseInOutCubic,
        EaseInQuart,
        EaseOutQuart,
        EaseInOutQuart,
        EaseInQuint,
        EaseOutQuint,
        EaseInOutQuint,
        EaseInExpo,
        EaseOutExpo,
        EaseInOutExpo,
        EaseInCirc,
        EaseOutCirc,
        EaseInOutCirc,
        EaseInBack,
        EaseOutBack,
        EaseInOutBack,
        EaseInElastic,
        EaseOutElastic,
        EaseInOutElastic,
        EaseInBounce,
        EaseOutBounce,
        EaseInOutBounce,
        /// <summary>Creates a bounce easing with default values.</summary>
        Bounce,
        /// <summary>Creates a shake easing with default values.</summary>
        Shake
    }
}