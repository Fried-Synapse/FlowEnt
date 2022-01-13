using System;
using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Demo
{
    [Serializable]
    public class Phase3Animation : AbstractDemoAnimation
    {
#pragma warning disable RCS1169, IDE0044
        [SerializeField]
        private List<Transform> pendulums;
        private List<Transform> Pendulums => pendulums;
#pragma warning restore RCS1169, IDE0044

        public override AbstractAnimation GetAnimation()
            => new Flow()
                .At(0.0f, GetAnimation(Pendulums[0], new Vector3(4f, 0f, 4f)))
                .At(0.5f, GetAnimation(Pendulums[1], new Vector3(4f, 0f, -4f)))
                .At(1.0f, GetAnimation(Pendulums[2], new Vector3(4f, 0f, 0f)))
                .At(1.5f, GetAnimation(Pendulums[3], new Vector3(0f, 0f, -4f)));

        private AbstractAnimation GetAnimation(Transform transform, Vector3 direction)
            => new Flow()
                .OnStarted(() => transform.gameObject.SetActive(true))
                .Queue(new Tween(0.5f).For(transform.GetComponent<MeshRenderer>().material).AlphaTo(0f, 1f))
                .Queue(new Tween(1f).SetEasing(Easing.EaseOutQuad).For(transform).MoveLocalTo(direction))
                .Queue(new Tween(2f).SetEasing(Easing.EaseInOutQuad).SetLoopCount(3).SetLoopType(LoopType.PingPong).For(transform).MoveLocalTo(-direction))
                .Queue(new Tween(1f).SetEasing(Easing.EaseInQuad).For(transform).MoveLocalTo(Vector3.zero))
                .Queue(new Tween(0.5f).For(transform.GetComponent<MeshRenderer>().material).AlphaTo(0f))
                .OnCompleted(() => transform.gameObject.SetActive(false));
    }
}
