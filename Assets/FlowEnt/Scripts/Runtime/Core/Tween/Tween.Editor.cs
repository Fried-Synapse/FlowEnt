using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
#if UNITY_EDITOR

    public partial class Tween
    {
        private bool isEditorInitialised;

        public void SetT(float t)
        {
            if (Application.isPlaying)
            {
                throw new InvalidOperationException("SetT cannot be called in playmode.");
            }

            if (!isEditorInitialised)
            {
                isEditorInitialised = true;
                for (int i = 0; i < motions.Length; i++)
                {
                    motions[i].OnStart();
                }
            }

            t = easing.GetValue(t);

            Debug.Log($"{t}");
            for (int i = 0; i < motions.Length; i++)
            {
                motions[i].OnUpdate(t);
            }
        }
    }

#endif
}
