using Xunit;

namespace Platform.Timestamps.Tests
{
    public class UniqueTimestampFactoryTests
    {
        [Fact]
        public void UniqueTimestampTest()
        {
            var factory = new UniqueTimestampFactory();
            var timestamp1 = factory.Create();
            var timestamp2 = factory.Create();
            Assert.NotEqual(timestamp1, timestamp2);
        }
    }
}
