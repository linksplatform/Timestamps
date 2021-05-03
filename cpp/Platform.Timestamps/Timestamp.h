// Since I'm working only with this repo, my compiler doesn't really like the absence of std in this file, I'm not sure
// whether uint64_t is gonna work without "std::", so I'm gonna remove them for now

namespace Platform::Timestamps
{
    class Timestamp : public IEquatable<Timestamp>
    {
	public:
        const inline static std::string DefaultFormat = "yyyy.MM.dd hh:mm:ss.fffffff";
        operator DateTime() const { return DateTime((int64_t)this->Ticks, DateTimeKind.Utc); }

        // Timestamp (std::uint64_t ticks) : Timestamp(ticks) { }
		//
		// Can't see the difference between casting from a uint64_t and creating a new object;
		// probably gonna remove that

        operator uint64_t() const { return this->Ticks; } // I REALLY want to rename this to sth like
													   // operator uint64_t, but the compatibility may
													   // be ruined
        bool operator ==(const Timestamp &other) const { return Ticks == other.getTicks; }
        bool operator !=(const Timestamp &other) const { return Ticks != other.getTicks; }
        override std::string ToString() { return ((DateTime)this).ToString(DefaultFormat); }
        override int32_t GetHashCode() { return Ticks.GetHashCode(); } // The std::hash method
																			// is available in
																			// the <functional> header;
																			// shall we include it?
		uint64_t getTicks() const { return this->Ticks; }
	private:
        uint64_t Ticks;
    };

	Timestamp::Timestamp(const uint64_t &ticks) : Ticks(ticks) { }

    Timestamp::Timestamp(const DateTime &dateTime) : Timestamp((uint64_t)dateTime.ToUniversalTime().Ticks) { }
}
