using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    public static class UIToolkitExtensions
    {
        private static StyleSheet GetUssPath(this object obj)
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
                        return guid; ;
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

            return AssetDatabase.LoadAssetAtPath<StyleSheet>(Path.ChangeExtension(AssetDatabase.GUIDToAssetPath(assetGuid), "uss"));
        }

        public static void LoadUss(this VisualElement visualElement)
        {
            visualElement.styleSheets.Add(GetUssPath(visualElement));
        }

        public static void LoadUss(this EditorWindow editorWindow)
        {
            editorWindow.rootVisualElement.styleSheets.Add(GetUssPath(editorWindow));
        }
    }
}
