using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public struct DisplayName
    {
        [SerializeField]
        private string name;

        [SerializeField]
        private bool isEnabled;

        public override string ToString() => name;

        public static implicit operator string(DisplayName displayName) => displayName.ToString();
        public static implicit operator DisplayName(string name) => new() { name = name };
    }
}