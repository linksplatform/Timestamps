#pragma once

#include <typeindex>
#include <iomanip>
#include <sstream>
#include <string>
#include "CommonEraClock.h"

namespace Platform::Timestamps
{
    class Timestamp
    {
        std::uint64_t _ticks;
    public:
        static constexpr const char* DefaultFormat = "%Y.%m.%d %H:%M:%S";
        static constexpr std::uint64_t TicksPerSecond = 10'000'000;

        constexpr Timestamp(const std::uint64_t& ticks) noexcept
            : _ticks(ticks)
        {
        }
        constexpr Timestamp(const std::chrono::time_point<CommonEraClock>& common_time_point)
            : _ticks(common_time_point.time_since_epoch().count())
        {
        }
        constexpr Timestamp(const Timestamp& timestamp)
            : _ticks(timestamp.Ticks())
        {
        }
        constexpr Timestamp() = default;

        constexpr std::uint64_t Ticks() const noexcept
        {
            return _ticks;
        }
        void Ticks(std::uint64_t ticks)
        {
            _ticks = ticks;
        }

        auto operator<=>(const Timestamp& other) const noexcept = default;

        explicit operator std::string() const
        {
            std::time_t time = CommonEraClock::to_time_t(CommonEraClock::from_ticks(_ticks));
            std::stringstream stream;
            stream << std::put_time(std::gmtime(&time), DefaultFormat)
                   << '.' << _ticks % TicksPerSecond;
            return stream.str();
        }

        friend std::ostream& operator<<(std::ostream& out, const Timestamp& timestamp)
        {
            return out << static_cast<std::string>(timestamp);
        }
    };
}// namespace Platform::Timestamps

template<>
struct std::hash<Platform::Timestamps::Timestamp>
{
    std::size_t operator()(const Platform::Timestamps::Timestamp& timestamp) const noexcept
    {
        return std::hash<std::uint64_t>{}(timestamp.Ticks());
    }
};