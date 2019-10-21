using NUnit.Framework;

namespace Unity.Serialization.Json.Tests
{
    partial class JsonSerializationTests
    {
        const string k_AssetPath = "Assets/Tests/test-image.asset";

        class UnityObjectContainer
        {
            public UnityEngine.Object Object;
        }

        class GlobalObjectIdContainer
        {
            public UnityEditor.GlobalObjectId Id;
        }

        [SetUp]
        public void CreateAssets()
        {
            var image = new UnityEngine.Texture2D(1, 1);
            UnityEditor.AssetDatabase.CreateAsset(image, k_AssetPath);
            UnityEditor.AssetDatabase.ImportAsset(k_AssetPath, UnityEditor.ImportAssetOptions.ForceSynchronousImport | UnityEditor.ImportAssetOptions.ForceUpdate);
        }

        [TearDown]
        public void DeleteAssets()
        {
            UnityEditor.AssetDatabase.DeleteAsset(k_AssetPath);
        }

        [Test]
        public void JsonSerialization_SerializeUnityObject()
        {
            var src = new UnityObjectContainer
            {
                Object = UnityEditor.AssetDatabase.LoadMainAssetAtPath(k_AssetPath)
            };

            var json = JsonSerialization.Serialize(src);
            Assert.That(json, Does.Match("{\n    \"Object\": \"[\\w-]+\"\n}"));

            var dst = new UnityObjectContainer();
            JsonSerialization.DeserializeFromString(json, ref dst);
            Assert.That(dst.Object, Is.Not.Null);
            Assert.That(dst.Object, Is.Not.False);
            Assert.That(UnityEditor.AssetDatabase.GetAssetPath(dst.Object), Is.EqualTo(k_AssetPath));
        }

        [Test, Ignore("GlobalObjectIdentifierToObjectSlow currently returns null in this case")]
        public void JsonSerialization_SerializeUnityObject_DeserializeDeletedAsset()
        {
            var src = new UnityObjectContainer
            {
                Object = UnityEditor.AssetDatabase.LoadMainAssetAtPath(k_AssetPath)
            };

            var json = JsonSerialization.Serialize(src);
            Assert.That(json, Does.Match("{\n    \"Object\": \"[\\w-]+\"\n}"));

            UnityEditor.AssetDatabase.DeleteAsset(k_AssetPath);

            var dst = new UnityObjectContainer();
            JsonSerialization.DeserializeFromString(json, ref dst);
            Assert.That(dst.Object, Is.Not.Null);
            Assert.That(dst.Object, Is.False);
            Assert.That(dst.Object.GetType(), Is.EqualTo(typeof(UnityEngine.Texture2D)));
        }

        [Test]
        public void JsonSerialization_SerializeUnityObject_FromGlobalObjectId()
        {
            var src = new GlobalObjectIdContainer
            {
                Id = UnityEditor.GlobalObjectId.GetGlobalObjectIdSlow(UnityEditor.AssetDatabase.LoadMainAssetAtPath(k_AssetPath))
            };

            var json = JsonSerialization.Serialize(src);
            Assert.That(json, Does.Match("{\n    \"Id\": \"[\\w-]+\"\n}"));

            var dst = new GlobalObjectIdContainer();
            JsonSerialization.DeserializeFromString(json, ref dst);
            Assert.That(dst.Id, Is.Not.EqualTo(new UnityEditor.GlobalObjectId()));

            var obj = UnityEditor.GlobalObjectId.GlobalObjectIdentifierToObjectSlow(dst.Id);
            Assert.That(obj, Is.Not.Null);
            Assert.That(obj, Is.Not.False);
            Assert.That(UnityEditor.AssetDatabase.GetAssetPath(obj), Is.EqualTo(k_AssetPath));
        }
    }
}
