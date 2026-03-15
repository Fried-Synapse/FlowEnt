using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.Quickit.Editor
{
    public static class ProjectTools
    {
        [MenuItem("Tools/Quickit/Project/Delete solution files", false, 200)]
        public static void DeleteSolutionFiles()
        {
            List<string> ext = new List<string> { "csproj", "sln" };

            Directory.GetParent(Application.dataPath)
                .EnumerateFiles("*.*", SearchOption.TopDirectoryOnly)
                .Where(f => ext.Contains(Path.GetExtension(f.FullName).TrimStart('.').ToLowerInvariant()))
                .ForEach(f => f.Delete());
        }
    }
}
