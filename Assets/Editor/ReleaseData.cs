using System.IO;
using UnityEngine;

namespace FriedSynapse.Release
{
    public class ReleaseData : ScriptableObject
    {
        [SerializeField]
        private string releaseVersion;
        public string ReleaseVersion => releaseVersion;

        public string Destination => $"/Users/Shared/Work/FriedSynapse/Builds/{Application.productName}/Packages";

        public string GetFileName()
        {
            return $"{Application.productName} {releaseVersion}.unitypackage";
        }

        public string GetFilePath()
        {
            return Path.Combine(Destination, GetFileName());
        }
    }
}