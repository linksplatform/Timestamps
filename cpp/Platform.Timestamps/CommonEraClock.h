#ifndef PLATFORM_TIMESTAMPS_COMMON_ERA_CLOCK
#define PLATFORM_TIMESTAMPS_COMMON_ERA_CLOCK

#include <chrono>

namespace Platform::Timestamps
{
    struct common_era_clock
    {
        using ticks = std::chrono::duration<std::uint64_t, std::ratio_multiply<std::nano, std::ratio<100> > >;
        using duration = ticks;
        using rep = duration::rep;
        using period = duration::period;
        using time_point = std::chrono::time_point<common_era_clock>;
        static constexpr bool is_steady = false;
        static constexpr std::uint64_t ticks_after_anno_domini = 621355968000000000;

        static duration time_since_epoch()
        {
            return std::chrono::duration_cast<duration>
                (std::chrono::system_clock::now().time_since_epoch()) + duration(ticks_after_anno_domini);
        }

        static time_point now()
        {
            return time_point(time_since_epoch());
        }

        static std::chrono::time_point<std::chrono::system_clock, std::chrono::seconds>
            to_sys(const time_point &common_time_point)
        {
            return std::chrono::time_point<std::chrono::system_clock, std::chrono::seconds>
                (std::chrono::duration_cast<std::chrono::seconds>(common_time_point.time_since_epoch())
                     - std::chrono::duration_cast<std::chrono::seconds>(duration(ticks_after_anno_domini)));
        }

        static time_point from_sys(const std::chrono::time_point
            <std::chrono::system_clock, std::chrono::system_clock::duration>& sys_time_point)
        {
            return time_point(std::chrono::duration_cast<duration>(sys_time_point.time_since_epoch())
                                  + duration(ticks_after_anno_domini));
        }

        static std::uint64_t to_ticks(const time_point &common_time_point)
        {
            return common_time_point.time_since_epoch().count();
        }

        static time_point from_ticks(std::uint64_t tick_number)
        {
            return time_point(duration(tick_number));
        }

        static std::time_t to_time_t(const time_point &common_time_point)
        {
            return std::chrono::system_clock::to_time_t(to_sys(common_time_point));
        }

        static time_point from_time_t(const std::time_t &time)
        {
            return std::chrono::time_point_cast<duration>
                (std::chrono::time_point<common_era_clock, duration>(std::chrono::seconds(time)));
        }
    };
} // namespace Platform::Timestamps

#endif // PLATFORM_TIMESTAMPS_COMMON_ERA_CLOCK