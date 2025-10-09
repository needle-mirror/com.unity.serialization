using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Unity.Properties;
using UnityEngine;

namespace Unity.Serialization.Tests
{
    [TestFixture]
    partial class SerializationTestFixture
    {
        internal abstract class BaseClassWithGenericField<T>
        {
            public T BaseValue;
        }
    
        internal class ClassWithMultipleGenerics<T, V> : BaseClassWithGenericField<V>
        {
            public T FirstGeneric;
            public V SecondGeneric;
        }
        
        [GeneratePropertyBag]
        internal class ClassWithMultipleLevelsOfGenerics : ClassWithMultipleGenerics<int, float>
        {
        }

        [Test] 
        public void ClassWithMultipleLevelsOfGenerics_CanBeSerializedAndDeserialized()
        {
            var src = new ClassWithMultipleLevelsOfGenerics
            {
                FirstGeneric = 1,
                SecondGeneric = 2,
                BaseValue = 3
            };

            var dst = SerializeAndDeserialize(src);

            Assert.That(dst, Is.Not.SameAs(src));
            Assert.That(dst.FirstGeneric, Is.EqualTo(src.FirstGeneric));
            Assert.That(dst.SecondGeneric, Is.EqualTo(src.SecondGeneric));
            Assert.That(dst.BaseValue, Is.EqualTo(src.BaseValue));
        }
        
        
        public class QueueBase : MonoBehaviour
        {
        }
        
        public class QueueSlot : QueueBase
        {
        }
        
        public class QueueSlotChild : QueueSlot
        {
        }
        
        [Test] 
        public void ClassWithMultipleComponents_CanBeSerializedAndDeserialized()
        {
            BlackboardVariable variable = new BlackboardVariable<QueueBase>();
            var convertedVariable = Activator.CreateInstance(typeof(ClassWithMultipleComponents<,>).MakeGenericType(typeof(QueueBase), typeof(QueueSlot)), variable) as BlackboardVariable;

            var dst = SerializeAndDeserialize(convertedVariable);
            
            Assert.That(dst, Is.Not.SameAs(null));
            Assert.That(dst, Is.Not.SameAs(convertedVariable));
        }
        
        [Test] 
        public void ClassWith3MultipleComponents_CanBeSerializedAndDeserialized()
        {
            BlackboardVariable variable = new BlackboardVariable<QueueBase>();
            var convertedVariable = Activator.CreateInstance(typeof(ClassWith3MultipleComponents<,,>).MakeGenericType(typeof(QueueBase), typeof(QueueSlotChild), typeof(QueueSlot)), variable) as BlackboardVariable;
            var dst = SerializeAndDeserialize(convertedVariable);
            
            Assert.That(dst, Is.Not.SameAs(null));
            Assert.That(dst, Is.Not.SameAs(convertedVariable));
        }
    }
}