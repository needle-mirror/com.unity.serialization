using NUnit.Framework;

namespace Unity.Serialization.Json.Tests
{
    partial class JsonSerializationTests
    {
        class UnityGUIDContainer
        {
            public UnityEditor.GUID Guid;
        }

        [Test]
        public void JsonSerialization_SerializeUnityGUID()
        {
            var src = new UnityGUIDContainer
            {
                Guid = UnityEditor.GUID.Generate()
            };
            var dst = new UnityGUIDContainer();

            var json = JsonSerialization.Serialize(src);
            JsonSerialization.DeserializeFromString(json, ref dst);

            Assert.That(dst.Guid, Is.EqualTo(src.Guid));
        }
    }
}
