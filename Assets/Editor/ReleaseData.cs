using System.IO;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Builder.Editor
{
    public class ReleaseData : ScriptableObject
    {
        private const string FlowEntName = "FlowEnt";
        [SerializeField]
        private string releaseVersion;
        public string ReleaseVersion => releaseVersion;

        [SerializeField]
        private string destination;
        public string Destination => destination;

        public string GetFileName()
        {
            return $"{FlowEntName} {releaseVersion}.unitypackage";
        }

        public string GetFilePath()
        {
            return Path.Combine(Destination, GetFileName());
        }
    }
}