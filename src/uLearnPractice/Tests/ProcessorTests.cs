using GenericPractice;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ProcessorTests
    {
        [Test]
        public void CheckCreatedType_Test()
        {
            var processor = Processor.CreateEngine<int>().For<string>().With<long>();
            Assert.AreEqual(typeof(Processor<int, string, long>), processor.GetType());
        }
    }
}
