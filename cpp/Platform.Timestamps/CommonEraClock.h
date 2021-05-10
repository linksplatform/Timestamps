#pragma once

#include <chrono>

struct CommonEraClock
{
    static constexpr uint64_t ticks_after_ad = 621355968000000000;
    using ticks = std::chrono::duration<uint64_t, std::ratio_multiply<std::nano, std::ratio<100> > >;
    using duration = ticks;
    using rep = duration::rep;
    using period = duration::period;
    using time_point = std::chrono::time_point<CommonEraClock>;
    static const bool is_steady = false;

        static duration time_since_epoch()
        {
            return std::chrono::duration_cast<duration>
                (std::chrono::system_clock::now().time_since_epoch()) + duration(ticks_after_ad);
        }

        static time_point now() noexcept
        {
            return time_point(time_since_epoch());
        }

        static std::chrono::time_point<std::chrono::system_clock, std::chrono::seconds>
            to_sys(const time_point& common_time_point) noexcept
        {
            return std::chrono::time_point<std::chrono::system_clock, std::chrono::seconds>
                    (std::chrono::duration_cast<std::chrono::seconds>(common_time_point.time_since_epoch())
                            - std::chrono::duration_cast<std::chrono::seconds>(duration(ticks_after_ad)));
        }

        static time_point from_sys(const std::chrono::time_point
            <std::chrono::system_clock, std::chrono::seconds>& sys_time_point) noexcept
        {
            return time_point(std::chrono::duration_cast<duration>(sys_time_point.time_since_epoch())
                + duration(ticks_after_ad));
        }

        static uint64_t to_ticks(const time_point& common_time_point) noexcept
        {
            return common_time_point.time_since_epoch().count();
        }

        static time_point from_ticks(const uint64_t& tick_number) noexcept
        {
            return time_point(duration(tick_number));
        }

        static std::time_t to_time_t(const time_point& common_time_point) noexcept
        {
            return std::chrono::system_clock::to_time_t(to_sys(common_time_point));
        }

        static time_point from_time_t(const std::time_t& time) noexcept
        {
            return std::chrono::time_point_cast<duration>
                (std::chrono::time_point<CommonEraClock, duration>(std::chrono::seconds(time)));
        }
};