using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(TweenBuilder))]
    public class TweenBuilderPropertyDrawer : AbstractAnimationBuilderPropertyDrawer<Tween, TweenBuilder>
    {
        private const PlayState Started = PlayState.Playing | PlayState.Paused;
    }
}
