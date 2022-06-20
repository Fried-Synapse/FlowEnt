namespace FriedSynapse.FlowEnt
{
    public interface IManuallyUpdatable
    {
        public float ElapsedTime { get; }
        public float TotalTime { get; }
        public float Ratio { get; set; }
    }
}
