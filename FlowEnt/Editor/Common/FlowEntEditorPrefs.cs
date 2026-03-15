using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace FriedSynapse.FlowEnt.Editor
{
    internal static class FlowEntEditorPrefs
    {
        private const string BaseKey = "FriendSynapse:FlowEnt:";
        internal const string FavouritesFoldoutKey = BaseKey + "FavouritesFoldout";
        internal const string RecentFoldoutKey = BaseKey + "RecentFoldout";
        internal const string AllFoldoutKey = BaseKey + "AllFoldout";
        internal const string FavouritesKey = BaseKey + "Favourites";
        internal const string RecentKey = BaseKey + "Recent";
    }

    internal abstract class AbstractPersistentEditorPrefDatum<T>
    {
        protected AbstractPersistentEditorPrefDatum(string key, T defaultValue = default)
        {
            Key = key;
            DefaultValue = defaultValue;
        }

        protected abstract Func<string, T> Getter { get; }
        protected abstract Action<string, T> Setter { get; }

        protected string Key { get; }
        protected T DefaultValue { get; }
        internal bool HasValue => EditorPrefs.HasKey(Key);

        internal T Value
        {
            get => HasValue ? Getter(Key) : DefaultValue;
            set => Setter(Key, value);
        }
    }

    internal class PersistentEditorPrefString : AbstractPersistentEditorPrefDatum<string>
    {
        internal PersistentEditorPrefString(string key, string defaultValue = default) : base(key, defaultValue)
        {
        }

        protected override Func<string, string> Getter => EditorPrefs.GetString;

        protected override Action<string, string> Setter => EditorPrefs.SetString;
    }

    internal class PersistentEditorPrefListString : AbstractPersistentEditorPrefDatum<List<string>>
    {
        private const char Separator = '|';

        internal PersistentEditorPrefListString(string key, List<string> defaultValue = default) : base(key,
            defaultValue)
        {
        }

        protected override Func<string, List<string>> Getter =>
            key =>
            {
                string value = EditorPrefs.GetString(key);
                return string.IsNullOrEmpty(value) ? DefaultValue : value.Split(Separator).ToList();
            };

        protected override Action<string, List<string>> Setter =>
            (key, value) => EditorPrefs.SetString(key, value == null ? null : string.Join(Separator, value));
    }

    internal class PersistentEditorPrefBool : AbstractPersistentEditorPrefDatum<bool>
    {
        internal PersistentEditorPrefBool(string key, bool defaultValue = default) : base(key, defaultValue)
        {
        }

        protected override Func<string, bool> Getter => EditorPrefs.GetBool;

        protected override Action<string, bool> Setter => EditorPrefs.SetBool;
    }

    internal class PersistentEditorPrefFloat : AbstractPersistentEditorPrefDatum<float>
    {
        internal PersistentEditorPrefFloat(string key, float defaultValue = default) : base(key, defaultValue)
        {
        }

        protected override Func<string, float> Getter => EditorPrefs.GetFloat;

        protected override Action<string, float> Setter => EditorPrefs.SetFloat;
    }

    internal class PersistentEditorPrefInt : AbstractPersistentEditorPrefDatum<int>
    {
        internal PersistentEditorPrefInt(string key, int defaultValue = default) : base(key, defaultValue)
        {
        }

        protected override Func<string, int> Getter => EditorPrefs.GetInt;

        protected override Action<string, int> Setter => EditorPrefs.SetInt;
    }

    internal class PersistentEditorPrefEnum<TEnum> : AbstractPersistentEditorPrefDatum<TEnum>
        where TEnum : Enum
    {
        internal PersistentEditorPrefEnum(string key, TEnum defaultValue = default) : base(key, defaultValue)
        {
        }

        protected override Func<string, TEnum> Getter => (key) => (TEnum)(object)EditorPrefs.GetInt(key);

        protected override Action<string, TEnum> Setter => (key, value) => EditorPrefs.SetInt(key, (int)(object)value);
    }
}