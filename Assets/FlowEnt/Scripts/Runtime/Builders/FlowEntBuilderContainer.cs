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

        [SerializeField]
        private StartTypeEnum startType;

        public StartTypeEnum StartType => startType;

        [SerializeField]
        private float delay;

        public float Delay => delay;

        [SerializeField]
        private List<IAbstractAnimationBuilder> animations;

        public List<IAbstractAnimationBuilder> Animations => animations;

        void Start()
        {
        }
    }
}