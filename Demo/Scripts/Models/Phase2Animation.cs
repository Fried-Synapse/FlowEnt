using System;
using System.Collections.Generic;
using FriedSynapse.FlowEnt.Motions.Renderers;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Demo
{
    [Serializable]
    public class Phase2Animation : AbstractDemoAnimation
    {
        private class BlowUpMotion : AlphaToMotion<Renderer>
        {
            public BlowUpMotion(Renderer item) : base(item, 0f)
            {
            }

            public override void OnUpdate(float t)
            {
                const int multiplier = 5;
                base.OnUpdate(Mathf.Clamp01((t * multiplier) - (multiplier - 1)));
            }
        }

        [SerializeField]
        private List<Transform> blowers;
        private List<Transform> Blowers => blowers;

        public override AbstractAnimation GetAnimation()
            => new Flow()
                .Queue(GetAnimation(Blowers[0]))
                .At(1f, GetAnimation(Blowers[1]))
                .At(2f, GetAnimation(Blowers[2]))
                .At(3f, GetAnimation(Blowers[3]));

        private AbstractAnimation GetAnimation(Transform transform)
            => new Tween(3f)
                .For(transform).MoveLocalToY(2f).ScaleLocalTo(Vector3.one * 5f)
                .Apply(new BlowUpMotion(transform.GetComponent<Renderer>()))
                .OnCompleted(() => transform.gameObject.SetActive(false));
    }
}
