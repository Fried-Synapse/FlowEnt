using System;

namespace FriedSynapse.FlowEnt.Editor
{
    internal partial class PreviewerWindow
    {
        private class AnimationInfo
        {
            internal AnimationInfo(string name, MemberType type, AbstractAnimation animation)
            {
                Init(name, type, animation);
            }

            internal AnimationInfo(string name, MemberType type, IAbstractAnimationBuilder animationBuilder)
            {
                try
                {
                    Init(name, type, animationBuilder.Build());
                }
                catch (Exception ex)
                {
                    Exception = ex;
                }
            }

            private void Init(string name, MemberType type, AbstractAnimation animation)
            {
                Name = name;
                Type = type;
                Animation = animation;
                if (animation.PlayState != PlayState.Building)
                {
                    animation.Stop();
                    animation.Reset();
                }
            }

            internal string Name { get; private set; }
            internal MemberType Type { get; private set; }
            internal AbstractAnimation Animation { get; private set; }
            internal Exception Exception { get; private set; }
        }
    }
}