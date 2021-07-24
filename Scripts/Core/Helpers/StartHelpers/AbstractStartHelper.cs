using System;

namespace FlowEnt
{
    internal abstract class AbstractStartHelper : AbstractUpdatable
    {
        protected AbstractStartHelper(Action<float> callback)
        {
            Callback = callback;
        }

        protected Action<float> Callback { get; }
    }
}
