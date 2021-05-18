#include <sstream>
#include <iomanip>
#include <sstream>
#include <string>
#include <typeindex>

#include "CommonEraClock.h"

namespace Platform::Timestamps
{
    class Timestamp
    {
        public: static constexpr const char* DefaultFormat = "%Y.%m.%d %H:%M:%S";

        public: const std::uint64_t Ticks;

        public: constexpr Timestamp(const std::uint64_t& ticks) noexcept
            : Ticks(ticks)
        {
        }

        public: constexpr Timestamp(const std::chrono::time_point<common_era_clock>& common_time_point)
            : Ticks(common_time_point.time_since_epoch().count())
        {
        }

        public: constexpr Timestamp(const Timestamp& timestamp)
            : Ticks(timestamp.Ticks)
        {
        }

        public: constexpr Timestamp()
             : Ticks(0)
        {
        }

        public: explicit operator std::string() const
        {
            std::time_t time = common_era_clock::to_time_t(common_era_clock::from_ticks(Ticks));
            std::stringstream stream;
            stream << std::put_time(std::gmtime(&time), DefaultFormat)
                   << '.' << Ticks % TicksPerSecond;
            return stream.str();
        }

        public: constexpr auto operator<=>(const Timestamp& other) const noexcept = default;

        public: friend std::ostream& operator<<(std::ostream& out, const Timestamp& timestamp)
        {
            return out << static_cast<std::string>(timestamp);
        }

        public: static constexpr std::uint64_t TicksPerSecond = 10'000'000;
    };
}// namespace Platform::Timestamps

template<>
struct std::hash<Platform::Timestamps::Timestamp>
{
    std::size_t operator()(const Platform::Timestamps::Timestamp& timestamp) const noexcept
    {
        return std::hash<std::uint64_t>{}(timestamp.Ticks);
    }
};