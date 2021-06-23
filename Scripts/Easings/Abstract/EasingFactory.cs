using System;

namespace FlowEnt
{
    public static class EasingFactory
    {
        public static IEasing Create(Easing easing)
        {
            switch (easing)
            {
                case Easing.Linear:
                    return new LinearEasing();
                case Easing.EaseInSine:
                    return new EaseInSine();
                case Easing.EaseOutSine:
                    return new EaseOutSine();
                case Easing.EaseInOutSine:
                    return new EaseInOutSine();
                case Easing.EaseInQuad:
                    return new EaseInQuad();
                case Easing.EaseOutQuad:
                    return new EaseOutQuad();
                case Easing.EaseInOutQuad:
                    return new EaseInOutQuad();
                case Easing.EaseInCubic:
                    return new EaseInCubic();
                case Easing.EaseOutCubic:
                    return new EaseOutCubic();
                case Easing.EaseInOutCubic:
                    return new EaseInOutCubic();
                case Easing.EaseInQuart:
                    return new EaseInQuart();
                case Easing.EaseOutQuart:
                    return new EaseOutQuart();
                case Easing.EaseInOutQuart:
                    return new EaseInOutQuart();
                case Easing.EaseInQuint:
                    return new EaseInQuint();
                case Easing.EaseOutQuint:
                    return new EaseOutQuint();
                case Easing.EaseInOutQuint:
                    return new EaseInOutQuint();
                case Easing.EaseInExpo:
                    return new EaseInExpo();
                case Easing.EaseOutExpo:
                    return new EaseOutExpo();
                case Easing.EaseInOutExpo:
                    return new EaseInOutExpo();
                case Easing.EaseInCirc:
                    return new EaseInCirc();
                case Easing.EaseOutCirc:
                    return new EaseOutCirc();
                case Easing.EaseInOutCirc:
                    return new EaseInOutCirc();
                case Easing.EaseInBack:
                    return new EaseInBack();
                case Easing.EaseOutBack:
                    return new EaseOutBack();
                case Easing.EaseInOutBack:
                    return new EaseInOutBack();
                case Easing.EaseInElastic:
                    return new EaseInElastic();
                case Easing.EaseOutElastic:
                    return new EaseOutElastic();
                case Easing.EaseInOutElastic:
                    return new EaseInOutElastic();
                case Easing.EaseInBounce:
                    return new EaseInBounce();
                case Easing.EaseOutBounce:
                    return new EaseOutBounce();
                case Easing.EaseInOutBounce:
                    return new EaseInOutBounce();
                default:
                    throw new ArgumentException($"Unknown easing {easing}");
            }
        }
    }
}