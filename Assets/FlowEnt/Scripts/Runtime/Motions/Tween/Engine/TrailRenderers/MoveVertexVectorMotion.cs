using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.TrailRenderers
{
    /// <summary>
    /// Lerps the position of a vertex using <see cref="TrailRenderer.SetPosition(int, Vector3)" />.
    /// </summary>
    public class MoveVertexVectorMotion : AbstractVector3Motion<TrailRenderer>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
#pragma warning disable IDE0044, RCS1169
            [SerializeField]
            private int index;
#pragma warning restore IDE0044, RCS1169
            public override ITweenMotion Build()
                => new MoveVertexVectorMotion(item, index, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
#pragma warning disable IDE0044, RCS1169
            [SerializeField]
            private int index;
#pragma warning restore IDE0044, RCS1169
            public override ITweenMotion Build()
                => new MoveVertexVectorMotion(item, index, From, to);
        }

        public MoveVertexVectorMotion(TrailRenderer item, int index, Vector3 value) : base(item, value)
        {
            this.index = index;
        }

        public MoveVertexVectorMotion(TrailRenderer item, int index, Vector3? from, Vector3 to) : base(item, from, to)
        {
            this.index = index;
        }

        private readonly int index;

        protected override Vector3 GetFrom() => item.GetPosition(index);
        protected override void SetValue(Vector3 value) => item.SetPosition(index, value);
    }
}
