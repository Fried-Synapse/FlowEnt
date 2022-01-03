using System.Collections.Generic;
using FriedSynapse.FlowEnt.Motions.Echo;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Builder
{
    public class PlaygroundController : MonoBehaviour
    {
#pragma warning disable RCS1169, IDE0044
        [SerializeField]
        private List<Transform> objects;
        private List<Transform> Objects => objects;
#pragma warning restore RCS1169, IDE0044

#pragma warning disable IDE0051, RCS1213
        private void Start()
        {
            new Echo().KeyDown(KeyCode.S, (_) => Debug.Log("pressed")).Start();

            Objects[0].Echo()
                .MoveTo(Objects[1], 5, SpeedType.Gravity)
                .LookAt(Objects[1])
                .Start();
        }

        private void OnDrawGizmos()
        {
        }
#pragma warning restore IDE0051, RCS1213
    }
}