namespace FriedSynapse.FlowEnt.Motions.Abstract
{
    public abstract class AbstractAnimationCurve3dMotion<TItem> : AbstractMotion<TItem>
    {
        protected AbstractAnimationCurve3dMotion(TItem item, AnimationCurve3d animationCurve) : base(item)
        {
            this.animationCurve = animationCurve;
        }

        protected readonly AnimationCurve3d animationCurve;
    }
}
