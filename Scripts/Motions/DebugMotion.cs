using UnityEngine;

namespace FlowEnt.Motions
{
    public class DebugMotion : AbstractMotion
    {
        public DebugMotion(string name)
        {
            Name = name;
        }

        private string Name { get; }

        public override void OnStart()
        {
            Debug.Log(GetLogData("Start", "4871F3"));
        }

        public override void OnUpdate(float t)
        {
            Debug.Log(GetLogData("Update", "48F352", $"<b><color=#48ECF3>with {t}</color></b>"));
        }

        public override void OnLoopComplete()
        {
            Debug.Log(GetLogData("LoopComplete", "F3C648"));
        }

        public override void OnComplete()
        {
            Debug.Log(GetLogData("Complete", "F39E48"));
        }

        private string GetLogData(string eventName, string colour, string extra = "")
        {
            return $"<b>{Name,-20}</b> <b><color=#{colour}>On {eventName,-20}</color></b> {extra}" +
                $"\n\t\t<b><color=#DEDEDE>at {Time.time,-20}</color></b> with <b><color=#DEDEDE>delta {Time.deltaTime,-20}</color></b>";
        }
    }
}