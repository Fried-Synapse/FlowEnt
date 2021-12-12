using System;
using UnityEngine;

namespace FriedSynapse.Quickit
{
    public static class MathExtensions
    {
    }

    public static class Mathq
    {
        public static float Round(float value, int digits = 0)
        {
            float multiplier = Mathf.Pow(10, digits);
            return Mathf.Round(value * multiplier) / multiplier;
        }

        public static Vector2 Round(Vector2 value, int digits = 0)
        {
            value.x = Round(value.x, digits);
            value.y = Round(value.y, digits);
            return value;
        }

        public static Vector3 Round(Vector3 value, int digits = 0)
        {
            value.x = Round(value.x, digits);
            value.y = Round(value.y, digits);
            value.z = Round(value.z, digits);
            return value;
        }

        public static Vector4 Round(Vector4 value, int digits = 0)
        {
            value.x = Round(value.x, digits);
            value.y = Round(value.y, digits);
            value.z = Round(value.z, digits);
            value.w = Round(value.w, digits);
            return value;
        }
    }
}
