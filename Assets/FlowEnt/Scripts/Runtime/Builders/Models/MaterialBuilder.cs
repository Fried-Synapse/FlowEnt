using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class MaterialBuilder : AbstractBuilder<Material>, IHasUndoableObjects
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

        private Material builtMaterial;

        public MaterialBuilder()
        {
        }

        public MaterialBuilder(Material predefinedMaterial)
        {
            type = MaterialType.Predefined;
            this.predefinedMaterial = predefinedMaterial;
        }

        public Material BuiltMaterial
        {
            get
            {
                if (builtMaterial == null)
                {
                    builtMaterial = Build();
                }

                return builtMaterial;
            }
        }

        public override Material Build() => type switch
        {
            MaterialType.Predefined => predefinedMaterial,
            MaterialType.Instance => gameObjectWithInstance != null
                ? gameObjectWithInstance.TryGetComponent(out Renderer renderer) ? renderer.sharedMaterial : null
                : null,
            _ => null
        };

        List<Object> IHasUndoableObjects.GetUndoableObjects() => new() { Build() };
    }

    [Serializable]
    public class MaterialBuilderWithProperty : MaterialBuilder
    {
        [SerializeField]
        private int propertyId;

        public MaterialBuilderWithProperty()
        {
        }

        public MaterialBuilderWithProperty(Material material, int propertyId) : base(material)
        {
            this.propertyId = propertyId;
        }

        public int PropertyId => propertyId;
    }


    [Serializable]
    public class MaterialBuilderWithProperty<T> : MaterialBuilderWithProperty
    {
        public MaterialBuilderWithProperty()
        {
        }

        public MaterialBuilderWithProperty(Material material, int propertyId) : base(material, propertyId)
        {
        }
    }
}