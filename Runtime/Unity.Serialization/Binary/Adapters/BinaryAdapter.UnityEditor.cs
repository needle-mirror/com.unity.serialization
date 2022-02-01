#if UNITY_EDITOR
using Unity.Collections.LowLevel.Unsafe;
using Unity.Collections.LowLevel.Unsafe.NotBurstCompatible;

namespace Unity.Serialization.Binary.Adapters
{
    unsafe partial class BinaryAdapter : IBinaryAdapter
        , IBinaryAdapter<UnityEditor.GUID>
        , IBinaryAdapter<UnityEditor.GlobalObjectId>
    {
        void IBinaryAdapter<UnityEditor.GUID>.Serialize(UnsafeAppendBuffer* writer, UnityEditor.GUID value)
        {
            writer->AddNBC(value.ToString());
        }

        UnityEditor.GUID IBinaryAdapter<UnityEditor.GUID>.Deserialize(UnsafeAppendBuffer.Reader* reader)
        {
            reader->ReadNextNBC(out string str);
            return UnityEditor.GUID.TryParse(str, out var value) ? value : default;
        }

        void IBinaryAdapter<UnityEditor.GlobalObjectId>.Serialize(UnsafeAppendBuffer* writer, UnityEditor.GlobalObjectId value)
        {
            writer->AddNBC(value.ToString());
        }
        
        UnityEditor.GlobalObjectId IBinaryAdapter<UnityEditor.GlobalObjectId>.Deserialize(UnsafeAppendBuffer.Reader* reader)
        {
            reader->ReadNextNBC(out string str);
            return UnityEditor.GlobalObjectId.TryParse(str, out var value) ? value : default;
        }
    }
}
#endif