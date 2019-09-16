using System;
using System.Globalization;
using System.IO;
using Unity.Properties;

namespace Unity.Serialization.Json
{
    class JsonPrimitiveAdapter : JsonVisitorAdapter
        , IVisitAdapterPrimitives
        , IVisitAdapter<string>
        , IVisitAdapter<Guid>
        , IVisitAdapter<DirectoryInfo>
        , IVisitAdapter<FileInfo>
        , IVisitAdapter
    {
        public JsonPrimitiveAdapter(JsonVisitor visitor) : base(visitor)
        {
        }

        public VisitStatus Visit<TProperty, TContainer>(IPropertyVisitor visitor, TProperty property, ref TContainer container, ref sbyte value, ref ChangeTracker changeTracker)
            where TProperty : IProperty<TContainer, sbyte>
        {
            Append(property, value, (builder, v) => { builder.Append(v); });
            return VisitStatus.Handled;
        }

        public VisitStatus Visit<TProperty, TContainer>(IPropertyVisitor visitor, TProperty property, ref TContainer container, ref short value, ref ChangeTracker changeTracker)
            where TProperty : IProperty<TContainer, short>
        {
            Append(property, value, (builder, v) => { builder.Append(v); });
            return VisitStatus.Handled;
        }

        public VisitStatus Visit<TProperty, TContainer>(IPropertyVisitor visitor, TProperty property, ref TContainer container, ref int value, ref ChangeTracker changeTracker)
            where TProperty : IProperty<TContainer, int>
        {
            Append(property, value, (builder, v) => { builder.Append(v); });
            return VisitStatus.Handled;
        }

        public VisitStatus Visit<TProperty, TContainer>(IPropertyVisitor visitor, TProperty property, ref TContainer container, ref long value, ref ChangeTracker changeTracker)
            where TProperty : IProperty<TContainer, long>
        {
            Append(property, value, (builder, v) => { builder.Append(v); });
            return VisitStatus.Handled;
        }

        public VisitStatus Visit<TProperty, TContainer>(IPropertyVisitor visitor, TProperty property, ref TContainer container, ref byte value, ref ChangeTracker changeTracker)
            where TProperty : IProperty<TContainer, byte>
        {
            Append(property, value, (builder, v) => { builder.Append(v); });
            return VisitStatus.Handled;
        }

        public VisitStatus Visit<TProperty, TContainer>(IPropertyVisitor visitor, TProperty property, ref TContainer container, ref ushort value, ref ChangeTracker changeTracker)
            where TProperty : IProperty<TContainer, ushort>
        {
            Append(property, value, (builder, v) => { builder.Append(v); });
            return VisitStatus.Handled;
        }

        public VisitStatus Visit<TProperty, TContainer>(IPropertyVisitor visitor, TProperty property, ref TContainer container, ref uint value, ref ChangeTracker changeTracker)
            where TProperty : IProperty<TContainer, uint>
        {
            Append(property, value, (builder, v) => { builder.Append(v); });
            return VisitStatus.Handled;
        }

        public VisitStatus Visit<TProperty, TContainer>(IPropertyVisitor visitor, TProperty property, ref TContainer container, ref ulong value, ref ChangeTracker changeTracker)
            where TProperty : IProperty<TContainer, ulong>
        {
            Append(property, value, (builder, v) => { builder.Append(v); });
            return VisitStatus.Handled;
        }

        public VisitStatus Visit<TProperty, TContainer>(IPropertyVisitor visitor, TProperty property, ref TContainer container, ref float value, ref ChangeTracker changeTracker)
            where TProperty : IProperty<TContainer, float>
        {
            Append(property, value, (builder, v) => { builder.Append(v.ToString(CultureInfo.InvariantCulture)); });
            return VisitStatus.Handled;
        }

        public VisitStatus Visit<TProperty, TContainer>(IPropertyVisitor visitor, TProperty property, ref TContainer container, ref double value, ref ChangeTracker changeTracker)
            where TProperty : IProperty<TContainer, double>
        {
            Append(property, value, (builder, v) => { builder.Append(v.ToString(CultureInfo.InvariantCulture)); });
            return VisitStatus.Handled;
        }

        public VisitStatus Visit<TProperty, TContainer>(IPropertyVisitor visitor, TProperty property, ref TContainer container, ref bool value, ref ChangeTracker changeTracker)
            where TProperty : IProperty<TContainer, bool>
        {
            Append(property, value, (builder, v) => { builder.Append(v ? "true" : "false"); });
            return VisitStatus.Handled;
        }

        public VisitStatus Visit<TProperty, TContainer>(IPropertyVisitor visitor, TProperty property, ref TContainer container, ref char value, ref ChangeTracker changeTracker)
            where TProperty : IProperty<TContainer, char>
        {
            Append(property, value, (builder, v) => { builder.Append(EncodeJsonString(string.Empty + v)); });
            return VisitStatus.Handled;
        }

        public VisitStatus Visit<TProperty, TContainer>(IPropertyVisitor visitor, TProperty property, ref TContainer container, ref string value, ref ChangeTracker changeTracker)
            where TProperty : IProperty<TContainer, string>
        {
            Append(property, value, (builder, v) => { builder.Append(EncodeJsonString(v)); });
                                                   return VisitStatus.Handled;
        }

        public VisitStatus Visit<TProperty, TContainer>(IPropertyVisitor visitor, TProperty property, ref TContainer container, ref Guid value, ref ChangeTracker changeTracker)
            where TProperty : IProperty<TContainer, Guid>
        {
            Append(property, value, (builder, v) => { builder.Append(EncodeJsonString(v.ToString("N"))); });
            return VisitStatus.Handled;
        }

        public VisitStatus Visit<TProperty, TContainer>(IPropertyVisitor visitor, TProperty property, ref TContainer container, ref DirectoryInfo value, ref ChangeTracker changeTracker)
            where TProperty : IProperty<TContainer, DirectoryInfo>
        {
            Append(property, value, (builder, v) => { builder.Append(EncodeJsonString(v.GetRelativePath())); });
            return VisitStatus.Handled;
        }

        public VisitStatus Visit<TProperty, TContainer>(IPropertyVisitor visitor, TProperty property, ref TContainer container, ref FileInfo value, ref ChangeTracker changeTracker)
            where TProperty : IProperty<TContainer, FileInfo>
        {
            Append(property, value, (builder, v) => { builder.Append(EncodeJsonString(v.GetRelativePath())); });
            return VisitStatus.Handled;
        }

        public VisitStatus Visit<TProperty, TContainer, TValue>(IPropertyVisitor visitor, TProperty property, ref TContainer container, ref TValue value, ref ChangeTracker changeTracker) where TProperty : IProperty<TContainer, TValue>
        {
            if (!typeof(TValue).IsEnum)
                return VisitStatus.Unhandled;

            Append(property, value, (builder, v) =>
            {
                var underlyingType = Enum.GetUnderlyingType(typeof(TValue));
                switch (Type.GetTypeCode(underlyingType))
                {
                    case TypeCode.Byte:
                        builder.Append((byte) (object) v);
                        break;
                    case TypeCode.Int16:
                        builder.Append((short) (object) v);
                        break;
                    case TypeCode.Int32:
                        builder.Append((int) (object) v);
                        break;
                    case TypeCode.Int64:
                        builder.Append((long) (object) v);
                        break;
                    case TypeCode.SByte:
                        builder.Append((sbyte) (object) v);
                        break;
                    case TypeCode.UInt16:
                        builder.Append((ushort) (object) v);
                        break;
                    case TypeCode.UInt32:
                        builder.Append((uint) (object) v);
                        break;
                    case TypeCode.UInt64:
                        builder.Append((ulong) (object) v);
                        break;
                    default:
                        throw new InvalidOperationException($"Unable to serialize enum value: {v} of type {typeof(TValue).FullName}.");
                }
            });
            return VisitStatus.Handled;
        }
    }

    static class StringExtensions
    {
        public static string ToForwardSlash(this string value)
        {
            return value.Replace('\\', '/');
        }
    }

    static class DirectoryInfoExtensions
    {
        public static string GetRelativePath(this DirectoryInfo directoryInfo)
        {
            var relativePath = new DirectoryInfo(".").FullName.ToForwardSlash();
            var path = directoryInfo.FullName.ToForwardSlash();
            return path.StartsWith(relativePath) ? path.Substring(relativePath.Length).TrimStart('/') : path;
        }
    }

    static class FileInfoExtensions
    {
        public static string GetRelativePath(this FileInfo fileInfo)
        {
            var relativePath = new DirectoryInfo(".").FullName.ToForwardSlash();
            var path = fileInfo.FullName.ToForwardSlash();
            return path.StartsWith(relativePath) ? path.Substring(relativePath.Length).TrimStart('/') : path;
        }
    }
}
