#if UNITY_EDITOR
using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class GizmoOptions
    {
        private const float DefaultWidth = 1f;
        private static readonly Color DefaultColour = Color.white;

        public GizmoOptions(Color colour = default, float width = DefaultWidth)
        {
            if (colour == default)
            {
                colour = DefaultColour;
            }

            Colour = colour;
            Width = width;
        }


        [SerializeField]
        private Color colour = DefaultColour;

        public Color Colour { get => colour; set => colour = value; }


        [SerializeField]
        private float width = DefaultWidth;

        public float Width { get => width; set => width = value; }

        public float Step { get; set; } = 0.001f;
    }
}
#endif