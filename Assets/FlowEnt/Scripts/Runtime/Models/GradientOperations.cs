using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public class GradientOperations
    {
        private const int MaxKeys = 8;

        private Gradient a;
        private Gradient b;
        private readonly Gradient result = new Gradient();
        private List<float> keys;

        public Gradient Lerp(Gradient a, Gradient b, float t)
            => Execute(a, b, (key) => Color.Lerp(a.Evaluate(key), b.Evaluate(key), t));

        public Gradient LerpUnclamped(Gradient a, Gradient b, float t)
            => Execute(a, b, (key) => Color.LerpUnclamped(a.Evaluate(key), b.Evaluate(key), t));

        private Gradient Execute(Gradient a, Gradient b, Func<float, Color> operation)
        {
            if (this.a != a || this.b != b)
            {
                keys = Init(a, b);
                this.a = a;
                this.b = b;
            }
            return Execute(keys, operation, result);
        }

        public static Gradient Add(Gradient a, Gradient b)
            => Execute(Init(a, b), (key) => a.Evaluate(key) + b.Evaluate(key), new Gradient());

        public static Gradient Subtract(Gradient a, Gradient b)
            => Execute(Init(a, b), (key) => a.Evaluate(key) - b.Evaluate(key), new Gradient());

        public static bool AreEqual(Gradient a, Gradient b)
        {
            if (a.colorKeys.Length != b.colorKeys.Length || a.alphaKeys.Length != b.alphaKeys.Length)
            {
                return false;
            }

            for (int i = 0; i < a.colorKeys.Length; i++)
            {
                GradientColorKey colorKeyA = a.colorKeys[i];
                GradientColorKey colorKeyB = b.colorKeys[i];
                if (colorKeyA.time != colorKeyB.time || colorKeyA.color != colorKeyB.color)
                {
                    return false;
                }

                GradientAlphaKey alphaKeyA = a.alphaKeys[i];
                GradientAlphaKey alphaKeyB = b.alphaKeys[i];
                if (alphaKeyA.time != alphaKeyB.time || alphaKeyA.alpha != alphaKeyB.alpha)
                {
                    return false;
                }
            }

            return true;
        }

        public static Gradient Generate(params Color[] colours)
        {
            const int minKeys = 2;
            if (colours.Length < minKeys || colours.Length > MaxKeys)
            {
                throw new ArgumentException($"{nameof(Generate)} needs between {minKeys} and {MaxKeys} colours.");
            }
            GradientColorKey[] colourKeys = new GradientColorKey[colours.Length];
            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[colours.Length];

            for (int i = 0; i < colours.Length; i++)
            {
                Color colour = colours[i];
                float key = i / (float)(colours.Length - 1f);
                colourKeys[i] = new GradientColorKey(colour, key);
                alphaKeys[i] = new GradientAlphaKey(colour.a, key);
            }

            Gradient result = new Gradient();
            result.SetKeys(colourKeys, alphaKeys);

            return result;
        }

        private static Gradient Execute(List<float> keys, Func<float, Color> operation, Gradient result)
        {
            GradientColorKey[] colours = new GradientColorKey[keys.Count];
            GradientAlphaKey[] alphas = new GradientAlphaKey[keys.Count];

            for (int i = 0; i < keys.Count; i++)
            {
                float key = keys[i];
                Color colour = operation(key);
                colours[i] = new GradientColorKey(colour, key);
                alphas[i] = new GradientAlphaKey(colour.a, key);
            }

            result.SetKeys(colours, alphas);

            return result;
        }

        private static List<float> Init(Gradient a, Gradient b)
        {
            List<float> keys = new List<float>();
            void tryAddKey(float key)
            {
                if (!keys.Contains(key))
                {
                    keys.Add(key);
                }
            }
            for (int i = 0; i < a.colorKeys.Length; i++)
            {
                tryAddKey(a.colorKeys[i].time);
            }

            for (int i = 0; i < b.colorKeys.Length; i++)
            {
                tryAddKey(b.colorKeys[i].time);
            }

            for (int i = 0; i < a.alphaKeys.Length; i++)
            {
                tryAddKey(a.alphaKeys[i].time);
            }

            for (int i = 0; i < b.alphaKeys.Length; i++)
            {
                tryAddKey(b.alphaKeys[i].time);
            }
            if (keys.Count > MaxKeys)
            {
                throw new InvalidOperationException($"The two gradients cannot have more than {MaxKeys} keys in total.");
            }
            return keys.Take(Math.Min(keys.Count, MaxKeys)).ToList();
        }
    }
}
