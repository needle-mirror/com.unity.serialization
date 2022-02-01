#if !NET_DOTS
using System.IO;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Collections.LowLevel.Unsafe.NotBurstCompatible;

namespace Unity.Serialization.Binary.Adapters
{
    unsafe partial class BinaryAdapter :
        IBinaryAdapter<DirectoryInfo>,
        IBinaryAdapter<FileInfo>
    {
        void IBinaryAdapter<DirectoryInfo>.Serialize(UnsafeAppendBuffer* writer, DirectoryInfo value)
        {
            if (null == value) 
                writer->AddNBC("null");
            else 
                writer->AddNBC(value.GetRelativePath());
        }

        DirectoryInfo IBinaryAdapter<DirectoryInfo>.Deserialize(UnsafeAppendBuffer.Reader* reader)
        {
            reader->ReadNextNBC(out string str);
            return str.Equals("null") ? null : new DirectoryInfo(str);
        }

        void IBinaryAdapter<FileInfo>.Serialize(UnsafeAppendBuffer* writer, FileInfo value)
        {
            if (null == value) 
                writer->AddNBC("null");
            else 
                writer->AddNBC(value.GetRelativePath());
        }

        FileInfo IBinaryAdapter<FileInfo>.Deserialize(UnsafeAppendBuffer.Reader* reader)
        {
            reader->ReadNextNBC(out string str);
            return str.Equals("null") ? null : new FileInfo(str);
        }
    }
}
#endif