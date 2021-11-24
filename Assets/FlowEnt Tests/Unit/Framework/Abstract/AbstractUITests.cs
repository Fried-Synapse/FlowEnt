using UnityEngine;
using UnityEngine.UI;

namespace FriedSynapse.FlowEnt.Tests.Unit
{
    public abstract class AbstractUITest : AbstractTests<UIVariables>
    {
        protected override string SceneName => "UnitTestsUIScene";

        public RectTransform RectTransform => (RectTransform)GameObject.transform;

        protected virtual void PrepareObject(RectTransform rectTransform) { }

        public override void CreateObjects(int count)
        {
            Canvas canvas = Object.FindObjectOfType<Canvas>();
            for (int i = 0; i < count; i++)
            {
                GameObject gameObject = new GameObject();
                gameObject.name = $"Test UI game object {i}";
                gameObject.transform.SetParent(canvas.transform);
                RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
                rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                rectTransform.pivot = new Vector2(0.5f, 0.5f);
                rectTransform.sizeDelta = new Vector2(100f, 100f);
                rectTransform.anchoredPosition = new Vector2(0f, 0f);
                gameObject.AddComponent<CanvasRenderer>();
                gameObject.AddComponent<Image>();
                PrepareObject(rectTransform);
                GameObjects.Add(gameObject);
            }
        }
    }
}
