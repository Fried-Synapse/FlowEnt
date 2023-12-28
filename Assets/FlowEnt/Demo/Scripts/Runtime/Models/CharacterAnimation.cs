using System;
using System.Collections.Generic;
using FriedSynapse.FlowEnt.Easings;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Demo
{
    [Serializable]
    public class CharacterAnimation : AbstractDemoAnimation, IEditorDrawer
    {
        private readonly Vector3 Initial = new Vector3(0, -180, 0);
        private readonly Vector3 Step1 = new Vector3(-15, -205, 0);
        private readonly Vector3 Step2 = new Vector3(-25, -155, 0);

#pragma warning disable RCS1169, IDE0044
        [SerializeField]
        private Transform character;
        private Transform Character => character;

        [SerializeField]
        private List<Vector3> splinePoints;
        private List<Vector3> SplinePoints => splinePoints;
#pragma warning restore RCS1169, IDE0044

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
            GetSpline().DrawGizmo(Color.black, 2f);
        }
#endif
    }
}
