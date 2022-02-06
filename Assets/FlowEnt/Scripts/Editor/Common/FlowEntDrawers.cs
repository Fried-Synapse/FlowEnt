using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    internal static class FlowEntDrawers
    {
        internal static Rect GetRect(Rect position, int index)
            => GetRect(position, index, FlowEntConstants.SpacedSingleLineHeight, FlowEntConstants.SingleLineHeight);

        internal static Rect GetRect(Rect position, int index, float spacedLineHeight, float lineHeight)
            => new Rect(position.x, position.y + (index * spacedLineHeight), position.width, lineHeight);
    }
}
