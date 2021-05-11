#pragma once

#include <string>
#include <sstream>
#include <iomanip>
#include <functional>
#include "CommonEraClock.h"

namespace Platform::Timestamps
{
    class Timestamp
    {
	public:
        static constexpr const char* DefaultFormat = "%Y.%m.%d %H:%M:%S";
        static constexpr int TicksPerSecond = 10000000;

        Timestamp(const uint64_t &ticks) : _ticks(ticks) { }
        Timestamp(const std::chrono::time_point<CommonEraClock> &common_time_point)
            : _ticks(common_time_point.time_since_epoch().count()) { }
        Timestamp(const Timestamp& timestamp) : _ticks(timestamp.Ticks()) {}
        Timestamp() : _ticks(0) { }

        uint64_t Ticks() const noexcept { return _ticks; }
        void Ticks(const uint64_t& ticks)   { _ticks = ticks; }

        bool operator ==(const Timestamp &other) const { return _ticks == other.Ticks(); }
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
            return out << std::string(timestamp);
        }
    private:
        uint64_t _ticks;
    };
}

template<>
struct std::hash<Platform::Timestamps::Timestamp>
{
    size_t operator()(const Platform::Timestamps::Timestamp& timestamp) noexcept
    {
        return std::hash<uint64_t>{}(timestamp.Ticks());
    }
};
