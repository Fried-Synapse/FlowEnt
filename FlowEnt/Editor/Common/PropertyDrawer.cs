using System.Collections.Generic;
using UnityEditor;

namespace FriedSynapse.FlowEnt.Editor
{
    public class PropertyDrawer<TData> : PropertyDrawer
        where TData : new()
    {
        private readonly Dictionary<string, TData> data = new Dictionary<string, TData>();

        protected TData GetData(string propertyPath)
        {
            if (data.TryGetValue(propertyPath, out TData datum))
            {
                return datum;
            }
            datum = new TData();
            data.Add(propertyPath, datum);
            return datum;
        }

        protected TData GetData(SerializedProperty property)
            => GetData(property.propertyPath);
    }
}
