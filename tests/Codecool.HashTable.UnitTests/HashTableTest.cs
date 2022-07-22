using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Codecool.HashTable.UnitTests
{
    [TestFixture]
    public class HashTableTest
    {
        HashTable<string, string> _hashTable;

        [SetUp]
        public void InitHashTable()
        {
            // Add code that runs before each test method
            _hashTable = new HashTable<string, string>();
        }

        [Test]
        public void Indexer_PutAndGet_ValidKeysAdnValuesWithoutCollision_InsertedElementReceivedByKey()
        {
            // Arrange
            var key1 = "a String";
            var key2 = "also a String";
            var value1 = "value-1";
            var value2 = "value-2";
            // Act
            _hashTable[key1] = value1;
            _hashTable[key2] = value2;
            // Assert
            Assert.AreEqual(value1, _hashTable[key1]);
            Assert.AreEqual(value2, _hashTable[key2]);
        }

        [Test]
        public void PutAndGet_KeyHashCollision_InsertedElementReceivedByKey()
        {
            // Arrange
            var hashTable = new HashTable<StringWrapper, int>();
            var key1 = new StringWrapper("a String");
            var key2 = new StringWrapper("also a String");
            var value1 = 0;
            var value2 = int.MaxValue;
            // Act
            hashTable[key1] = 0;
            hashTable[key2] = value2;
            // Assert
            Assert.AreEqual(value1, hashTable[key1]);
            Assert.AreEqual(value2, hashTable[key2]);
        }

        [Test]
        public void PutAndGet_NullKey_ElementIsStoredAndCanBeReceived()
        {
            // Arrange
            string key = null;
            String value = "a String";
            // Act
            _hashTable[key] = value;
            // Assert
            Assert.AreEqual(value, _hashTable[key]);
        }

        [Test]
        public void PutAndGet_NullValue_StoredAndCanBeReceived()
        {
            // Arrange
            var key = "key-for-null";
            // Act
            _hashTable[key] = null;
            // Assert
            Assert.IsNull(_hashTable[key]);
        }

        [Test]
        public void Put_ElementWithSameKeyMultipleTimes_OverrideValueForSpecificKey()
        {
            // Arrange
            var key = "key";
            var value1 = "value1";
            var value2 = "value2";
            // Act
            _hashTable[key] = value1;
            _hashTable[key] = value2;
            // Assert
            Assert.AreEqual(value2, _hashTable[key]);
        }

        [Test]
        public void Get_NonExistingKey_ThrowsKeyNotFuondExceptionAfterRemove()
        {
            // Arrange
            var key = "non-existing-key";
            // Act
            // Assert
            Assert.Throws<KeyNotFoundException>(() => { var notFound = _hashTable[key]; });
        }

        [Test]
        public void Remove_ExistingKey_ReturnsWithValueBelongsToKey()
        {
            // Arrange
            var key = "key";
            var value = "value";
            _hashTable[key] = value;
            // Act
            string remove = _hashTable.Remove(key);
            // Assert
            Assert.AreEqual(value, remove);
            Assert.Throws<KeyNotFoundException>(() => { var notfound = _hashTable[key]; });
        }

        [Test]
        public void Remove_NonExistingKey_ThrowsKeyNotFuondException()
        {
            // Arrange
            var existingKey = "existing-key";
            var nonExistingKey = "non-existing-key";
            var value = "value";
            _hashTable[existingKey] = value;
            // Act
            // Assert
            Assert.Throws<KeyNotFoundException>(() => _hashTable.Remove(nonExistingKey));
        }

        [Test]
        public void Clear_NonEmptyHashTable_DeletesAllEntry()
        {
            // Arrange
            var value = "value";
            var keys = 20;
            for (int i = 0; i < keys; i++)
            {
                var key = i.ToString();
                _hashTable[key] = value;
            }
            // Act
            _hashTable.Clear();
            // Assert
            for (int i = 0; i < keys; i++)
            {
                Assert.Throws<KeyNotFoundException>(() => { var notFound = _hashTable[i.ToString()]; });
            }
        }

        /// <summary>
        /// Helper class that ensures hash collision
        /// by returning always the same hash code.
        /// </summary>
        class StringWrapper
        {
            private string _text;

            public StringWrapper(string text)
            {
                this._text = text ?? throw new ArgumentNullException(nameof(text));
            }

            public override bool Equals(object obj)
            {
                return obj is StringWrapper wrapper &&
                       _text == wrapper._text;
            }

            public override int GetHashCode()
            {
                return 0;
            }
        }
    }
}
