using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween
{
    /// <summary>
    /// Logs tracing data on certain events using the name provided to help with debugging.
    /// </summary>
    public class DebugMotion : AbstractTweenMotion
    {
        [Serializable]
        public class Builder : AbstractTweenMotionBuilder
        {
            [SerializeField]
            private string name;
            public override AbstractTweenMotion Build()
                => new DebugMotion(name);
        }

        public DebugMotion(string name)
        {
            Name = name;
        }

        private string Name { get; }

        public override void OnStart()
        {
            FlowEntDebug.Log(GetLogData("Start", FlowEntInternalConstants.Blue));
        }

        public override void OnUpdate(float t)
        {
            FlowEntDebug.Log(GetLogData("Update", FlowEntInternalConstants.Green, $"<b><color={FlowEntInternalConstants.Cyan}>with {t}</color></b>"));
        }

        public override void OnLoopComplete()
        {
            FlowEntDebug.Log(GetLogData("LoopComplete", FlowEntInternalConstants.Orange));
        }

        public override void OnComplete()
        {
            FlowEntDebug.Log(GetLogData("Complete", FlowEntInternalConstants.Red));
        }

        private string GetLogData(string eventName, string colour, string extra = "")
        {
            return $"Motion: <b>{Name,-20}</b> <b><color={colour}>On {eventName,-20}</color></b> {extra}" +
                $"\n\t\t<b><color={FlowEntInternalConstants.Grey}>at {Time.time,-20}</color></b> with <b><color={FlowEntInternalConstants.Grey}>delta {Time.deltaTime,-20}</color></b>";
        }
    }
}