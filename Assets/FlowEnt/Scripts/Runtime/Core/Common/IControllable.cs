using System.Threading.Tasks;

namespace FriedSynapse.FlowEnt
{
    public interface IControllable
    {
        float TimeScale { get; set; }
        PlayState PlayState { get; }
        void Resume();
        void Pause();
        void Stop(bool triggerOnCompleted = false);
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
