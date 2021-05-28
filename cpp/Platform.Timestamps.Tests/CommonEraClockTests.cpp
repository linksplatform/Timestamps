#include <Platform.Timestamps.h>
#include <gtest/gtest.h>

#include <typeindex>

namespace Platform::Timestamps::Tests
{
    TEST(TimestampsTest, TimestampEquality)
    {
        Timestamp t1(0);
        Timestamp t2(common_era_clock::from_sys(std::chrono::system_clock::now()));
        ASSERT_NE(t1, t2);

        std::chrono::time_point<std::chrono::system_clock> time1 = std::chrono::system_clock::now();
        Timestamp timestamp1(time1.time_since_epoch().count());
        Timestamp timestamp2(timestamp1);
        ASSERT_EQ(timestamp1, timestamp2);
    }

    TEST(TimestampsTest, ClockConversion)
    {
        std::chrono::time_point<common_era_clock> time1 = common_era_clock::now();
        Platform::Timestamps::Timestamp t1(time1);
        std::chrono::time_point<common_era_clock> time2 = common_era_clock::from_ticks(t1.Ticks);
        Platform::Timestamps::Timestamp t2(time2.time_since_epoch().count());
        ASSERT_EQ(time1, time2);
        ASSERT_EQ(t1, t2);
        ASSERT_EQ(std::string(t1), std::string(t2));
        ASSERT_EQ(std::hash<Timestamp>{}(t1), std::hash<Timestamp>{}(t2));
        ASSERT_EQ(common_era_clock::to_time_t(time1), common_era_clock::to_time_t(time2));
    }
}
