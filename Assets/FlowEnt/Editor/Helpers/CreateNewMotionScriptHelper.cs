using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public static class CreateNewMotionScriptHelper
    {
        private const string MotionScriptTemplateName = "MotionScriptTemplate.cs";

        [MenuItem(itemName: "Assets/Create/Motion Script %#m", isValidateFunction: false, priority: 82)]
        public static void CreateScriptFromTemplate()
        {
            string[] results = AssetDatabase.FindAssets(MotionScriptTemplateName);
            switch (results.Length)
            {
                case 0:
                    Debug.LogError($"Cannot find file \"{MotionScriptTemplateName}\". Please make sure all the FlowEnt scripts are in the project.");
                    break;
                case 1:
                    ProjectWindowUtil.CreateScriptAssetFromTemplateFile(AssetDatabase.GUIDToAssetPath(results[0]), "NewMotionScript.cs");
                    break;
                default:
                    Debug.LogWarning($"Multiple versions of {MotionScriptTemplateName} were found. The first occurence was used to create the motion script.");
                    goto case 1;
            }
        }
    }
}
