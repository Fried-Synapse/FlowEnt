using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace FriedSynapse.FlowEnt
{
    public static class FlowEntDebug
    {
        private const string FlowEntName = "FriedSynapse.FlowEnt";
        private const string FlowEntEditorName = "FriedSynapse.FlowEnt.Editor";
        private static string ProjectRoot { get; }

        static FlowEntDebug()
        {
            ProjectRoot = Directory.GetParent(Application.dataPath)?.FullName ?? "";
        }

        public static void Log(object message)
            => Debug.Log($"<b><color={FlowEntInternalConstants.FlowEnt}>[FlowEnt]</color></b> {message}");

        public static void LogWarning(object message)
            => Debug.LogWarning($"<b><color={FlowEntInternalConstants.FlowEnt}>[FlowEnt]</color></b> {message}");

        public static void LogError(object message)
            => Debug.LogError($"<b><color={FlowEntInternalConstants.FlowEnt}>[FlowEnt]</color></b> {message}");

#if FlowEnt_Debug || (UNITY_EDITOR && FlowEnt_Debug_Editor)
        internal static void LogException(AbstractUpdatable abstractUpdatable, Exception exception)
        {
            string info = string.Empty;
            if (!string.IsNullOrEmpty(abstractUpdatable.hierarchy))
            {
                info = $"<color={FlowEntInternalConstants.Orange}><b>Hierarchy</b>:</color> {abstractUpdatable.hierarchy}\n\n";
            }

            if (!string.IsNullOrEmpty(abstractUpdatable.stackTrace))
            {
                info = $"{info}<color={FlowEntInternalConstants.Orange}><b>Stack Trace</b>:</color>\n{abstractUpdatable.stackTrace}\n\n";
            }

            LogError(
                $"<color={FlowEntInternalConstants.Red}><b>Update Exception</b></color> on Animation <b>{abstractUpdatable}</b>\n" +
                $"{info}" +
                $"<color={FlowEntInternalConstants.Orange}><b>Exception</b></color>:\n{exception}");
        }
#endif

        internal static string GetStackTrace()
        {
            StackTrace stackTrace = new(true);
            IEnumerable<string> frames = stackTrace.GetFrames()
                .Where(f => validateNameSpace(f.GetMethod().DeclaringType.Assembly.GetName()))
                .Select(getLine);

            return string.Join("\n", frames).Trim();

            static bool validateNameSpace(AssemblyName assemblyName)
                => assemblyName.Name != FlowEntName && assemblyName.Name != FlowEntEditorName;

            static string getLine(StackFrame frame)
            {
                string fileName = frame.GetFileName();
                if (fileName == null || !fileName.Contains(ProjectRoot))
                {
                    return frame.GetMethod().ToString();
                }

                fileName = Path.GetRelativePath(ProjectRoot, fileName);
                int fileLineNumber = frame.GetFileLineNumber();
                MethodBase methodInfo = frame.GetMethod();
                return $"{methodInfo.DeclaringType.FullName}.{methodInfo.Name} (at <a href=\"{fileName}\" line=\"{fileLineNumber}\">{fileName}:{fileLineNumber}</a>";
            }
        }
    }
}