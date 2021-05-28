namespace Platform::Timestamps
{
    struct Timestamp
    {
        public: static constexpr const char* DefaultFormat = "%Y.%m.%d %H:%M:%S";

        public: static constexpr std::uint64_t TicksPerSecond = 10'000'000; // the number of ticks passed from 0001/01/01 12:00 to 1970/01/01 00:00

        public: const std::uint64_t Ticks;

        public: constexpr Timestamp(std::uint64_t ticks) noexcept
            : Ticks(ticks)
        {
        }

        public: constexpr Timestamp(const common_era_clock::time_point& common_time_point)
            : Ticks(common_era_clock::to_ticks(common_time_point))
        {
        }

        public: constexpr operator common_era_clock::time_point() const
        {
            return common_era_clock::from_ticks(Ticks);
        }

        public: constexpr Timestamp(const Timestamp& timestamp)
            : Ticks(timestamp.Ticks)
        {
        }

        public: constexpr operator std::uint64_t() const
        {
            return Ticks;
        }

        public: operator std::string() const
        {
            std::time_t time = common_era_clock::to_time_t(*this);
            std::stringstream stream;
            stream << std::put_time(std::gmtime(&time), DefaultFormat)
                   << '.' << Ticks % TicksPerSecond;
            return stream.str();
        }

        public: friend std::ostream& operator<<(std::ostream& out, const Timestamp& timestamp)
        {
            return out << static_cast<std::string>(timestamp);
        }

        public: constexpr auto operator<=>(const Timestamp& other) const noexcept = default;
    };
} // namespace Platform::Timestamps

namespace std
{
    template<>
    struct hash<Platform::Timestamps::Timestamp>
    {
        std::size_t operator()(const Platform::Timestamps::Timestamp& timestamp) const noexcept
        {
            return std::hash<std::uint64_t>{}(timestamp.Ticks);
        }
    };
}
