using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal abstract class FlowEntWindow<TWindow> : EditorWindow
        where TWindow : FlowEntWindow<TWindow>
    {
#pragma warning disable IDE0044, RCS1169
        [SerializeField]
        private Texture2D logo;
        protected Texture2D Logo => logo;
        [SerializeField]
        private VisualTreeAsset contentAsset;
#pragma warning restore IDE0044, RCS1169
        protected abstract string Name { get; }
        internal static TWindow Instance { get; private set; }
        protected VisualElement Content { get; private set; }
        private static Type[] types;
        protected static Type[] Types
        {
            get
            {
                if (types == null)
                {
                    Type type = typeof(TWindow);
                    List<Type> newTypes = type.Assembly.GetTypes().Where(t => t.IsClass && t.IsSubclassOf(type)).ToList();
                    newTypes.Add(type);
                    types = newTypes.ToArray();
                }
                return types;
            }
        }

#pragma warning disable RCS1158
        internal static void ShowSingleton()
        {
            if (Instance == null)
            {
                Instance = GetWindow<TWindow>();
                Instance.titleContent = new GUIContent(Instance.Name, Instance.Logo);
            }
            else
            {
                Instance.Focus();
            }
        }

        internal static void ShowGrouped()
        {
            TWindow window = CreateWindow<TWindow>(Types);
            window.titleContent = new GUIContent(window.Name, Instance.Logo);
        }
#pragma warning restore RCS1158

#pragma warning disable IDE0051, RCS1213
        private void OnEnable()
        {
            Instance ??= (TWindow)this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }
#pragma warning restore IDE0051, RCS1213

        protected abstract void CreateGUI();

        protected void LoadHeader()
        {
            Header header = new Header(Name);
            header.AddToClassList("header-wrapper");
            rootVisualElement.Add(header);
        }

        protected void LoadContent()
        {
            rootVisualElement.LoadUxml(contentAsset);
            Content = rootVisualElement.Query("content");
        }
    }
}
