using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class ComponentsContainer : ScriptableObject
    {
#pragma warning disable IDE0044, RCS1169
        [SerializeField]
        private List<VisualTreeAsset> visualTreeAssets;
        [SerializeField]
        private List<StyleSheet> styleSheets;
#pragma warning restore IDE0044, RCS1169

        internal VisualTreeAsset GetUxml(string name)
            => visualTreeAssets.Find(a => a.name == name);
        internal StyleSheet GetUss(string name)
            => styleSheets.Find(a => a.name == name);
    }
}
