using System.Threading;
using System.Threading.Tasks;

namespace FriedSynapse.FlowEnt
{
    public interface IControllable
    {
        float TimeScale { get; set; }
        PlayState PlayState { get; }
        void Resume();
        void Pause();
        void ChangeFrame(float modifier);
        void Stop();
    }

    public interface ISeekable
    {
        //NOTE this should not be here, but for the life of me I don't know where to put it...
        public float ElapsedTime { get; }
        public bool IsSeekable { get; }
        public float Ratio { get; set; }
    }

    internal interface IFluentControllable<TType>
    {
        TType Start();
        Task<TType> StartAsync(CancellationToken? token = null);
        TType Resume();
        TType Pause();
        TType Stop(bool triggerOnCompleted = false);
        TType Reset();
    }
}
