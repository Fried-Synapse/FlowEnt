using System;
using System.IO;
using UnityEditor;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt
{
    public static class UIToolkitExtensions
    {
        private static T GetPath<T>(object obj, string extension)
            where T : UnityEngine.Object
        {
            string name = obj.GetType().Name;
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

        public static void LoadUxml(this VisualElement visualElement)
        {
            visualElement.Add(GetPath<VisualTreeAsset>(visualElement, "uxml").Instantiate());
        }

        public static void LoadUxml(this EditorWindow editorWindow)
        {
            editorWindow.rootVisualElement.Add(GetPath<VisualTreeAsset>(editorWindow, "uxml").Instantiate());
        }

        public static void LoadUss(this VisualElement visualElement)
        {
            visualElement.styleSheets.Add(GetPath<StyleSheet>(visualElement, "uss"));
        }

        public static void LoadUss(this EditorWindow editorWindow)
        {
            editorWindow.rootVisualElement.styleSheets.Add(GetPath<StyleSheet>(editorWindow, "uss"));
        }
    }
}
