using System;
using System.Collections.Generic;
using FriedSynapse.FlowEnt.Easings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FriedSynapse.FlowEnt.Demo
{
    [Serializable]
    public class Phase1Animation : AbstractDemoAnimation
    {
        [SerializeField]
        private List<Transform> jumpers;
        private List<Transform> Jumpers => jumpers;
        [SerializeField]
        private List<Transform> rotators;
        private List<Transform> Rotators => rotators;

        public override AbstractAnimation GetAnimation()
            => new Flow()
                .Queue(new Tween(0.3f).ForAll(Jumpers).Apply(t =>
                            {
                                t.OnStarted(() =>
                                {
                                    t.Item.gameObject.SetActive(true);
                                    t.Item.localPosition = new Vector3(t.Item.localPosition.x, -0.5f, t.Item.localPosition.z);
                                });
                                t.ScaleLocalToY(1f).MoveLocalToY(0f);
                            }))
                .Queue(new Tween(2f).SetEasing(new BounceEasing(5)).ForAll(Jumpers).Apply(t => t.MoveLocalToY(Random.Range(2f, 5f))))
                .QueueDelay(1.7f)
                .Queue(new Tween(1f).SetEasing(Easing.EaseInCirc).ForAll(Jumpers).Apply(t => t.MoveLocalTo(Vector3.zero).OnCompleted(() => t.Item.gameObject.SetActive(false))))
                .At(2f, new Tween(2f).SetEasing(Easing.EaseOutQuad).ForAll(Rotators).Apply(t => t.MoveLocalToY(0f).ScaleLocalTo(Vector3.one).RotateY(720f)))
                .Queue(new Tween(1f).SetEasing(Easing.EaseInCirc).ForAll(Rotators).Apply(t => t.MoveLocalTo(Vector3.zero).OnCompleted(() => t.Item.gameObject.SetActive(false))));
    }
}
