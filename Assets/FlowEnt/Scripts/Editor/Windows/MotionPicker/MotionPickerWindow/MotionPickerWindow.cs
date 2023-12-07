using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class MotionPickerWindow : AbstractBaseWindow<MotionPickerWindow>
    {
        private class TypeInfo
        {
            public Type Type { get; set; }
            public List<string> Names { get; set; } = new List<string>();
            public int SearchIndex { get; private set; }

            public void ComputeSearchIndex(List<string> parts)
            {
                SearchIndex = parts.Count(sp => Names.Any(n => n.IndexOf(sp, StringComparison.OrdinalIgnoreCase) >= 0));
            }

            public string GetToolTip()
            {
                return "xxx\nyyyy";
            }
        }

        private const string Name = "Select motion...";

        private Action<Type> callback;
        private string searchText;
        private List<TypeInfo> types = new List<TypeInfo>();

        private TextField searchBox;
        private Foldout favourites;
        private Foldout recent;
        private Foldout all;


        private PersistentEditorPrefBool FavouritesFoldout { get; set; } =
            new PersistentEditorPrefBool(FlowEntEditorPrefs.FavouritesFoldoutKey);

        private PersistentEditorPrefBool RecentFoldout { get; set; } =
            new PersistentEditorPrefBool(FlowEntEditorPrefs.RecentFoldoutKey);

        private PersistentEditorPrefBool AllFoldout { get; set; } =
            new PersistentEditorPrefBool(FlowEntEditorPrefs.AllFoldoutKey, true);

        private PersistentEditorPrefListString Favourites { get; set; } =
            new PersistentEditorPrefListString(FlowEntEditorPrefs.FavouritesKey);

        internal static void Show<TMotionBuilder>(Action<TMotionBuilder> callback)
            where TMotionBuilder : IMotionBuilder
        {
            if (Instance != null)
            {
                Instance.Close();
            }

            Instance = GetWindow<MotionPickerWindow>(false, Name);
            Instance = CreateWindow<MotionPickerWindow>(Name);
            Instance.ShowModalUtility();
            Instance.titleContent = new GUIContent(Name);
            Instance.types = GetTypes<TMotionBuilder>().OrderBy(t => t.Names[0]).ToList();
            Instance.callback = type => callback.Invoke((TMotionBuilder)Activator.CreateInstance(type));
            Instance.Redraw();
        }

        protected override void CreateGUI()
        {
            LoadContent();
            searchBox = Content.Query<TextField>("searchBox").First();
            favourites = GetInitFoldout("favourites", FavouritesFoldout);
            recent = GetInitFoldout("recent", RecentFoldout);
            all = GetInitFoldout("all", AllFoldout);
        }

        private FoldoutScrollable GetInitFoldout(string name, PersistentEditorPrefBool editorPrefs)
        {
            FoldoutScrollable foldout = Content.Query<FoldoutScrollable>(name).First();
            foldout.value = editorPrefs;
            foldout.RegisterValueChangedCallback(value => editorPrefs.Value = value.newValue);
            return foldout;
        }

        private void Redraw(string searchTerm = null)
        {
            foreach (TypeInfo typeInfo in types)
            {
                all.Add(new MotionElement());
            }
        }

        private static List<TypeInfo> GetTypes<TMotionBuilder>()
            where TMotionBuilder : IMotionBuilder
        {
            List<TypeInfo> objects = new List<TypeInfo>();
            Type type = typeof(TMotionBuilder);

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                objects.AddRange(assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(type))
                    .Select(t => new TypeInfo() { Type = t, Names = MotionNameGetter.GetNames(t) }));
            }

            return objects;
        }
    }
}