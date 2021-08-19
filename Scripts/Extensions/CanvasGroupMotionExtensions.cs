using FriedSynapse.FlowEnt.Motions.CanvasGroups;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public static class CanvasGroupMotionExtensions
    {
        #region MoveAnchoredPositionTo

        public static TweenMotion<CanvasGroup> Alpha(this TweenMotion<CanvasGroup> motionWrapper, float value)
            => motionWrapper.Apply(new AlphaMotion(motionWrapper.Item, value));

        #endregion

        #region MoveAnchoredPositionTo

        public static TweenMotion<CanvasGroup> AlphaTo(this TweenMotion<CanvasGroup> motionWrapper, float to)
            => motionWrapper.Apply(new AlphaToMotion(motionWrapper.Item, to));

        public static TweenMotion<CanvasGroup> AlphaTo(this TweenMotion<CanvasGroup> motionWrapper, float from, float to)
            => motionWrapper.Apply(new AlphaToMotion(motionWrapper.Item, from, to));

        #endregion
    }
}