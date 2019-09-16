using NUnit.Framework;
using System.IO;

namespace Unity.Serialization.Json.Tests
{
    partial class JsonSerializationTests
    {
        const string k_AbsoluteFile = "/Absolute/Path/With Spaces/Boring Text.txt";
        const string k_RelativeFile = "Relative/Path/With Spaces/My Awesome Texture.png";

        [Test]
        public void JsonSerialization_SerializeFileInfo()
        {
            var src = new FileInfoContainer
            {
                AbsoluteFile = new FileInfo(k_AbsoluteFile),
                RelativeFile = new FileInfo(k_RelativeFile)
            };

            var json = JsonSerialization.Serialize(src);
            Assert.That(json.Contains(k_AbsoluteFile + '\"'), Is.True);
            Assert.That(json.Contains('\"' + k_RelativeFile + '\"'), Is.True);

            var dst = new FileInfoContainer();
            JsonSerialization.DeserializeFromString(json, ref dst);
            Assert.That(dst.AbsoluteFile, Is.Not.Null);
            Assert.That(dst.AbsoluteFile.FullName, Is.EqualTo(src.AbsoluteFile.FullName));
            Assert.That(dst.RelativeFile, Is.Not.Null);
            Assert.That(dst.RelativeFile.FullName, Is.EqualTo(src.RelativeFile.FullName));
        }

        class FileInfoContainer
        {
            public FileInfo AbsoluteFile;
            public FileInfo RelativeFile;
        }
    }
}
