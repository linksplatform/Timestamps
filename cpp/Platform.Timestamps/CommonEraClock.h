#pragma once

#include <chrono>

namespace Platform::Timestamps
{
    struct common_era_clock
    {
        using ticks = std::chrono::duration<std::uint64_t, std::ratio_multiply<std::nano, std::ratio<100>>>;
        using duration = ticks;
        using rep = duration::rep;
        using period = duration::period;
        using time_point = std::chrono::time_point<common_era_clock>;
        static constexpr bool is_steady = false;
        static constexpr std::uint64_t ticks_between_anno_domini_and_unix_epoch = 621355968000000000; // the number of ticks passed from 0001/01/01 12:00 to 1970/01/01 00:00

        static duration time_since_epoch()
        {
            return std::chrono::duration_cast<duration>
                (std::chrono::system_clock::now().time_since_epoch()) + duration(ticks_between_anno_domini_and_unix_epoch);
        }

        static time_point now()
        {
            return time_point(time_since_epoch());
        }

        static std::chrono::system_clock::time_point to_sys(const common_era_clock::time_point& time_point)
        {
            using namespace std::chrono;
            return system_clock::time_point
            {
                duration_cast<system_clock::duration>(time_point.time_since_epoch()) -
                duration_cast<system_clock::duration>(common_era_clock::duration(ticks_between_anno_domini_and_unix_epoch))
            };
        }

        static time_point from_sys(const std::chrono::system_clock::time_point& time_point)
        {
            return common_era_clock::time_point(
    std::chrono::duration_cast<duration>(time_point.time_since_epoch()) + duration(ticks_between_anno_domini_and_unix_epoch));
        }

        constexpr static std::uint64_t to_ticks(const common_era_clock::time_point& time_point)
        {
            return time_point.time_since_epoch().count();
        }

        constexpr static time_point from_ticks(std::uint64_t tick_number)
        {
            return time_point(duration(tick_number));
        }

        static std::time_t to_time_t(const common_era_clock::time_point& time_point)
        {
            return std::chrono::system_clock::to_time_t(to_sys(time_point));
        }

        static time_point from_time_t(const std::time_t &time)
        {
            return std::chrono::time_point_cast<duration>
                (std::chrono::time_point<common_era_clock, duration>(std::chrono::seconds(time)));
        }
    };
} // namespace Platform::Timestamps
