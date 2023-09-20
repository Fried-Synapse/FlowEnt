using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace FriedSynapse.FlowEnt
{
    public abstract class AbstractAuthoring : MonoBehaviour
    {
        public enum StartModeEnum
        {
            Awake,
            Start,
            OnEnable,
            Custom
        }

        [SerializeField]
        private StartModeEnum startMode;

        public StartModeEnum StartMode => startMode;

        [SerializeField]
        private float delay;

        public float Delay => delay;

        [SerializeField]
        private bool autoDestroy = true;

        public bool AutoDestroy => autoDestroy;

        [SerializeField]
        private bool triggerOnCompleted;

        public bool TriggerOnCompleted => triggerOnCompleted;

        protected abstract void StartAnimations();
        protected abstract void StopAnimations();

        public void StartCustomMode()
        {
            if (StartMode == StartModeEnum.Custom)
            {
                StartAnimations();
            }
        }

        private void Awake()
        {
            if (StartMode == StartModeEnum.Awake)
            {
                StartAnimations();
            }
        }

        private void Start()
        {
            if (StartMode == StartModeEnum.Start)
            {
                StartAnimations();
            }
        }

        private void OnEnable()
        {
            if (StartMode == StartModeEnum.OnEnable)
            {
                StartAnimations();
            }
        }

        private void OnDisable()
        {
            if (StartMode == StartModeEnum.OnEnable)
            {
                StopAnimations();
            }
        }

        private void OnDestroy()
        {
            if (AutoDestroy)
            {
                StopAnimations();
            }
        }
    }

    public class AbstractAuthoring<TAnimation, TAnimationBuilder> : AbstractAuthoring
        where TAnimation : AbstractAnimation
        where TAnimationBuilder : AbstractBuilder<TAnimation>
    {
        [SerializeField]
        private protected TAnimationBuilder animationBuilder;

        public TAnimationBuilder AnimationBuilder => animationBuilder;

        public TAnimation Animation { get; set; }

        private Tween DelayTween { get; set; }

        protected override void StartAnimations()
        {
            void startAnimations()
            {
                Animation = (TAnimation)AnimationBuilder.Build().Start();
            }

            if (Delay > 0)
            {
                DelayTween = new Tween(Delay).OnCompleted(startAnimations).Start();
            }
            else
            {
                startAnimations();
            }
        }

        protected override void StopAnimations()
        {
            DelayTween?.Stop();
            Animation?.Stop(TriggerOnCompleted);
        }
    }
}