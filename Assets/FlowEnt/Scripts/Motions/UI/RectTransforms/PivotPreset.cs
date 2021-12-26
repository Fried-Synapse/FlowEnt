using System;
using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.UI.RectTransforms
{
    public enum PivotPreset
    {
        BottomLeft,
        BottomCenter,
        BottomRight,

        MiddleLeft,
        MiddleCenter,
        MiddleRight,

        TopLeft,
        TopCenter,
        TopRight,
    }

    public static class PivotPresetFactory
    {
        public static Vector2 GetPivot(PivotPreset preset)
        {
            switch (preset)
            {
                case PivotPreset.BottomLeft:
                    return new Vector2(0, 0);
                case PivotPreset.BottomCenter:
                    return new Vector2(0.5f, 0);
                case PivotPreset.BottomRight:
                    return new Vector2(1, 0);

                case PivotPreset.MiddleLeft:
                    return new Vector2(0, 0.5f);
                case PivotPreset.MiddleCenter:
                    return new Vector2(0.5f, 0.5f);
                case PivotPreset.MiddleRight:
                    return new Vector2(1, 0.5f);

                case PivotPreset.TopLeft:
                    return new Vector2(0, 1);
                case PivotPreset.TopCenter:
                    return new Vector2(0.5f, 1);
                case PivotPreset.TopRight:
                    return new Vector2(1, 1);
            }
            throw new ArgumentException($"Unknown preset type {preset}.");
        }
    }
}
