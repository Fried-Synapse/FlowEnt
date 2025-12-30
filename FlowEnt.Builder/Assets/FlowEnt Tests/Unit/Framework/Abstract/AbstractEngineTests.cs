using UnityEngine;

namespace FriedSynapse.FlowEnt.Tests.Unit
{
    public abstract class AbstractEngineTests : AbstractTests<EngineVariables>
    {
        protected virtual GameObject CreateObject()
            => GameObject.CreatePrimitive(PrimitiveType.Cube);

        public override void CreateObjects(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject gameObject = CreateObject();
                gameObject.name = $"Test game object {i}";
                gameObject.transform.position = new Vector3(0, 0, i);
                gameObject.transform.localScale = Vector3.one * 0.9f;
                GameObjects.Add(gameObject);
            }
        }
    }

    public abstract class AbstractEngineTests<TComponent> : AbstractEngineTests
        where TComponent : Component
    {
        private TComponent component;
        protected virtual TComponent Component => component ??= GameObject.AddComponent<TComponent>();

        protected override void OnTeardown()
        {
            base.OnTeardown();
            component = null;
        }
    }
}
