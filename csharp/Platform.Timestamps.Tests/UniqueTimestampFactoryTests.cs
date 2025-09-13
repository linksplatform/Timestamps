using Platform.Interfaces;
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

        [Fact]
        public void UniqueTimestampFactoryImplementsIFactoryConceptTest()
        {
            // Arrange: Create factory instance
            var factory = new UniqueTimestampFactory();

            // Act & Assert: Concept verification

            // 1. Factory must implement IFactory<Timestamp> interface
            Assert.IsAssignableFrom<IFactory<Timestamp>>(factory);

            // 2. Factory must create valid instances
            var instance1 = factory.Create();
            var instance2 = factory.Create();
            
            Assert.NotNull(instance1);
            Assert.NotNull(instance2);

            // 3. Type consistency - all created instances must be of correct type
            Assert.IsType<Timestamp>(instance1);
            Assert.IsType<Timestamp>(instance2);

            // 4. Factory contract for UniqueTimestampFactory - must create unique timestamps
            Assert.NotEqual(instance1, instance2);

            // 5. Multiple invocations should work consistently
            var timestamps = new Timestamp[100];
            for (int i = 0; i < 100; i++)
            {
                timestamps[i] = factory.Create();
                Assert.NotNull(timestamps[i]);
                Assert.IsType<Timestamp>(timestamps[i]);
            }

            // 6. All generated timestamps should be unique (specific to UniqueTimestampFactory concept)
            for (int i = 0; i < timestamps.Length - 1; i++)
            {
                for (int j = i + 1; j < timestamps.Length; j++)
                {
                    Assert.NotEqual(timestamps[i], timestamps[j]);
                }
            }

            // 7. Covariance support - factory should work as IFactory<Timestamp>
            IFactory<Timestamp> factoryInterface = factory;
            var timestampFromInterface = factoryInterface.Create();
            Assert.NotNull(timestampFromInterface);
            Assert.IsType<Timestamp>(timestampFromInterface);
        }
    }
}
