#pragma once

#include <chrono>

using hundred_nanoseconds = std::chrono::duration<uint64_t, std::ratio<1, 10000000> >;
static inline const uint64_t ticks_after_ad = 621355968000000000;

struct CommonEraClock
{
	using duration = hundred_nanoseconds;
	using rep = duration::rep;
	using period = duration::period;
	using time_point = std::chrono::time_point<CommonEraClock>;
	static const bool is_steady = false;

	static std::chrono::duration<uint64_t, std::ratio<1, 10000000> > time_since_epoch()
	{
		return std::chrono::duration_cast<duration>(std::chrono::system_clock::now().time_since_epoch()) + hundred_nanoseconds(ticks_after_ad);
	}

	static time_point now() noexcept
	{
		return time_point(time_since_epoch());
	}
};