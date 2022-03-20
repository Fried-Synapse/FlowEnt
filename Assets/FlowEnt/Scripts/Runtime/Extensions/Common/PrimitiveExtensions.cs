using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public static class PrimitiveExtensions
    {
        public static Gradient Lerp(Gradient a, Gradient b, float t)
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
