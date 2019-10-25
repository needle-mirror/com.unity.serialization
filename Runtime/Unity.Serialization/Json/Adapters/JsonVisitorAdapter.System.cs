using System;
using System.Globalization;
using Unity.Properties;

namespace Unity.Serialization.Json
{
    class JsonVisitorAdapterSystem : JsonVisitorAdapter,
        IVisitAdapter<Guid>,
        IVisitAdapter<DateTime>,
        IVisitAdapter<TimeSpan>
    {
        public JsonVisitorAdapterSystem(JsonVisitor visitor) : base(visitor) { }

        public static void RegisterTypes()
        {
            TypeConversion.Register<SerializedStringView, Guid>(view => Guid.TryParseExact(view.ToString(), "N", out var guid) ? guid : default);
            TypeConversion.Register<Guid, string>(guid => guid.ToString("N", CultureInfo.InvariantCulture));

            TypeConversion.Register<SerializedStringView, DateTime>(view => DateTime.TryParseExact(view.ToString(), "o", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var dateTime) ? dateTime.ToLocalTime() : default);
            TypeConversion.Register<DateTime, string>(dateTime => dateTime.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture));

            TypeConversion.Register<SerializedStringView, TimeSpan>(view => TimeSpan.TryParseExact(view.ToString(), "c", CultureInfo.InvariantCulture, out var timeSpan) ? timeSpan : default);
            TypeConversion.Register<TimeSpan, string>(timeSpan => timeSpan.ToString("c", CultureInfo.InvariantCulture));
        }

        public VisitStatus Visit<TProperty, TContainer>(IPropertyVisitor visitor, TProperty property, ref TContainer container, ref Guid value, ref ChangeTracker changeTracker)
            where TProperty : IProperty<TContainer, Guid>
        {
            AppendJsonString(property, value);
            return VisitStatus.Handled;
        }

        public VisitStatus Visit<TProperty, TContainer>(IPropertyVisitor visitor, TProperty property, ref TContainer container, ref DateTime value, ref ChangeTracker changeTracker)
            where TProperty : IProperty<TContainer, DateTime>
        {
            AppendJsonString(property, value);
            return VisitStatus.Override;
        }

        public VisitStatus Visit<TProperty, TContainer>(IPropertyVisitor visitor, TProperty property, ref TContainer container, ref TimeSpan value, ref ChangeTracker changeTracker)
            where TProperty : IProperty<TContainer, TimeSpan>
        {
            AppendJsonString(property, value);
            return VisitStatus.Override;
        }
    }
}
