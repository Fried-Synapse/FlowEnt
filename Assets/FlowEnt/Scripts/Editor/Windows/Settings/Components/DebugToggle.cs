using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class FeatureToggle : VisualElement
    {
        internal FeatureToggle(string name, string symbol, string tooltip = null, string warning = null)
        {
            this.symbol = symbol;
            toggle = new Toggle(name);
            toggle.tooltip = tooltip;
            Add(toggle);
            if (!string.IsNullOrEmpty(warning))
            {
                Add(new HelpBox(warning, HelpBoxMessageType.Warning));
            }

            Init();
            Bind();
        }

        private readonly string symbol;
        private readonly Toggle toggle;

        internal bool Value => toggle.value;

        private void Init()
        {
            bool isActive = GetSymbols(EditorUserBuildSettings.selectedBuildTargetGroup).Contains(symbol);
            toggle.SetEnabled(!EditorApplication.isPlayingOrWillChangePlaymode);
            toggle.SetValueWithoutNotify(isActive);
        }

        private void Bind()
        {
            EditorApplication.playModeStateChanged += playModeStateChange =>
                toggle.SetEnabled(playModeStateChange == PlayModeStateChange.EnteredEditMode);

            toggle.RegisterValueChangedCallback(eventData =>
            {
                bool isActive = eventData.newValue;
                foreach (BuildTargetGroup target in Enum.GetValues(typeof(BuildTargetGroup)))
                {
                    TryUpdateSymbols(target, isActive);
                }
            });
        }

        private void TryUpdateSymbols(BuildTargetGroup targetGroup, bool isActive)
        {
            try
            {
                List<string> symbols = GetSymbols(targetGroup);
                if (isActive)
                {
                    symbols.Add(symbol);
                }
                else
                {
                    symbols.Remove(symbol);
                }

                SetSymbols(targetGroup, symbols);
            }
            catch
            {
                //HACK there is no way to check if we can change of not, so we try and see...
            }
        }

        private static List<string> GetSymbols(BuildTargetGroup targetGroup)
        {
            PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup, out string[] symbols);
            return symbols.ToList();
        }

        private static void SetSymbols(BuildTargetGroup targetGroup, List<string> symbols)
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, symbols.ToArray());
        }
    }
}