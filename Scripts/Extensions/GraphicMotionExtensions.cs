using FlowEnt.Motions.GraphicMotions;
using UnityEngine;
using UnityEngine.UI;

namespace FlowEnt
{
    public static class GraphicMotionExtensions
    {

        public static TweenMotion<TGraphic> AlphaTo<TGraphic>(this TweenMotion<TGraphic> motionWrapper, float to)
            where TGraphic : Graphic
            => motionWrapper.Apply(new AlphaToMotion(motionWrapper.Item, to));


        public static TweenMotion<TGraphic> AlphaTo<TGraphic>(this TweenMotion<TGraphic> motionWrapper, float from, float to)
            where TGraphic : Graphic
            => motionWrapper.Apply(new AlphaToMotion(motionWrapper.Item, from, to));

    }
}