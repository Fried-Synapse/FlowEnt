using System.Threading.Tasks;

namespace FriedSynapse.FlowEnt
{
    public interface IControllable
    {
        float TimeScale { get; set; }
        PlayState PlayState { get; }
        void Resume();
        void Pause();
        void NextFrame();
        void Stop();
    }

    internal interface IFluentControllable<TType>
    {
        TType Start();
        Task<TType> StartAsync();
        TType Resume();
        TType Pause();
        TType Stop(bool triggerOnCompleted = false);
        TType Reset();
    }
}
