using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class DynamicMaterial
    {
        private const string Tooltip = "Due to how Unity manages materials, we have 2 options. We either get the material that is used by all renderers or just the current instance." +
                                       "\n*Instance* - uses the instance of the material, which is assigner at runtime, and therefore doesn't change the other objects using it (Renderer.sharedMaterial)" +
                                       "\n*Predefined* - changes the defined material. Will affect all objects that use this material.";
        
        public enum MaterialType
        {
            Instance,
            Predefined,
        }

        [SerializeField]
        [Tooltip(Tooltip)]
        private MaterialType type;

        [SerializeField]
        private GameObject gameObject;

        [SerializeField]
        private Material material;

        private Material instancedMaterial;

        public DynamicMaterial()
        {
        }

        public DynamicMaterial(Material material)
        {
            this.material = material;
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

        public static implicit operator DynamicMaterial(Material value) => new(value);

        public static implicit operator Material(DynamicMaterial value) => value.Material;

        private Material GetMaterial() => type switch
        {
            MaterialType.Predefined => material,
            MaterialType.Instance => gameObject != null ? gameObject.GetComponent<Renderer>()?.sharedMaterial : null,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    [Serializable]
    public class DynamicMaterialWithProperty<T> : DynamicMaterial
    {
        [SerializeField]
        private int propertyId;

        public DynamicMaterialWithProperty()
        {
        }

        public DynamicMaterialWithProperty(Material material, int propertyId) : base(material)
        {
            this.propertyId = propertyId;
        }

        public int PropertyId => propertyId;
    }
}