using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    internal interface IFluentTweenEventable<T>
    {
        T OnStarting(Action callback);
        T OnStarted(Action callback);

        T OnUpdating(Action<float> callback);
        T OnUpdated(Action<float> callback);

        T OnLoopCompleted(Action<int?> callback);

        T OnCompleted(Action callback);
    }

    internal interface IFluentTweenOptionable<T>
    {
        T SetTime(float time);

        T SetEasing(IEasing easing);

        T SetEasing(Easing easing);

        T SetEasing(AnimationCurve animationCurve);

        T SetLoopType(LoopType loopType);

        T SetLoopCount(int? loopCount);

        T SetTimeScale(float timeScale);

        T SetSkipFrames(int frames);

        T SetDelay(float time);
    }
}
