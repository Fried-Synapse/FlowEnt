using UnityEngine;

namespace FriedSynapse.FlowEnt.Tests.Unit
{
    public abstract class AbstractEngineTests : AbstractTests<EngineVariables>
    {
        protected readonly Vector3 scale = Vector3.one * 0.9f;
        protected readonly PrimitiveType PrimitiveType = PrimitiveType.Cube;
        protected virtual void PrepareObject(GameObject gameObject) { }
        public override void CreateObjects(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType);
                gameObject.name = $"Test game object {i}";
                gameObject.transform.position = new Vector3(0, 0, i);
                gameObject.transform.localScale = Vector3.one * 0.9f;
                PrepareObject(gameObject);
                GameObjects.Add(gameObject);
            }
        }
    }
}
