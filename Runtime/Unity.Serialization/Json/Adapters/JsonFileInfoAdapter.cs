using System.IO;
using Unity.Properties;

namespace Unity.Serialization.Json
{
    class JsonFileInfoAdapter : JsonVisitorAdapter,
        IVisitAdapter<FileInfo>
    {
        public JsonFileInfoAdapter(JsonVisitor visitor) : base(visitor) { }

        public VisitStatus Visit<TProperty, TContainer>(IPropertyVisitor visitor, TProperty property, ref TContainer container, ref FileInfo value, ref ChangeTracker changeTracker)
            where TProperty : IProperty<TContainer, FileInfo>
        {
            Append(property, value, (builder, v) => { builder.Append(EncodeJsonString(v.GetRelativePath())); });
            return VisitStatus.Handled;
        }
    }
}
