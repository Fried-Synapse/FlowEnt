using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public class GradientOperation
    {
        private const int MaxKeys = 8;

        private Gradient a;
        private Gradient b;
        private readonly Gradient result = new Gradient();
        private List<float> keys;

        private void Init(Gradient a, Gradient b)
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
            this.a = a;
            this.b = b;
            this.keys = keys.Take(Math.Min(keys.Count, MaxKeys)).ToList();
        }

        private Gradient RunOperation(Gradient a, Gradient b, Func<float, Color> operation)
        {
            if (this.a != a || this.b != b)
            {
                Init(a, b);
            }

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

        public Gradient Addition(Gradient a, Gradient b)
            => RunOperation(a, b, (key) => a.Evaluate(key) + b.Evaluate(key));

        public Gradient Difference(Gradient a, Gradient b)
            => RunOperation(a, b, (key) => a.Evaluate(key) - b.Evaluate(key));

        public Gradient Lerp(Gradient a, Gradient b, float t)
                => RunOperation(a, b, (key) => Color.Lerp(a.Evaluate(key), b.Evaluate(key), t));

        public Gradient LerpUnclamped(Gradient a, Gradient b, float t)
                => RunOperation(a, b, (key) => Color.LerpUnclamped(a.Evaluate(key), b.Evaluate(key), t));
    }
}
