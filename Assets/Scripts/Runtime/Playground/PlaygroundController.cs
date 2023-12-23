using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FriedSynapse.FlowEnt.Motions.Abstract;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Builder
{
    public class PlaygroundController : MonoBehaviour
    {
        [SerializeField]
        private Transform character;

        private Transform Character => character;

        [SerializeField]
        private Transform green;

        private Transform Green => green;

        [SerializeField]
        private Transform yellow;

        private Transform Yellow => yellow;

        [SerializeField]
        private Transform red;

        private Transform Red => red;

        [SerializeField]
        private TweenBuilder tween;

        private TweenBuilder Tween => tween;

        [SerializeField]
        private EchoBuilder echo;

        private EchoBuilder Echo => echo;

        [SerializeField]
        private FlowBuilder flow;

        private FlowBuilder Flow => flow;

        public AbstractAnimation TweenProp
            => new Tween(1)
                .SetName("tween name")
                .SetTimeScale(1)
                .SetEasing(Easing.EaseInOutCubic)
                .SetLoopCount(3)
                .SetLoopType(LoopType.PingPong)
                .For(Green)
                    .MoveLocalYTo(3.5f);

        public AbstractAnimation EchoProp
            => new Echo(1)
                .For(Green)
                    .MoveY(3.5f);

        public Flow FlowProp
            => new Flow()
                .Queue(
                    new Tween(1)
                        .For(Green)
                            .MoveLocalY(2.5f))
                        .Start();

        private AbstractAnimation GetTween()
            => new Tween(1)
                .SetTimeScale(2)
                .SetLoopCount(3)
                .For(Green)
                    .MoveLocalYTo(3.5f);

        private async void Start()
        {
            await Task.Yield();
            Tween.Build().Start();
        }

        public void ResetMob()
        {
            static void reset(Transform transform, Vector3 position)
            {
                transform.localPosition = position;
            }

            reset(Green, new Vector3(-2f, 0f, 0f));
            reset(Yellow, new Vector3(0f, 0f, 0f));
            reset(Red, new Vector3(2f, 0f, 0f));
        }

        public void AddAllMotions<TMotionBuilder>()
            where TMotionBuilder : IMotionBuilder
        {
            Type type = typeof(TMotionBuilder);
            List<TMotionBuilder> items = new();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                items.AddRange(assembly
                    .GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(type))
                    .Select(t => (TMotionBuilder)Activator.CreateInstance(t)));
            }

            switch (type)
            {
                case not null when type == typeof(AbstractTweenMotionBuilder):
                    Tween.Motions.Items.AddRange(items.Cast<AbstractTweenMotionBuilder>());
                    break;
                case not null when type == typeof(AbstractEchoMotionBuilder):
                    Echo.Motions.Items.AddRange(items.Cast<AbstractEchoMotionBuilder>());
                    break;
            }
        }

        public void Clear()
        {
            Tween.Motions.Items.Clear();
            Echo.Motions.Items.Clear();
        }

        public void Log(float t)
        {
            Debug.Log($"{t}");
        }

        public void Log(Vector2 t)
        {
            Debug.Log($"{t}");
        }

        public void Log(Vector3 t)
        {
            Debug.Log($"{t}");
        }

        public void Log(string name)
        {
            Debug.Log(name);
        }
    }
}