using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Builder
{
    public class PlaygroundController : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> objects;
        private List<Transform> Objects => objects;

        private void Start()
        {
            Objects[0].Tween(1)
                .Start();
        }

        private void OnDrawGizmos()
        {
        }
    }
}