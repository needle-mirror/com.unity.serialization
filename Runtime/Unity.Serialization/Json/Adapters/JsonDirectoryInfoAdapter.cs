using System.IO;
using Unity.Properties;

namespace Unity.Serialization.Json
{
    class JsonDirectoryInfoAdapter : JsonVisitorAdapter,
        IVisitAdapter<DirectoryInfo>
    {
        public JsonDirectoryInfoAdapter(JsonVisitor visitor) : base(visitor) { }

        public VisitStatus Visit<TProperty, TContainer>(IPropertyVisitor visitor, TProperty property, ref TContainer container, ref DirectoryInfo value, ref ChangeTracker changeTracker)
            where TProperty : IProperty<TContainer, DirectoryInfo>
        {
            Append(property, value, (builder, v) => { builder.Append(EncodeJsonString(v.GetRelativePath())); });
            return VisitStatus.Handled;
        }
    }
}
