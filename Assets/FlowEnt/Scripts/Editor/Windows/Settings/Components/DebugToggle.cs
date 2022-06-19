using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class DebugToggle : VisualElement
    {
        public DebugToggle(string name, string symbol, string warning = null)
        {
            this.symbol = symbol;
            toggle = new Toggle(name);
            toggle.tooltip = "Compilation symbols are platform specific.";
            Add(toggle);
            if (!string.IsNullOrEmpty(warning))
            {
                this.warning = new HelpBox(warning, HelpBoxMessageType.Warning);
                Add(this.warning);
            }
            Init();
            Bind();
        }

        private readonly string symbol;
        private readonly Toggle toggle;
        private readonly HelpBox warning;

        private void Init()
        {
            bool isActive = GetSymbols().Contains(symbol);
            toggle.SetEnabled(!EditorApplication.isPlayingOrWillChangePlaymode);
            toggle.SetValueWithoutNotify(isActive);
            warning?.SetEnabled(isActive);
        }

        private void Bind()
        {
            EditorApplication.playModeStateChanged += playModeStateChange => toggle.SetEnabled(playModeStateChange == PlayModeStateChange.EnteredEditMode);

            toggle.RegisterValueChangedCallback(eventData =>
            {
                List<string> symbols = GetSymbols();
                bool isActive = eventData.newValue;
                if (isActive)
                {
                    symbols.Add(symbol);
                }
                else
                {
                    symbols.Remove(symbol);
                }

                SetSymbols(symbols);
                warning?.SetEnabled(isActive);
            });
        }

        private static List<string> GetSymbols()
        {
            PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, out string[] symbols);
            return symbols.ToList();
        }

        private static void SetSymbols(List<string> symbols)
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, symbols.ToArray());
        }
    }
}
