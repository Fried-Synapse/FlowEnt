using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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
            try
            {
                //await Task.Yield();
                var x = Flow.Build().Start();
                new Tween(1).OnCompleted(() => Destroy(Green.gameObject)).Start();
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
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

        public void Log(float value)
        {
            Debug.Log(value);
        }

        public void Log(Vector2 value)
        {
            Debug.Log(value);
        }

        public void Log(Vector3 value)
        {
            Debug.Log(value);
        }

        public void Log(string value)
        {
            Debug.Log(value);
        }
    }
}