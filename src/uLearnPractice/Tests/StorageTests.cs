using System;
using System.Linq;
using GenericPractice;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class StorageTests
    {
        private IStorage<Guid> Storage { get; set; }
        
        [SetUp]
        public void SetUp()
        {
            Storage = new GuidStorage();
            Storage.Create<int>();
            Storage.Create<Guid>();
            Storage.Create<StorageTests>();
            Storage.Create<GuidStorage>();
            Storage.Create<GuidStorage>();
            Storage.Create<int>();
        }

        [Test]
        public void GetWithIntCount_Test()
        {
            var items = Storage.GetWith<int>();
            Assert.AreEqual(2, items.Count);
        }

        [Test]
        public void GetWithGuidStorageCount_Test()
        {
            var items = Storage.GetWith<GuidStorage>();
            Assert.AreEqual(2, items.Count);
        }

        [Test]
        public void GetWithStorageTestsCount_Test()
        {
            var items = Storage.GetWith<StorageTests>();
            Assert.AreEqual(1, items.Count);
        }

        [Test]
        public void GetWithGuidCount_Test()
        {
            var items = Storage.GetWith<Guid>();
            Assert.AreEqual(1, items.Count);
        }

        [Test]
        public void GetInt_Test()
        {
            var items = Storage.GetWith<int>();
            var all = items.All(x =>
            {
                var item = Storage.Get<int>(x.Key);
                return item.Equals(x.Value);
            });
            Assert.IsTrue(all);
        }

        [Test]
        public void GetGuid_Test()
        {
            var items = Storage.GetWith<Guid>();
            var all = items.All(x =>
            {
                var item = Storage.Get<Guid>(x.Key);
                return item.Equals(x.Value);
            });
            Assert.IsTrue(all);
        }

        [Test]
        public void GetGuidStorage_Test()
        {
            var items = Storage.GetWith<GuidStorage>();
            var all = items.All(x =>
            {
                var item = Storage.Get<GuidStorage>(x.Key);
                return item.Equals(x.Value);
            });
            Assert.IsTrue(all);
        }

        [Test]
        public void GetStorageTests_Test()
        {
            var items = Storage.GetWith<StorageTests>();
            var all = items.All(x =>
            {
                var item = Storage.Get<StorageTests>(x.Key);
                return item.Equals(x.Value);
            });
            Assert.IsTrue(all);
        }
    }
}