using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal abstract class AbstractBaseWindow<TWindow> : EditorWindow
        where TWindow : AbstractBaseWindow<TWindow>
    {
        [SerializeField]
        private VisualTreeAsset contentAsset;

        internal static TWindow Instance { get; private protected set; }
        internal static bool IsAvailable => Instance != null && Instance.Content != null;

        protected VisualElement Content { get; private set; }

        protected abstract void CreateGUI();

        private void OnEnable()
        {
            Instance ??= (TWindow)this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        protected void LoadContent()
        {
            rootVisualElement.LoadUxml(contentAsset);
            Content = rootVisualElement.Query("content");
        }

        protected void TryClose()
        {
            try
            {
                Instance.Close();
            }
            catch
            {
                //HACK: there is a bug in unity that crashes on Close when it doesn't have a parent but there is no way to check for one...
            }
        }
    }

    internal abstract class AbstractThemedWindow<TWindow> : AbstractBaseWindow<TWindow>
        where TWindow : AbstractThemedWindow<TWindow>
    {
        [SerializeField]
        private Texture2D logo;

        protected Texture2D Logo => logo;
        protected abstract string Name { get; }

        private static Type[] types;
        protected static Type[] Types
        {
            get
            {
                if (types == null)
                {
                    Type type = typeof(TWindow);
                    List<Type> newTypes = type.Assembly.GetTypes().Where(t => t.IsClass && t.IsSubclassOf(type))
                        .ToList();
                    newTypes.Add(type);
                    types = newTypes.ToArray();
                }

                return types;
            }
        }

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

        protected void LoadHeader()
        {
            Header header = new Header(Name);
            header.AddToClassList("header-wrapper");
            rootVisualElement.Add(header);
        }
    }
}