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

        private Action<Type> callback;
        private List<MotionTypeInfo> motionsTypeInfo;

        private TextField searchBox;
        private Toggle autoClose;
        private FoldoutScrollable favourites;
        private FoldoutScrollable recent;
        private FoldoutScrollable all;

        private PersistentEditorPrefBool FavouritesFoldoutPrefs { get; } =
            new PersistentEditorPrefBool(FlowEntEditorPrefs.FavouritesFoldoutKey);

        private PersistentEditorPrefBool RecentFoldoutPrefs { get; } =
            new PersistentEditorPrefBool(FlowEntEditorPrefs.RecentFoldoutKey);

        private PersistentEditorPrefBool AllFoldoutPrefs { get; } =
            new PersistentEditorPrefBool(FlowEntEditorPrefs.AllFoldoutKey, true);

        private PersistentEditorPrefListString FavouritesPrefs { get; } =
            new PersistentEditorPrefListString(FlowEntEditorPrefs.FavouritesKey, new List<string>());

        private PersistentEditorPrefListString RecentPrefs { get; } =
            new PersistentEditorPrefListString(FlowEntEditorPrefs.RecentKey, new List<string>());

        internal static void Show<TMotionBuilder>(Action<TMotionBuilder> callback)
            where TMotionBuilder : IMotionBuilder
        {
            Instance?.TryClose();
            Instance = GetWindow<MotionPickerWindow>(false, Name);
            Instance = CreateWindow<MotionPickerWindow>(Name);
            Instance.Show();
            Instance.titleContent = new GUIContent(Name);
            Instance.motionsTypeInfo =
                MotionTypeInfo.GetTypes<TMotionBuilder>().OrderBy(t => t.Names.Preferred).ToList();
            Instance.callback = type => callback.Invoke((TMotionBuilder)Activator.CreateInstance(type));
            Instance.Redraw();
        }

        protected override void CreateGUI()
        {
            // if (motionsTypeInfo == null)
            // {
            //     TryClose();
            //     return;
            // }

            LoadContent();
            searchBox = Content.Query<TextField>("searchBox").First();
            autoClose = Content.Query<Toggle>("autoClose").First();
            favourites = GetInitFoldout("favourites", FavouritesFoldoutPrefs);
            recent = GetInitFoldout("recent", RecentFoldoutPrefs);
            all = GetInitFoldout("all", AllFoldoutPrefs);
            Bind();
        }

        private void OnLostFocus()
        {
            EditorApplication.delayCall += () => Instance?.Close();
        }

        private FoldoutScrollable GetInitFoldout(string name, PersistentEditorPrefBool editorPrefs)
        {
            FoldoutScrollable foldout = Content.Query<FoldoutScrollable>(name).First();
            foldout.value = editorPrefs.Value;
            foldout.RegisterValueChangedCallback(value => editorPrefs.Value = value.newValue);
            return foldout;
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
                            List<string> favouritePrefs = FavouritesPrefs.Value;

                            if (isSelected)
                            {
                                favouritePrefs.Add(motionTypeInfo.Names.FullName);
                            }
                            else
                            {
                                favouritePrefs.Remove(motionTypeInfo.Names.FullName);
                            }

                            FavouritesPrefs.Value = favouritePrefs;
                            Redraw();
                        }
                    });
            }
        }

        private void AddRecent(MotionTypeInfo motionTypeInfo)
        {
            List<string> recentPrefs = RecentPrefs.Value;
            if (recentPrefs.Contains(motionTypeInfo.Names.FullName))
            {
                recentPrefs.Remove(motionTypeInfo.Names.FullName);
            }

            recentPrefs.Insert(0, motionTypeInfo.Names.FullName);

            if (recentPrefs.Count > 10)
            {
                recentPrefs.RemoveAt(RecentPrefs.Value.Count - 1);
            }

            RecentPrefs.Value = recentPrefs;
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