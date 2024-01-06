using System;
using System.Collections.Generic;
using FriedSynapse.FlowEnt.Easings;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Demo
{
    [Serializable]
    public class CharacterAnimation : AbstractDemoAnimation, IEditorDrawer
    {
        private readonly Vector3 Initial = new(0, -180, 0);
        private readonly Vector3 Step1 = new(-15, -205, 0);
        private readonly Vector3 Step2 = new(-25, -155, 0);

        [SerializeField]
        private Transform character;
        private Transform Character => character;

        [SerializeField]
        private List<Vector3> splinePoints;
        private List<Vector3> SplinePoints => splinePoints;

        private ICurve GetSpline() => new BSpline(SplinePoints).Normalise();

        public override AbstractAnimation GetAnimation()
            => new Flow()
                .SetName("Character flow")
                .Queue(new Tween(1.5f).SetEasing(Easing.EaseInOutSine).For(Character).RotateLocalTo(Initial, Step1))
                .QueueDelay(0.5f)
                .Queue(new Tween(1.5f).SetEasing(Easing.EaseInOutSine).For(Character).RotateLocalTo(Step1, Step2))
                .QueueDelay(0.5f)
                .Queue(new Tween(2f).SetEasing(Easing.EaseInOutSine).For(Character).RotateLocalTo(Step2, Initial))
                .QueueDelay(3.5f)
                .Queue(new Flow()
                    .Queue(new Tween(1f).SetEasing(Easing.EaseInOutCubic).For(Character).RotateY(-90f))
                    .At(0f, new Tween(1f).SetEasing(new BounceEasing(1)).For(Character).MoveLocalYTo(2f)))
                .Queue(new Flow()
                    .Queue(new Tween(3f).SetEasing(Easing.EaseInOutCubic).For(Character).MoveLocalXTo(10f))
                    .At(2f, new Tween(1f).SetEasing(Easing.EaseInOutCubic).For(Character).RotateY(180)))
                .QueueDelay(0.5f)
                .Queue(new Tween(2.5f).SetEasing(Easing.EaseInOutCubic).For(Character).MoveTo(SplinePoints[0]).LookAt(Vector3.zero))
                .Queue(new Tween(18f).For(Character).MoveTo(GetSpline()).LookAt(Vector3.zero));

#if UNITY_EDITOR
        public void OnDraw()
        {
            FlowEntGizmos.DrawCurve(GetSpline(), new GizmoOptions(Color.black));
        }
#endif
    }
}
