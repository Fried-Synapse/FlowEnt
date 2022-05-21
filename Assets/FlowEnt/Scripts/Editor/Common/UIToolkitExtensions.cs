using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt
{
    internal static class UIToolkitExtensions
    {
        private static T Get<T>(string name, string extension)
            where T : UnityEngine.Object
        {
            string[] guids = AssetDatabase.FindAssets($"{name} t:script");

            string filterGuids()
            {
                foreach (string guid in guids)
                {
                    string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                    if (Path.GetFileNameWithoutExtension(assetPath) == name)
                    {
                        return guid;
                    }
                }
                return string.Empty;
            }

            string assetGuid = guids.Length switch
            {
                0 => throw new ArgumentException($"{name} could not be found."),
                1 => guids[0],
                _ => filterGuids()
            };

            return AssetDatabase.LoadAssetAtPath<T>(Path.ChangeExtension(AssetDatabase.GUIDToAssetPath(assetGuid), extension));
        }

        internal static void AddAll(this VisualElement visualElement, IEnumerable<VisualElement> items)
        {
            foreach (VisualElement item in items)
            {
                visualElement.Add(item);
            }
        }

        internal static void LoadUxml<T>(this VisualElement visualElement)
            => visualElement.AddAll(new List<VisualElement>(Get<VisualTreeAsset>(typeof(T).Name, "uxml").CloneTree().Children()));

        internal static void LoadUxml(this VisualElement visualElement)
            => visualElement.AddAll(new List<VisualElement>(Get<VisualTreeAsset>(visualElement.GetType().Name, "uxml").CloneTree().Children()));

        internal static void LoadUxml(this EditorWindow editorWindow)
            => editorWindow.rootVisualElement.AddAll(new List<VisualElement>(Get<VisualTreeAsset>(editorWindow.GetType().Name, "uxml").CloneTree().Children()));

        internal static void LoadUss<T>(this VisualElement visualElement)
            => visualElement.styleSheets.Add(Get<StyleSheet>(typeof(T).Name, "uss"));

        internal static void LoadUss(this VisualElement visualElement)
            => visualElement.styleSheets.Add(Get<StyleSheet>(visualElement.GetType().Name, "uss"));

        internal static void LoadUss(this EditorWindow editorWindow)
            => editorWindow.rootVisualElement.styleSheets.Add(Get<StyleSheet>(editorWindow.GetType().Name, "uss"));
    }
}
