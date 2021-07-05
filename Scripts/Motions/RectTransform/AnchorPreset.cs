using System;
using UnityEngine;

namespace FlowEnt.Motions.RectTransformMotions
{
    public enum AnchorPreset
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

        VerticalStretchLeft,
        VerticalStretchCenter,
        VerticalStretchRight,

        HorizontalStretchBottom,
        HorizontalStretchMiddle,
        HorizontalStretchTop,

        StretchAll
    }

    public class AnchorPresetData
    {
        public AnchorPresetData(Vector2 min, Vector2 max)
        {
            Min = min;
            Max = max;
        }

        public Vector2 Min { get; set; }
        public Vector2 Max { get; set; }
    }

    public static class AnchorPresetFactory
    {
        public static AnchorPresetData GetAnchors(AnchorPreset preset)
        {
            switch (preset)
            {
                case AnchorPreset.BottomLeft:
                    return new AnchorPresetData(new Vector2(0f, 0f), new Vector2(0f, 0f));
                case AnchorPreset.BottomCenter:
                    return new AnchorPresetData(new Vector2(0.5f, 0f), new Vector2(0.5f, 0f));
                case AnchorPreset.BottomRight:
                    return new AnchorPresetData(new Vector2(1f, 0f), new Vector2(1f, 0f));

                case AnchorPreset.MiddleLeft:
                    return new AnchorPresetData(new Vector2(0f, 0.5f), new Vector2(0f, 0.5f));
                case AnchorPreset.MiddleCenter:
                    return new AnchorPresetData(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
                case AnchorPreset.MiddleRight:
                    return new AnchorPresetData(new Vector2(1f, 0.5f), new Vector2(1f, 0.5f));

                case AnchorPreset.TopRight:
                    return new AnchorPresetData(new Vector2(1f, 1f), new Vector2(1f, 1f));
                case AnchorPreset.TopCenter:
                    return new AnchorPresetData(new Vector2(0.5f, 1f), new Vector2(0.5f, 1f));
                case AnchorPreset.TopLeft:
                    return new AnchorPresetData(new Vector2(0f, 1f), new Vector2(0f, 1f));

                case AnchorPreset.VerticalStretchLeft:
                    return new AnchorPresetData(new Vector2(0f, 0f), new Vector2(0f, 1f));
                case AnchorPreset.VerticalStretchCenter:
                    return new AnchorPresetData(new Vector2(0.5f, 0f), new Vector2(0.5f, 1f));
                case AnchorPreset.VerticalStretchRight:
                    return new AnchorPresetData(new Vector2(1f, 0f), new Vector2(1f, 1f));

                case AnchorPreset.HorizontalStretchBottom:
                    return new AnchorPresetData(new Vector2(0f, 0f), new Vector2(1f, 0f));
                case AnchorPreset.HorizontalStretchMiddle:
                    return new AnchorPresetData(new Vector2(0f, 0.5f), new Vector2(1f, 0.5f));
                case AnchorPreset.HorizontalStretchTop:
                    return new AnchorPresetData(new Vector2(0f, 1f), new Vector2(1f, 1f));

                case AnchorPreset.StretchAll:
                    return new AnchorPresetData(new Vector2(0f, 0f), new Vector2(1f, 1f));
            }
            throw new ArgumentException($"Unknown preset type {preset}.");
        }

    }
}
