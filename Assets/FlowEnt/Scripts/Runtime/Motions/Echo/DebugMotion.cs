using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo
{
    /// <summary>
    /// Logs tracing data on certain events using the name provided to help with debugging.
    /// </summary>
    public class DebugMotion : AbstractEchoMotion
    {
        public DebugMotion(string name)
        {
            Name = name;
        }

        private string Name { get; }

        public override void OnStart()
        {
            FlowEntDebug.Log(GetLogData("Start", FlowEntConstants.Blue));
        }

        public override void OnUpdate(float t)
        {
            FlowEntDebug.Log(GetLogData("Update", FlowEntConstants.Green, $"<b><color={FlowEntConstants.Cyan}>with {t}</color></b>"));
        }

        public override void OnLoopComplete()
        {
            FlowEntDebug.Log(GetLogData("LoopComplete", FlowEntConstants.Orange));
        }

        public override void OnComplete()
        {
            FlowEntDebug.Log(GetLogData("Complete", FlowEntConstants.Red));
        }

        private string GetLogData(string eventName, string colour, string extra = "")
        {
            return $"Motion: <b>{Name,-20}</b> <b><color={colour}>On {eventName,-20}</color></b> {extra}" +
                $"\n\t\t<b><color={FlowEntConstants.Grey}>at {Time.time,-20}</color></b> with <b><color={FlowEntConstants.Grey}>delta {Time.deltaTime,-20}</color></b>";
        }
    }
}