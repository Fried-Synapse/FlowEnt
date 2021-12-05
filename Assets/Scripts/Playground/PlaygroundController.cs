using System.Collections.Generic;
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

        private void Start()
        {
        }

        private void OnDrawGizmos()
        {
        }
    }
}