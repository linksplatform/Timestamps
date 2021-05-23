#include <Platform.Timestamps.h>
#include <gtest/gtest.h>

namespace Platform::Timestamps::Tests
{
    TEST(TimestampsTest, UniqueFactoryTest)
    {
        auto factory = UniqueTimestampFactory();
        auto timestamp1 = factory.Create();
        auto timestamp2 = factory.Create();
        ASSERT_NE(timestamp1, timestamp2);
    }
}
