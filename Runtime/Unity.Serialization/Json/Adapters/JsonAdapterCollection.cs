using System.Collections.Generic;
using Unity.Serialization.Json.Unsafe;

namespace Unity.Serialization.Json.Adapters
{
    struct JsonAdapterCollection
    {
        public JsonAdapter InternalAdapter;
        public List<IJsonAdapter> Global;
        public List<IJsonAdapter> UserDefined;
        
        public bool TrySerialize<TValue>(JsonStringBuffer writer, ref TValue value)
        {
            if (null != UserDefined && UserDefined.Count > 0)
            {
                foreach (var adapter in UserDefined)
                {
                    if (TrySerializeAdapter(adapter, writer, ref value))
                    {
                        return true;
                    }
                }
            }
            
            if (null != Global && Global.Count > 0)
            {
                foreach (var adapter in Global)
                {
                    if (TrySerializeAdapter(adapter, writer, ref value))
                    {
                        return true;
                    }
                }
            }
            
            return TrySerializeAdapter(InternalAdapter, writer, ref value);
        }

        static bool TrySerializeAdapter<TValue>(IJsonAdapter adapter, JsonStringBuffer writer, ref TValue value)
        {
            if (adapter is IJsonAdapter<TValue> typed)
            {
                typed.Serialize(writer, value);
                return true;
            }
                    
            if (adapter is Adapters.Contravariant.IJsonAdapter<TValue> typedContravariant)
            {
                typedContravariant.Serialize(writer, value);
                return true;
            }

            return false;
        }
        
        public bool TryDeserialize<TValue>(UnsafeValueView view, ref TValue value)
        {   
            if (null != UserDefined && UserDefined.Count > 0)
            {
                foreach (var adapter in UserDefined)
                {
                    if (TryDeserializeAdapter(adapter, view, ref value))
                    {
                        return true;
                    }
                }
            }
            
            if (null != Global && Global.Count > 0)
            {
                foreach (var adapter in Global)
                {
                    if (TryDeserializeAdapter(adapter, view, ref value))
                    {
                        return true;
                    }
                }
            }
            
            return TryDeserializeAdapter(InternalAdapter, view, ref value);
        }

        static bool TryDeserializeAdapter<TValue>(IJsonAdapter adapter, UnsafeValueView view, ref TValue value)
        {
            if (adapter is IJsonAdapter<TValue> typed)
            {
                value = typed.Deserialize(view.AsSafe());
                return true;
            }
                    
            if (adapter is Adapters.Contravariant.IJsonAdapter<TValue> typedContravariant)
            {
                // @TODO Type checking on return value.
                value = (TValue) typedContravariant.Deserialize(view.AsSafe());
                return true;
            }

            return false;
        }
    }
}