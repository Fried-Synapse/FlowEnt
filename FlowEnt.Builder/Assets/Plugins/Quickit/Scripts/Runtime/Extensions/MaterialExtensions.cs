using UnityEngine;

namespace FriedSynapse.Quickit
{
    public static class MaterialExtensions
    {
        public static void SetAlpha(this Material material, float alpha)
        {
            Color color = material.color;
            color.a = alpha;
            material.color = color;
        }
    }
}
