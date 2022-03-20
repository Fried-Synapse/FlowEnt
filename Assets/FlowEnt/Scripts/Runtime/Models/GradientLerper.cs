using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public struct GradientLerper
    {
        private const int MaxKeys = 8;
        public GradientLerper(Gradient a, Gradient b)
        {
            this.a = a;
            this.b = b;
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
            this.keys = keys.Take(Math.Min(keys.Count, MaxKeys)).ToList();
        }

#pragma warning disable RCS1085
        private Gradient a;
        public Gradient A { get => a; set => a = value; }
        private Gradient b;
        public Gradient B { get => b; set => b = value; }
#pragma warning restore RCS1085
        private readonly List<float> keys;

        public Gradient Lerp(float t)
        {
            GradientColorKey[] colours = new GradientColorKey[keys.Count];
            GradientAlphaKey[] alphas = new GradientAlphaKey[keys.Count];

            for (int i = 0; i < keys.Count; i++)
            {
                float key = keys[i];
                Color colour = Color.Lerp(a.Evaluate(key), b.Evaluate(key), t);
                colours[i] = new GradientColorKey(colour, key);
                alphas[i] = new GradientAlphaKey(colour.a, key);
            }

            Gradient gradient = new Gradient();
            gradient.SetKeys(colours, alphas);

            return gradient;
        }
    }
}
