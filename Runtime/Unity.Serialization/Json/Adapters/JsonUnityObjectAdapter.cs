using Unity.Properties;

namespace Unity.Serialization.Json
{
    internal class JsonUnityObjectAdapter : JsonVisitorAdapter
        , IVisitAdapter<UnityEngine.Object>
        , IVisitContainerAdapter
    {
        public JsonUnityObjectAdapter(JsonVisitor visitor) : base(visitor) { }

        public VisitStatus Visit<TProperty, TContainer>(IPropertyVisitor visitor, TProperty property, ref TContainer container, ref UnityEngine.Object value, ref ChangeTracker changeTracker)
            where TProperty : IProperty<TContainer, UnityEngine.Object>
        {
#if UNITY_EDITOR
            var str = EncodeJsonString(UnityEditor.GlobalObjectId.GetGlobalObjectIdSlow(value).ToString());
            Append(property, str, (builder, s) => { builder.Append(s); });
#endif
            return VisitStatus.Override;
        }

        public VisitStatus BeginContainer<TProperty, TValue, TContainer>(IPropertyVisitor visitor, TProperty property,
            ref TContainer container, ref TValue value, ref ChangeTracker changeTracker) where TProperty : IProperty<TContainer, TValue>
        {
            if (!typeof(UnityEngine.Object).IsAssignableFrom(typeof(TValue)))
            {
                return VisitStatus.Unhandled;
            }
#if UNITY_EDITOR
            var obj = value as UnityEngine.Object;
            var str = EncodeJsonString(UnityEditor.GlobalObjectId.GetGlobalObjectIdSlow(obj).ToString());
            Append(property, str, (builder, s) => { builder.Append(s); });
#endif
            return VisitStatus.Override;
        }

        public void EndContainer<TProperty, TValue, TContainer>(IPropertyVisitor visitor, TProperty property, ref TContainer container,
            ref TValue value, ref ChangeTracker changeTracker) where TProperty : IProperty<TContainer, TValue>
        {
        }
    }
}
