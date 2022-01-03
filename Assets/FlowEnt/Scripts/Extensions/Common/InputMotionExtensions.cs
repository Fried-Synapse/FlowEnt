using System;
using FriedSynapse.FlowEnt.Motions.Echo.Inputs;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public static class InputMotionExtensions
    {
        /// <summary>
        /// Applies a <see cref="KeyDownMotion" /> to the echo.
        /// </summary>
        /// <param name="echo"></param>
        /// <param name="keyCode"></param>
        /// <param name="callback"></param>
        public static Echo KeyDown(this Echo echo, KeyCode keyCode, Action<float> callback)
            => echo.Apply(new KeyDownMotion(keyCode, callback));

        /// <summary>
        /// Applies a <see cref="KeyMotion" /> to the echo.
        /// </summary>
        /// <param name="echo"></param>
        /// <param name="keyCode"></param>
        /// <param name="callback"></param>
        public static Echo Key(this Echo echo, KeyCode keyCode, Action<float> callback)
            => echo.Apply(new KeyMotion(keyCode, callback));

        /// <summary>
        /// Applies a <see cref="KeyUpMotion" /> to the echo.
        /// </summary>
        /// <param name="echo"></param>
        /// <param name="keyCode"></param>
        /// <param name="callback"></param>
        public static Echo KeyUp(this Echo echo, KeyCode keyCode, Action<float> callback)
            => echo.Apply(new KeyUpMotion(keyCode, callback));

        /// <summary>
        /// Applies a <see cref="ButtonDownMotion" /> to the echo.
        /// </summary>
        /// <param name="echo"></param>
        /// <param name="buttonName"></param>
        /// <param name="callback"></param>
        public static Echo ButtonDown(this Echo echo, string buttonName, Action<float> callback)
            => echo.Apply(new ButtonDownMotion(buttonName, callback));

        /// <summary>
        /// Applies a <see cref="ButtonMotion" /> to the echo.
        /// </summary>
        /// <param name="echo"></param>
        /// <param name="buttonName"></param>
        /// <param name="callback"></param>
        public static Echo Button(this Echo echo, string buttonName, Action<float> callback)
            => echo.Apply(new ButtonMotion(buttonName, callback));

        /// <summary>
        /// Applies a <see cref="ButtonUpMotion" /> to the echo.
        /// </summary>
        /// <param name="echo"></param>
        /// <param name="buttonName"></param>
        /// <param name="callback"></param>
        public static Echo ButtonUp(this Echo echo, string buttonName, Action<float> callback)
            => echo.Apply(new ButtonUpMotion(buttonName, callback));
    }
}
