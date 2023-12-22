using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.LineRenderers
{
    /// <summary>
    /// Lerps the position of a vertex using <see cref="LineRenderer.SetPosition(int, Vector3)" />.
    /// </summary>
    public class MoveVertexVectorMotion : AbstractVector3Motion<LineRenderer>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            [SerializeField]
            private int index;
            public override ITweenMotion Build()
                => new MoveVertexVectorMotion(item, index, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            [SerializeField]
            private int index;
            public override ITweenMotion Build()
                => new MoveVertexVectorMotion(item, index, From, to);
        }

        public MoveVertexVectorMotion(LineRenderer item, int index, Vector3 value) : base(item, value)
        {
            this.index = index;
        }

        public MoveVertexVectorMotion(LineRenderer item, int index, Vector3? from, Vector3 to) : base(item, from, to)
        {
            this.index = index;
        }

        private readonly int index;

        protected override Vector3 GetFrom() => item.GetPosition(index);
        protected override void SetValue(Vector3 value) => item.SetPosition(index, value);
    }
}
