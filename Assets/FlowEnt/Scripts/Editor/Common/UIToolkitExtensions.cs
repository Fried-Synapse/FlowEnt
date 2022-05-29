using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal static class UIToolkitExtensions
    {
        private static ComponentsContainer componentsContainer;
        private static ComponentsContainer ComponentsContainer
        {
            get
            {
                if (componentsContainer == null)
                {
                    componentsContainer = Resources.Load<ComponentsContainer>("ComponentsContainer");
                }
                return componentsContainer;
            }
        }

        internal static void AddRange(this VisualElement visualElement, IEnumerable<VisualElement> items)
        {
            foreach (VisualElement item in items)
            {
                visualElement.Add(item);
            }
        }

        internal static void AddRange(this VisualElementStyleSheetSet styleSheets, IEnumerable<StyleSheet> items)
        {
            foreach (StyleSheet item in items)
            {
                styleSheets.Add(item);
            }
        }

        internal static void LoadUxml(this VisualElement visualElement, VisualTreeAsset visualTreeAsset)
        {
            visualElement.AddRange(new List<VisualElement>(visualTreeAsset.CloneTree().Children()));
            visualElement.styleSheets.AddRange(visualTreeAsset.stylesheets);
        }

        internal static void LoadUxml<T>(this VisualElement visualElement)
            => LoadUxml(visualElement, ComponentsContainer.GetUxml(typeof(T).Name));

        internal static void LoadUxml(this VisualElement visualElement)
            => LoadUxml(visualElement, ComponentsContainer.GetUxml(visualElement.GetType().Name));

        internal static void LoadUss<T>(this VisualElement visualElement)
            => visualElement.styleSheets.Add(ComponentsContainer.GetUss(typeof(T).Name));

        internal static void LoadUss(this VisualElement visualElement)
            => visualElement.styleSheets.Add(ComponentsContainer.GetUss(visualElement.GetType().Name));
    }
}
