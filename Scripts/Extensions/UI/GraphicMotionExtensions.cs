using FriedSynapse.FlowEnt.Motions.UI.Graphics;
using UnityEngine;
using UnityEngine.UI;

namespace FriedSynapse.FlowEnt
{
    public static class GraphicMotionExtensions
    {
        #region Alpha

        public static TweenMotion<TGraphic> Alpha<TGraphic>(this TweenMotion<TGraphic> motion, float value)
            where TGraphic : Graphic
            => motion.Apply(new AlphaMotion<TGraphic>(motion.Item, value));

        public static TweenMotion<TGraphic> AlphaTo<TGraphic>(this TweenMotion<TGraphic> motion, float to)
            where TGraphic : Graphic
            => motion.Apply(new AlphaToMotion<TGraphic>(motion.Item, to));

        public static TweenMotion<TGraphic> AlphaTo<TGraphic>(this TweenMotion<TGraphic> motion, float from, float to)
            where TGraphic : Graphic
            => motion.Apply(new AlphaToMotion<TGraphic>(motion.Item, from, to));

        #endregion

        #region Color

        public static TweenMotion<TGraphic> Color<TGraphic>(this TweenMotion<TGraphic> motion, Color value)
            where TGraphic : Graphic
            => motion.Apply(new ColorMotion<TGraphic>(motion.Item, value));

        public static TweenMotion<TGraphic> ColorTo<TGraphic>(this TweenMotion<TGraphic> motion, Color to)
            where TGraphic : Graphic
            => motion.Apply(new ColorToMotion<TGraphic>(motion.Item, to));

        public static TweenMotion<TGraphic> ColorTo<TGraphic>(this TweenMotion<TGraphic> motion, Color from, Color to)
            where TGraphic : Graphic
            => motion.Apply(new ColorToMotion<TGraphic>(motion.Item, from, to));

        #endregion
    }
}
