using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class DynamicMaterial : IHasUndoableObjects
    {
        private const string Tooltip =
            "Due to how Unity manages materials, we have 2 options. We either get the material that is used by all renderers or just the current instance." +
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

        [SerializeField, AutoAssignButton]
        private GameObject gameObjectWithInstance;

        [SerializeField, AutoAssignButton]
        private Material predefinedMaterial;

        private Material material;

        public DynamicMaterial()
        {
        }

        public DynamicMaterial(Material predefinedMaterial)
        {
            type = MaterialType.Predefined;
            this.predefinedMaterial = predefinedMaterial;
        }

        public Material Material
        {
            get
            {
                //NOTE We don't want to cache in the previewer.
#if UNITY_EDITOR
                if (!Application.isPlaying || material == null)
#else
                if (material == null)
#endif
                {
                    material = GetMaterial();
                }

                return material;
            }
        }

        private Material GetMaterial() => type switch
        {
            MaterialType.Predefined => predefinedMaterial,
            MaterialType.Instance => gameObjectWithInstance != null
                ? gameObjectWithInstance.TryGetComponent(out Renderer renderer) ? renderer.sharedMaterial : null
                : null,
            _ => null
        };

        List<Object> IHasUndoableObjects.GetUndoableObjects() => new() { Material };
    }

    [Serializable]
    public class DynamicMaterialWithProperty : DynamicMaterial
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


    [Serializable]
    public class DynamicMaterialWithProperty<T> : DynamicMaterialWithProperty
    {
        public DynamicMaterialWithProperty()
        {
        }

        public DynamicMaterialWithProperty(Material material, int propertyId) : base(material, propertyId)
        {
        }
    }
}