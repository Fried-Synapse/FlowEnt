using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt
{
    public abstract class AbstractComponent : VisualElement
    {
        protected AbstractComponent()
        {
            if (HasUxml)
            {
                this.LoadUxml();
            }
            if (HasUss)
            {
                this.LoadUss();
            }
        }

        protected virtual bool HasUxml => false;
        protected virtual bool HasUss => false;
    }
}
