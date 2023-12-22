using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class MotionPickerWindow : AbstractBaseWindow<MotionPickerWindow>
    {
        private const string Name = "Select motion...";

        private Type selectionMotionType;
        private Action<Type> callback;
        private List<MotionTypeInfo> motionsTypeInfo;

        private TextField searchBox;
        private Toggle autoClose;
        private FoldoutScrollable favourites;
        private FoldoutScrollable recent;
        private FoldoutScrollable all;

        private PersistentEditorPrefBool FavouritesFoldoutPrefs { get; } =
            new (FlowEntEditorPrefs.FavouritesFoldoutKey);

        private PersistentEditorPrefBool RecentFoldoutPrefs { get; } =
            new (FlowEntEditorPrefs.RecentFoldoutKey);

        private PersistentEditorPrefBool AllFoldoutPrefs { get; } =
            new (FlowEntEditorPrefs.AllFoldoutKey, true);

        private PersistentEditorPrefListString FavouritesPrefs { get; } =
            new (FlowEntEditorPrefs.FavouritesKey, new List<string>());

        private Dictionary<string, PersistentEditorPrefListString> RecentPrefsDictionary { get; } = new();
        
        private PersistentEditorPrefListString RecentPrefs
        {
            get
            {
                if (!RecentPrefsDictionary.TryGetValue(selectionMotionType.Name,
                        out PersistentEditorPrefListString recentEditorPrefs))
                {
                    recentEditorPrefs = new PersistentEditorPrefListString(
                        FlowEntEditorPrefs.RecentKey + selectionMotionType.Name, new List<string>());
                    RecentPrefsDictionary.Add(selectionMotionType.Name, recentEditorPrefs);
                }

                return recentEditorPrefs;
            }
        }

        internal static void Show<TMotionBuilder>(Action<TMotionBuilder> callback)
            where TMotionBuilder : IMotionBuilder
        {
            Instance?.TryClose();
            Instance = CreateWindow<MotionPickerWindow>(Name);
            Instance.Show();
            Instance.titleContent = new GUIContent(Name);
            Instance.selectionMotionType = typeof(TMotionBuilder);
            Instance.motionsTypeInfo =
                MotionTypeInfo.GetTypes<TMotionBuilder>().OrderBy(t => t.Names.Preferred).ToList();
            Instance.callback = type => callback.Invoke((TMotionBuilder)Activator.CreateInstance(type));
            Instance.Redraw();
        }

        protected override void CreateGUI()
        {
            ApplyHacks();
            LoadContent();
            searchBox = Content.Query<TextField>("searchBox").First();
            searchBox.Focus();
            autoClose = Content.Query<Toggle>("autoClose").First();
            favourites = queryAndInitFoldout("favourites", FavouritesFoldoutPrefs);
            recent = queryAndInitFoldout("recent", RecentFoldoutPrefs);
            all = queryAndInitFoldout("all", AllFoldoutPrefs);
            Bind();

            FoldoutScrollable queryAndInitFoldout(string name, PersistentEditorPrefBool editorPrefs)
            {
                FoldoutScrollable foldout = Content.Query<FoldoutScrollable>(name).First();
                foldout.value = editorPrefs.Value;
                foldout.RegisterValueChangedCallback(value => editorPrefs.Value = value.newValue);
                return foldout;
            }
        }

        private void OnLostFocus()
        {
            EditorApplication.delayCall += () => Instance?.Close();
        }

        private void ApplyHacks()
        {
            //HACK if the windows stayed we will try to close it because we don't want an empty window.
            //we need to wait 1 frame in order to ensure we didn't just open this window intentionally
            EditorApplication.delayCall += () =>
            {
                if (motionsTypeInfo == null)
                {
                    TryClose();
                }
            };

            //HACK this removes the tab at the top and makes it look like a popup
            EditorApplication.delayCall += () => rootVisualElement.style.top = 0;
        }

        private void Bind()
        {
            searchBox.RegisterValueChangedCallback(_ => Redraw());
        }

        private void Redraw()
        {
            List<string> favouritesPrefs = FavouritesPrefs.Value;
            Redraw(favourites, motionsTypeInfo.Where(t => favouritesPrefs.Contains(t.Names.FullName)).ToList());
            List<string> recentPrefs = RecentPrefs.Value;
            List<MotionTypeInfo> recentMotions = motionsTypeInfo
                .Where(t => recentPrefs.Contains(t.Names.FullName))
                .OrderBy(t => recentPrefs.IndexOf(t.Names.FullName))
                .ToList();
            Redraw(recent, recentMotions);
            Redraw(all, motionsTypeInfo);
        }

        private void Redraw(FoldoutScrollable foldoutScrollable, List<MotionTypeInfo> motionsTypeInfo)
        {
            string searchText = searchBox.text;
            if (!string.IsNullOrEmpty(searchText))
            {
                motionsTypeInfo = Search(motionsTypeInfo, searchText);
            }

            foldoutScrollable.Clear();
            foreach (MotionTypeInfo motionTypeInfo in motionsTypeInfo)
            {
                foldoutScrollable.Add(
                    new MotionElement(motionTypeInfo, FavouritesPrefs.Value.Contains(motionTypeInfo.Names.FullName))
                    {
                        OnSelected = motionTypeInfo =>
                        {
                            callback?.Invoke(motionTypeInfo.Type);
                            AddRecent(motionTypeInfo);
                            if (autoClose.value)
                            {
                                Instance.Close();
                            }
                            else
                            {
                                Redraw();
                            }
                        },
                        OnFavouriteChanged = isSelected =>
                        {
                            List<string> favouritePrefsValue = FavouritesPrefs.Value;

                            if (isSelected)
                            {
                                favouritePrefsValue.Add(motionTypeInfo.Names.FullName);
                            }
                            else
                            {
                                favouritePrefsValue.Remove(motionTypeInfo.Names.FullName);
                            }

                            FavouritesPrefs.Value = favouritePrefsValue;
                            Redraw();
                        }
                    });
            }
        }

        private void AddRecent(MotionTypeInfo motionTypeInfo)
        {
            List<string> recentPrefsValue = RecentPrefs.Value;
            if (recentPrefsValue.Contains(motionTypeInfo.Names.FullName))
            {
                recentPrefsValue.Remove(motionTypeInfo.Names.FullName);
            }

            recentPrefsValue.Insert(0, motionTypeInfo.Names.FullName);

            if (recentPrefsValue.Count > 10)
            {
                recentPrefsValue.RemoveAt(recentPrefsValue.Count - 1);
            }

            RecentPrefs.Value = recentPrefsValue;
        }

        private static List<MotionTypeInfo> Search(List<MotionTypeInfo> motionsTypeInfo, string searchText)
        {
            List<string> searchParts = searchText
                .Split(" ")
                .Where(sp => !string.IsNullOrEmpty(sp))
                .ToList();

            motionsTypeInfo.ForEach(t => t.ComputeSearchIndex(searchParts));
            return motionsTypeInfo
                .Where(t => t.SearchIndex > 0)
                .OrderByDescending(t => t.SearchIndex)
                .ToList();
        }
    }
}