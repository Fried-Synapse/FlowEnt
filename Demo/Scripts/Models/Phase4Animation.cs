using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FriedSynapse.FlowEnt.Demo
{
    [Serializable]
    public class Phase4Animation : AbstractDemoAnimation
    {
        [SerializeField]
        private List<Transform> critters;
        private List<Transform> Critters => critters;

        public override AbstractAnimation GetAnimation()
        {
            Flow flow = new Flow();

            foreach (Transform critter in Critters)
            {
                Vector3 firstDestination = new Vector3(GetRandomAxis(1f, 8f), 0f, GetRandomAxis(1f, 8f));
                Vector3 secondDestination = new Vector3(GetRandomAxis(1f, 8f), 0f, GetRandomAxis(1f, 8f));
                Flow critterFlow = new Flow()
                    .QueueDeferred(() =>
                    {
                        critter.gameObject.SetActive(true);
                        critter.LookAt(firstDestination);
                        return new Tween(1f).For(critter).MoveLocalToY(-0.5f, 0f).ScaleLocalToY(0f, 1f);
                    })
                    .Queue(new Tween(Random.Range(3f, 4f)).For(critter).MoveLocalTo(firstDestination).OrientToPath())
                    .Queue(new Tween(Random.Range(2f, 3f)).For(critter).MoveLocalTo(secondDestination).OrientToPath())
                    .Queue(new Tween(4f).For(critter).MoveLocalTo(new Vector3(100f, 0f, 0)).OrientToPath()
                                        .For(critter.GetComponent<MeshRenderer>()).LateAlphaTo(0f, 0.8f))
                    .OnCompleted(() => critter.gameObject.SetActive(false));

                flow.At(Random.Range(0f, 0.5f), critterFlow);
            }
            return flow;
        }

        private float GetRandomAxis(float min, float max)
        {
            int sign = Random.Range(0, 2);
            return (sign == 0 ? -1 : 1) * Random.Range(min, max);
        }
    }
}
