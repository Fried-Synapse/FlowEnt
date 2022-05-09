using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(EchoBuilder))]
    public class EchoBuilderPropertyDrawer : AbstractAnimationBuilderPropertyDrawer<Echo, EchoBuilder>
    {
    }
}
