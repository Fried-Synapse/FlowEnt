using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlowEnt
{
    public class MoveToMotion : AbstractMotion<Transform>
    {
        public MoveToMotion(Transform item, Vector3 to) : base(item)
        {
            To = to;
        }

        public Vector3 To { get; }
        public Vector3? From { get; private set; }

        public override void OnStart()
        {
            From = Item.position;
        }

        public override void OnUpdate(float t)
        {
            Item.position = Vector3.LerpUnclamped(From.Value, To, t);
        }

        public override void OnComplete()
        {
        }
    }
}
