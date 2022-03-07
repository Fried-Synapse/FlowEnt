using System;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Provides common options for behaviours that require frame update.
    /// </summary>
    public abstract class AbstractUpdatable : FastListItem<AbstractUpdatable>
    {
        /// <summary>
        /// Creates a new instance using <see cref="FlowEntController"/>.
        /// </summary>
        protected AbstractUpdatable() : this(FlowEntController.UpdateControllerInstance)
        {
        }

        /// <summary>
        /// Creates a new instance using the specified <see cref="IUpdateController"/>.
        /// </summary>
        /// <param name="updateController"></param>
        protected AbstractUpdatable(IUpdateController updateController)
        {
            Id = lastId;
            ++lastId;
            this.updateController = updateController;
#if FlowEnt_Debug || (UNITY_EDITOR && FlowEnt_Debug_Editor)
            const string flowEntNamespace = "  at FriedSynapse.FlowEnt.";
            const string stackTraceNamespace = "  at System.Environment.get_StackTrace";
            const string flowEntDemoNamespace = "  at FriedSynapse.FlowEnt.Demo";
            string[] lines = Environment.StackTrace.Split('\n');
            System.Collections.Generic.List<string> trimmedLines = new System.Collections.Generic.List<string>();
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line) ||
                    line.StartsWith(stackTraceNamespace) ||
                    (line.StartsWith(flowEntNamespace) && !line.StartsWith(flowEntDemoNamespace)))
                {
                    continue;
                }
                trimmedLines.Add(line);
            }
            stackTrace = string.Join("\n", trimmedLines);
#endif
        }

#if FlowEnt_Debug || (UNITY_EDITOR && FlowEnt_Debug_Editor)
        internal string stackTrace;
#endif

        //HACK for having a constructor that does nothing, to instantiate quicker when needed
#pragma warning disable RCS1163, IDE0060
        private protected AbstractUpdatable(int thisIsAnEmptyConstructorForAnchor)
#pragma warning restore RCS1163, IDE0060
        {
        }

        private static ulong lastId;
        /// <summary>
        /// A value used to identify the current updatable that is automatically assigned.
        /// </summary>
        /// <remarks>
        /// The value is generated and it automatically increments starting from 0.
        /// </remarks>
        public ulong Id { get; }

        /// <summary>
        /// A name that can be used to identify the animation. Empty by default.
        /// </summary>
        public string Name { get; set; }

        internal UpdateType updateType;

        internal IUpdateController updateController;

        #region Events

        private protected Action onStarted;
        private protected Action<float> onUpdated;
        private protected Action onCompleted;

        #endregion

        internal abstract void StartInternal(float deltaTime = 0);
        internal abstract void UpdateInternal(float deltaTime);

        /// <summary>
        /// Stops the animation.
        /// </summary>
        /// <param name="triggerOnCompleted">If set to true will trigger the "OnCompleted" event on the animation</param>
        public virtual void Stop(bool triggerOnCompleted = false)
        {
        }

        public override string ToString()
            => $"[Id: {Id}{(Name == null ? string.Empty : $", Name: \"{Name}\"")}]";
    }

    internal class UpdatableAnchor : AbstractUpdatable
    {
        private const string InvalidImplementation = "This method should not be called.";
        public UpdatableAnchor() : base(0)
        {
        }

        internal override void StartInternal(float deltaTime)
        {
            throw new InvalidOperationException(InvalidImplementation);
        }

        internal override void UpdateInternal(float deltaTime)
        {
            throw new InvalidOperationException(InvalidImplementation);
        }
    }
}
