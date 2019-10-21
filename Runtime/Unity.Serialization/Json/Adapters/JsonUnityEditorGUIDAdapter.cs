using Unity.Properties;

namespace Unity.Serialization.Json
{
#if UNITY_EDITOR
    class JsonUnityEditorGUIDAdapter : JsonVisitorAdapter,
        IVisitAdapter<UnityEditor.GUID>
    {
        public JsonUnityEditorGUIDAdapter(JsonVisitor visitor) : base(visitor) { }

        public VisitStatus Visit<TProperty, TContainer>(IPropertyVisitor visitor, TProperty property, ref TContainer container, ref UnityEditor.GUID value, ref ChangeTracker changeTracker)
            where TProperty : IProperty<TContainer, UnityEditor.GUID>
        {
            Append(property, value, (builder, v) => { builder.Append(EncodeJsonString(v.ToString())); });
            return VisitStatus.Handled;
        }
    }
#endif
}
