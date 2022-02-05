using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public static class CreateNewMotionScriptHelper
    {
        private const string TweenMotionTemplateName = "TweenMotionScriptTemplate.cs";
        private const string EchoMotionTemplateName = "EchoMotionScriptTemplate.cs";

        [MenuItem(itemName: "Assets/Create/FlowEnt/Tween Motion Script %#t", isValidateFunction: false, priority: 82)]
        public static void CreateTweenMotionFromTemplate()
            => CreateScriptFromTemplate(TweenMotionTemplateName);

        [MenuItem(itemName: "Assets/Create/FlowEnt/Echo Motion Script %#e", isValidateFunction: false, priority: 83)]
        public static void CreateEchoMotionFromTemplate()
            => CreateScriptFromTemplate(EchoMotionTemplateName);

        private static void CreateScriptFromTemplate(string templateName)
        {
            string[] results = AssetDatabase.FindAssets(templateName);
            switch (results.Length)
            {
                case 0:
                    Debug.LogError($"Cannot find file \"{templateName}\". Please make sure all the FlowEnt scripts are in the project.");
                    break;
                case 1:
                    ProjectWindowUtil.CreateScriptAssetFromTemplateFile(AssetDatabase.GUIDToAssetPath(results[0]), "NewMotionScript.cs");
                    break;
                default:
                    Debug.LogWarning($"Multiple versions of {templateName} were found. The first occurrence was used to create the motion script.");
                    goto case 1;
            }
        }
    }
}
