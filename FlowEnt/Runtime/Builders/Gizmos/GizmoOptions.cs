#if UNITY_EDITOR
using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class GizmoOptions
    {
        private const float DefaultWidth = 1f;
        private static readonly Color DefaultColor = Color.white;

        public GizmoOptions()
        {
            Show = true;
            Color = DefaultColor;
            Width = DefaultWidth;
        }

        public GizmoOptions(Color color = default, float width = DefaultWidth)
        {
            if (color == default)
            {
                color = DefaultColor;
            }

            Color = color;
            Width = width;
        }

        [SerializeField]
        private bool isVisible;

        [SerializeField]
        private bool show = true;

        public bool Show { get => show; set => show = value; }

        [SerializeField]
        private Color color = DefaultColor;

        public Color Color { get => color; set => color = value; }


        [SerializeField]
        private float width = DefaultWidth;

        public float Width { get => width; set => width = value; }

        public Transform Transform { get; set; }
        public float Step { get; set; } = 0.001f;
    }
}
#endif