using UnityEditor;

namespace FriedSynapse.FlowEnt
{
    public class CreateNewMotionScriptHelper
    {
        private const string motionScriptTemplatePath = "Assets/FlowEnt/Scripts/Editor/MotionScriptTemplate.cs.txt";

        [MenuItem(itemName: "Assets/Create/Motion Script %#m", isValidateFunction: false, priority: 82)]
        public static void CreateScriptFromTemplate()
        {
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(motionScriptTemplatePath, "NewMotionScript.cs");
        }
    }
}
