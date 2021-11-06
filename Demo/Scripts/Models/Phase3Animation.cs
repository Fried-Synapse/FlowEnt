using System;
using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Demo
{
    [Serializable]
    public class Phase3Animation : AbstractDemoAnimation
    {
        [SerializeField]
        private List<Transform> penduls;
        private List<Transform> Penduls => penduls;

        public override AbstractAnimation GetAnimation()
            => new Flow()
                .At(0.0f, GetAnimation(Penduls[0], new Vector3(4f, 0f, 4f)))
                .At(0.5f, GetAnimation(Penduls[1], new Vector3(4f, 0f, -4f)))
                .At(1.0f, GetAnimation(Penduls[2], new Vector3(4f, 0f, 0f)))
                .At(1.5f, GetAnimation(Penduls[3], new Vector3(0f, 0f, -4f)));

        private AbstractAnimation GetAnimation(Transform transform, Vector3 direction)
            => new Flow()
                .OnStarted(() => transform.gameObject.SetActive(true))
                .Queue(new Tween(0.5f).For(transform.GetComponent<MeshRenderer>()).AlphaTo(0f, 1f))
                .Queue(new Tween(1f).For(transform).MoveLocalTo(direction))
                .Queue(new Tween(2f).SetLoopCount(3).SetLoopType(LoopType.PingPong).For(transform).MoveLocalTo(-direction))
                .Queue(new Tween(1f).For(transform).MoveLocalTo(Vector3.zero))
                .Queue(new Tween(0.5f).For(transform.GetComponent<MeshRenderer>()).AlphaTo(0f))
                .OnCompleted(() => transform.gameObject.SetActive(false));
    }
}
