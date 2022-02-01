#if !UNITY_DOTSRUNTIME
using Unity.Collections.LowLevel.Unsafe;
using Unity.Collections.LowLevel.Unsafe.NotBurstCompatible;
using UnityObject = UnityEngine.Object;

namespace Unity.Serialization.Binary.Adapters
{
    unsafe partial class BinaryAdapter :
        Contravariant.IBinaryAdapter<UnityObject>
    {
        void Contravariant.IBinaryAdapter<UnityObject>.Serialize(UnsafeAppendBuffer* writer, UnityObject value)
        {
#if UNITY_EDITOR
            var id = UnityEditor.GlobalObjectId.GetGlobalObjectIdSlow(value).ToString();
            writer->AddNBC(id);
#endif
        }

        object Contravariant.IBinaryAdapter<UnityObject>.Deserialize(UnsafeAppendBuffer.Reader* reader)
        {
#if UNITY_EDITOR
            reader->ReadNextNBC(out string value);
            
            if (UnityEditor.GlobalObjectId.TryParse(value, out var id))
            {
                return UnityEditor.GlobalObjectId.GlobalObjectIdentifierToObjectSlow(id);
            }
#endif
            return null;
        }
    }
}
#endif