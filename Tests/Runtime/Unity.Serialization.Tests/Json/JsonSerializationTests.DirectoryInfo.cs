using NUnit.Framework;
using System.IO;

namespace Unity.Serialization.Json.Tests
{
    partial class JsonSerializationTests
    {
        const string k_AbsoluteDirectory = "/Absolute/Path/With Spaces";
        const string k_RelativeDirectory = "Relative/Path/With Spaces";

        [Test]
        public void JsonSerialization_SerializeDirectoryInfo()
        {
            var src = new DirectoryInfoContainer
            {
                AbsoluteDirectory = new DirectoryInfo(k_AbsoluteDirectory),
                RelativeDirectory = new DirectoryInfo(k_RelativeDirectory)
            };

            var json = JsonSerialization.Serialize(src);
            Assert.That(json.Contains(k_AbsoluteDirectory + '\"'), Is.True);
            Assert.That(json.Contains('\"' + k_RelativeDirectory + '\"'), Is.True);

            var dst = new DirectoryInfoContainer();
            JsonSerialization.DeserializeFromString(json, ref dst);
            Assert.That(dst.AbsoluteDirectory, Is.Not.Null);
            Assert.That(dst.AbsoluteDirectory.FullName, Is.EqualTo(src.AbsoluteDirectory.FullName));
            Assert.That(dst.RelativeDirectory, Is.Not.Null);
            Assert.That(dst.RelativeDirectory.FullName, Is.EqualTo(src.RelativeDirectory.FullName));
        }

        class DirectoryInfoContainer
        {
            public DirectoryInfo AbsoluteDirectory;
            public DirectoryInfo RelativeDirectory;
        }
    }
}
