using NUnit.Framework;
using System;

namespace Unity.Serialization.Json.Tests
{
    partial class JsonSerializationTests
    {
        [Test]
        public void JsonSerialization_SerializeGuid()
        {
            var src = new GuidContainer
            {
                Guid = Guid.NewGuid()
            };
            var dst = new GuidContainer();

            var json = JsonSerialization.Serialize(src);
            JsonSerialization.DeserializeFromString(json, ref dst);

            Assert.That(dst.Guid, Is.EqualTo(src.Guid));
        }

        class GuidContainer
        {
            public Guid Guid;
        }
    }
}
