using UnityEditor;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(TweenBuilder))]
    public class TweenBuilderPropertyDrawer : AbstractAnimationBuilderPropertyDrawer<Tween, TweenBuilder>
    {
    }
}
