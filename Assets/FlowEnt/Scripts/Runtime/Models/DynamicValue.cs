using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FriedSynapse.FlowEnt
{
    public enum DynamicValueType
    {
        Constant,
        Random
    }

    [Serializable]
    public abstract class DynamicValue
    {
        [SerializeField]
        protected DynamicValueType type;
    }

    [Serializable]
    public class DynamicValue<TValue> : DynamicValue
    {
        [SerializeField]
        protected TValue value;

        [SerializeField]
        protected TValue min;

        [SerializeField]
        protected TValue max;

        public TValue Value
        {
            get =>
                type switch
                {
                    DynamicValueType.Constant => value,
                    DynamicValueType.Random => (TValue)(object)((min, max) switch
                    {
                        (float minFloat, float maxFloat) => Random.Range(minFloat, maxFloat),
                        (int minInt, int maxInt) => Random.Range(minInt, maxInt),
                        (Vector2 minVector, Vector2 maxVector) => new Vector2(
                            Random.Range(minVector.x, maxVector.x),
                            Random.Range(minVector.y, maxVector.y)),
                        (Vector3 minVector, Vector3 maxVector) => new Vector3(
                            Random.Range(minVector.x, maxVector.x),
                            Random.Range(minVector.y, maxVector.y),
                            Random.Range(minVector.z, maxVector.z)),
                        _ => throw new NotImplementedException()
                    }),
                    _ => throw new ArgumentOutOfRangeException()
                };
            set => this.value = value;
        }

        public static implicit operator TValue(DynamicValue<TValue> dynamicValue) => dynamicValue.Value;

        public static explicit operator DynamicValue<TValue>(TValue value) =>
            new DynamicValue<TValue>()
            {
                value = value
            };
    }
}