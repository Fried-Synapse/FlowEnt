using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FriedSynapse.FlowEnt
{
    public static class JsonSettings
    {
        private static JsonSerializerSettings fullyTyped;
        public static JsonSerializerSettings FullyTyped => fullyTyped ??= new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            ContractResolver = new DefaultContractResolver(),
        };
    }
}
