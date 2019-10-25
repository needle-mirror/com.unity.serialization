using Unity.Properties;

namespace Unity.Serialization.Json
{
    class JsonVisitorAdapterUnityEditor : JsonVisitorAdapter
#if UNITY_EDITOR
        , IVisitAdapter<UnityEditor.GUID>
        , IVisitAdapter<UnityEditor.GlobalObjectId>
#endif
    {
        public JsonVisitorAdapterUnityEditor(JsonVisitor visitor) : base(visitor) { }

        public static void RegisterTypes()
        {
#if UNITY_EDITOR
            TypeConversion.Register<SerializedStringView, UnityEditor.GUID>(view => UnityEditor.GUID.TryParse(view.ToString(), out var guid) ? guid : default);
            TypeConversion.Register<UnityEditor.GUID, string>(guid => guid.ToString());

            TypeConversion.Register<SerializedStringView, UnityEditor.GlobalObjectId>(view => UnityEditor.GlobalObjectId.TryParse(view.ToString(), out var id) ? id : default);
            TypeConversion.Register<UnityEditor.GlobalObjectId, string>(id => id.ToString());
#endif
        }

#if UNITY_EDITOR
        public VisitStatus Visit<TProperty, TContainer>(IPropertyVisitor visitor, TProperty property, ref TContainer container, ref UnityEditor.GUID value, ref ChangeTracker changeTracker)
            where TProperty : IProperty<TContainer, UnityEditor.GUID>
        {
            AppendJsonString(property, value);
            return VisitStatus.Handled;
        }

        public VisitStatus Visit<TProperty, TContainer>(IPropertyVisitor visitor, TProperty property, ref TContainer container, ref UnityEditor.GlobalObjectId value, ref ChangeTracker changeTracker)
            where TProperty : IProperty<TContainer, UnityEditor.GlobalObjectId>
        {
            AppendJsonString(property, value);
            return VisitStatus.Override;
        }
#endif
    }
}
