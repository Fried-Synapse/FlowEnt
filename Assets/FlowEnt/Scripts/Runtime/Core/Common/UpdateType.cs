using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Enum used to select which type of update the animation should hook into
    /// </summary>
    public enum UpdateType
    {
        /// <summary>
        /// Uses the Unity's Update method with <see cref="Time.deltaTime" /> 
        /// </summary>
        Update,
        /// <summary>
        /// Uses the Unity's Update method with <see cref="Time.smoothDeltaTime" /> 
        /// </summary>
        SmoothUpdate,
        /// <summary>
        /// Uses the Unity's LateUpdate method with <see cref="Time.deltaTime" /> 
        /// </summary>
        LateUpdate,
        /// <summary>
        /// Uses the Unity's LateUpdate method with <see cref="Time.smoothDeltaTime" /> 
        /// </summary>
        SmoothLateUpdate,
        /// <summary>
        /// Uses the Unity's LateUpdate method with <see cref="Time.fixedDeltaTime" /> 
        /// </summary>
        FixedUpdate,
        /// <summary>
        /// Uses the Unity's OnGUI method with <see cref="FlowEntTime.guiDeltaTime" /> 
        /// </summary>
        GuiUpdate,
        /// <summary>
        /// Uses the FlowEnt's custom method. In order to use this you need to invoke <see cref="FlowEntController.CustomUpdate" /> from your custom update loop.
        /// </summary>
        Custom
    }
}
