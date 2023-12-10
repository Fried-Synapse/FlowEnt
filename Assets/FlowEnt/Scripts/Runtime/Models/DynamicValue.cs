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

    //TODO this is a really neat concept but it brings a log of complications:
    //1 - it's not nullable. Which means it'll have to have a custom implementation for nullable option
    //2 - it's not backwards compatible.
    [Serializable]
    public struct DynamicValue<T>
    {
        [SerializeField]
        private DynamicValueType type;

        public DynamicValueType Type => type;

        [SerializeField]
        private T constant;

        public T Constant => constant;

        [SerializeField]
        private T randomMin;

        public T RandomMin => randomMin;

        [SerializeField]
        private T randomMax;

        public T RandomMax => randomMax;

        private T value;
        private bool hasValue;

        public T Value
        {
            get
            {
                if (!hasValue)
                {
                    hasValue = true;
                    value = type switch
                    {
                        DynamicValueType.Constant => value,
                        DynamicValueType.Random => GetRandom(),
                        _ => throw new NotImplementedException()
                    };
                }

                return value;
            }
        }

        private T GetRandom()
        {
            return (T)(object)((randomMin, randomMax) switch
            {
                (Color32 minColour, Color32 maxColour) => new Color32(
                    (byte)Random.Range(minColour.r, maxColour.r),
                    (byte)Random.Range(minColour.g, maxColour.g),
                    (byte)Random.Range(minColour.b, maxColour.b),
                    (byte)Random.Range(minColour.a, maxColour.a)),
                (Color minColour, Color maxColour) => new Color(
                    Random.Range(minColour.r, maxColour.r),
                    Random.Range(minColour.g, maxColour.g),
                    Random.Range(minColour.b, maxColour.b),
                    Random.Range(minColour.a, maxColour.a)),
                (float minFloat, float maxFloat) => Random.Range(minFloat, maxFloat),
                (int minInt, int maxInt) => Random.Range(minInt, maxInt),
                (Quaternion minQuaternion, Quaternion maxQuaternion) => Quaternion.Euler(new Vector3(
                    Random.Range(minQuaternion.eulerAngles.x, maxQuaternion.eulerAngles.x),
                    Random.Range(minQuaternion.eulerAngles.y, maxQuaternion.eulerAngles.y),
                    Random.Range(minQuaternion.eulerAngles.z, maxQuaternion.eulerAngles.z))),
                (Vector2 minVector, Vector2 maxVector) => new Vector2(
                    Random.Range(minVector.x, maxVector.x),
                    Random.Range(minVector.y, maxVector.y)),
                (Vector3 minVector, Vector3 maxVector) => new Vector3(
                    Random.Range(minVector.x, maxVector.x),
                    Random.Range(minVector.y, maxVector.y),
                    Random.Range(minVector.z, maxVector.z)),
                (Vector4 minVector, Vector4 maxVector) => new Vector4(
                    Random.Range(minVector.x, maxVector.x),
                    Random.Range(minVector.y, maxVector.y),
                    Random.Range(minVector.z, maxVector.z),
                    Random.Range(minVector.w, maxVector.w)),
                _ => throw new NotImplementedException()
            });
        }

        public static implicit operator T(DynamicValue<T> dynamicValue) => dynamicValue.Value;

        public static implicit operator DynamicValue<T>(T value) =>
            new DynamicValue<T>()
            {
                constant = value,
                randomMin = value,
                randomMax = value,
            };

        public static implicit operator DynamicValue<T>((T, T) value) =>
            new DynamicValue<T>()
            {
                constant = value.Item1,
                randomMin = value.Item1,
                randomMax = value.Item2,
            };
    }
}