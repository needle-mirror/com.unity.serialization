using System.Collections.Generic;
using Unity.Properties;

namespace Unity.Serialization.Json
{
    /// <summary>
    /// The default object output by <see cref="JsonSerialization"/> if an object type can not be resolved.
    /// </summary>
    public class JsonObject : Dictionary<string, object>
    {
        static JsonObject()
        {
            PropertyBag.Register(new KeyValueCollectionPropertyBag<JsonObject, string, object>());
        }
    }

    /// <summary>
    /// The default object output by <see cref="JsonSerialization"/> if an array type can not be resolved.
    /// </summary>
    public class JsonArray : List<object>
    {
        static JsonArray()
        {
            PropertyBag.Register(new IndexedCollectionPropertyBag<JsonArray, object>());
        }
    }
}