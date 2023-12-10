using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public struct SerializableNullable<T>
        where T : struct
    {
        private SerializableNullable(T value)
        {
            this.value = value;
            this.hasValue = true;
        }
        
        private SerializableNullable(T value, bool hasValue)
        {
            this.value = value;
            this.hasValue = hasValue;
        }

        [SerializeField]
        private T value;

        [SerializeField]
        private bool hasValue;

        public T Value
        {
            get
            {
                if (!HasValue)
                {
                    throw new InvalidOperationException("Serializable nullable object must have a value.");
                }

                return value;
            }
        }

        public bool HasValue => hasValue;

        public static implicit operator SerializableNullable<T>(T value) => new(value);

        public static implicit operator SerializableNullable<T>(T? value)
            => value.HasValue ? new SerializableNullable<T>(value.Value) : new SerializableNullable<T>();

        public static implicit operator T?(SerializableNullable<T> value) => value.HasValue ? value.Value : null;

        public static implicit operator T(SerializableNullable<T> value) => value.Value;
    }
}