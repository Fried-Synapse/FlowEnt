using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public static class HideFlagsUtility
    {
        [MenuItem("Tools/Quickit/Editor/Hide Flags/Show All Objects", false, 100)]
        [MenuItem("Help/Hide Flags/Show All Objects")]
        private static void ShowAll()
        {
            var allGameObjects = Object.FindObjectsOfType<GameObject>();
            foreach (var go in allGameObjects)
            {
                switch (go.hideFlags)
                {
                    case HideFlags.HideAndDontSave:
                        go.hideFlags = HideFlags.DontSave;
                        break;
                    case HideFlags.HideInHierarchy:
                    case HideFlags.HideInInspector:
                        go.hideFlags = HideFlags.None;
                        break;
                }
            }
        }
    }
}