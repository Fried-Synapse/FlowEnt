using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public class FlowEntBuilderContainer : MonoBehaviour
    {
        public enum StartTypeEnum
        {
            Awake,
            Start,
            OnEnable,
            Custom
        }

        [Header("Settings")]
        [SerializeField]
        private StartTypeEnum startType;

        public StartTypeEnum StartType => startType;

        [SerializeField]
        private float delay;

        public float Delay => delay;

        [SerializeField]
        private List<IAbstractAnimationBuilder> animations = new List<IAbstractAnimationBuilder>();

        [SerializeField]
        private IAbstractAnimationBuilder test = new TweenBuilder();

        public List<IAbstractAnimationBuilder> Animations => animations;

        void Start()
        {
        }
    }
}