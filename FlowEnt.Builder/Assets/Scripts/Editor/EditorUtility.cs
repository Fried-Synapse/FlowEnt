using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Builder.Editor
{
    public static class EditorUtility
    {
        [MenuItem("Fried Synapse/Show All Hidden Objects")]
        public static void ShowHiddenObjects()
        {
            foreach (GameObject gameObject in Object.FindObjectsOfType<GameObject>())
            {
                switch (gameObject.hideFlags)
                {
                    case HideFlags.HideAndDontSave:
                        gameObject.hideFlags = HideFlags.DontSave;
                        break;
                    case HideFlags.HideInHierarchy:
                    case HideFlags.HideInInspector:
                        gameObject.hideFlags = HideFlags.None;
                        break;
                }
            }
        }
    }
}
