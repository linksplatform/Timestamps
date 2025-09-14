using Xunit;

namespace Platform.Timestamps.Tests
{
    /// <summary>
    /// <para>Represents tests for the <see cref="UniqueTimestampFactory"/> class.</para>
    /// <para>Представляет тесты для класса <see cref="UniqueTimestampFactory"/>.</para>
    /// </summary>
    public class UniqueTimestampFactoryTests
    {
        /// <summary>
        /// <para>Tests that consecutive timestamps created by the factory are unique.</para>
        /// <para>Проверяет, что последовательные метки времени, созданные фабрикой, являются уникальными.</para>
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
