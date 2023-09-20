using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public class AnimationBuilderContainerRunner : MonoBehaviour
    {
        public enum StartModeEnum
        {
            Awake,
            Start,
            OnEnable,
            Custom
        }

        [Header("Settings")]
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

        [SerializeField]
        private AnimationBuilderContainer container;

        public AnimationBuilderContainer Container => container;

        public List<AbstractAnimation> Animations { get; set; }

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

        private void StartAnimations()
        {
            Animations = new List<AbstractAnimation>();

            void startAnimations()
            {
                Animations.AddRange(Container.Build().Start());
            }

            if (Delay > 0)
            {
                Animations.Add(new Tween(Delay).OnCompleted(startAnimations).Start());
            }
            else
            {
                startAnimations();
            }
        }

        private void StopAnimations()
        {
            Animations.Stop(TriggerOnCompleted);
        }
    }
}