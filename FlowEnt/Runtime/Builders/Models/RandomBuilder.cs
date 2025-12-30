using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class RandomBuilder<T> : AbstractBuilder<T>
        where T : struct
    {
        [SerializeField]
        private T min;

        public T Min => min;

        [SerializeField]
        private T max;

        public T Max => max;

        public override T Build()
        {
            return (T)(object)((randomMin: min, randomMax: max) switch
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
    }
}