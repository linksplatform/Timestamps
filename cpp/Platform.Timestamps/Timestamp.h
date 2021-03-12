namespace Platform::Timestamps
{
    struct Timestamp : public IEquatable<Timestamp>
    {
        public: inline static std::string DefaultFormat = "yyyy.MM.dd hh:mm:ss.fffffff";

        public: std::uint64_t Ticks = 0;

        public: Timestamp(std::uint64_t ticks) { Ticks = ticks; }

        public: Timestamp(DateTime dateTime) : Timestamp((std::uint64_t)dateTime.ToUniversalTime().Ticks) { }

        public: operator DateTime() const { return DateTime((std::int64_t)this->Ticks, DateTimeKind.Utc); }

        public: Timestamp(std::uint64_t ticks) : Timestamp(ticks) { }

        public: operator ulong() const { return this->Ticks; }

        public: override std::string ToString() { return ((DateTime)this).ToString(DefaultFormat); }

        public: bool operator ==(const Timestamp &other) const { return Ticks == other.Ticks; }

        public: override std::int32_t GetHashCode() { return Ticks.GetHashCode(); }
    };
}
