using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class MaterialWithProperty<T> : AbstractMaterialWithProperty
    {
        public MaterialWithProperty()
        {
        }

        public MaterialWithProperty(Material material, int propertyId) : base(material, propertyId)
        {
        }
    }

    [Serializable]
    public abstract class AbstractMaterialWithProperty
    {
        public enum MaterialType
        {
            Instance,
            Predefined,
        }

        [SerializeField]
        private MaterialType type;

        [SerializeField]
        private GameObject gameObject;

        [SerializeField]
        private Material material;

        [SerializeField]
        private int propertyId;

        private Material instancedMaterial;

        protected AbstractMaterialWithProperty()
        {
        }

        protected AbstractMaterialWithProperty(Material material, int propertyId) : this()
        {
            this.material = material;
            this.propertyId = propertyId;
        }

        public Material Material
        {
            get
            {
                if (instancedMaterial == null)
                {
                    instancedMaterial = GetMaterial();
                }

                return instancedMaterial;
            }
        }

        public int PropertyId => propertyId;

        private Material GetMaterial() => type switch
        {
            MaterialType.Predefined => material,
            MaterialType.Instance => gameObject != null ? gameObject.GetComponent<Renderer>()?.sharedMaterial : null,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}