using Xunit;

namespace Platform.Timestamps.Tests
{
    /// <summary>
    /// <para>
    /// Represents the unique timestamp factory tests.
    /// </para>
    /// <para></para>
    /// </summary>
    public class UniqueTimestampFactoryTests
    {
        /// <summary>
        /// <para>
        /// Tests that unique timestamp test.
        /// </para>
        /// <para></para>
        /// </summary>
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
