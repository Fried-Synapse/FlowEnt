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
            Symbol = symbol;
            Toggle = new Toggle(name);
            Add(Toggle);
            if (!string.IsNullOrEmpty(warning))
            {
                Warning = new HelpBox(warning, HelpBoxMessageType.Warning);
                Add(Warning);
            }
            Init();
            Bind();
        }

        private string Symbol { get; }
        private Toggle Toggle { get; }
        private HelpBox Warning { get; }

        private void Init()
        {
            bool isActive = GetSymbols().Contains(Symbol);
            Toggle.SetEnabled(!EditorApplication.isPlayingOrWillChangePlaymode);
            Toggle.SetValueWithoutNotify(isActive);
            Warning?.SetEnabled(isActive);
        }

        private void Bind()
        {
            EditorApplication.playModeStateChanged += playModeStateChange => Toggle.SetEnabled(playModeStateChange == PlayModeStateChange.EnteredEditMode);

            Toggle.RegisterValueChangedCallback(eventData =>
            {
                List<string> symbols = GetSymbols();
                bool isActive = eventData.newValue;
                if (isActive)
                {
                    symbols.Add(Symbol);
                }
                else
                {
                    symbols.Remove(Symbol);
                }

                SetSymbols(symbols);
                Warning?.SetEnabled(isActive);
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
