#pragma once

#include <chrono>

using hundred_nanoseconds = std::chrono::duration<uint64_t, std::ratio_multiply<std::nano, std::ratio<100> > >;
static inline const uint64_t ticks_after_ad = 621355968000000000;

struct CommonEraClock
{
	using duration = hundred_nanoseconds;
	using rep = duration::rep;
	using period = duration::period;
	using time_point = std::chrono::time_point<CommonEraClock>;
	static const bool is_steady = false;

	static hundred_nanoseconds time_since_epoch()
	{
		return std::chrono::duration_cast<duration>
		        (std::chrono::system_clock::now().time_since_epoch()) + hundred_nanoseconds(ticks_after_ad);
	}

	static time_point now() noexcept
	{
		return time_point(time_since_epoch());
	}

	static std::chrono::time_point<std::chrono::system_clock, std::chrono::seconds> to_sys(const time_point& tp) noexcept
	{
		return std::chrono::time_point<std::chrono::system_clock, std::chrono::seconds>
		        (std::chrono::duration_cast<std::chrono::seconds>(tp.time_since_epoch())
		                - std::chrono::seconds(ticks_after_ad));
	}

	static time_point from_sys(const std::chrono::time_point<std::chrono::system_clock, std::chrono::seconds>& tp) noexcept
	{
		return time_point(std::chrono::duration_cast<duration>(tp.time_since_epoch()) + hundred_nanoseconds(ticks_after_ad));
	}

	static uint64_t to_ticks(const time_point& tp) noexcept
	{
		return tp.time_since_epoch().count();
	}

	static time_point from_ticks(const uint64_t& tick_number) noexcept
	{
		return time_point(hundred_nanoseconds(tick_number));
	}

	static std::time_t to_time_t(const time_point& tp) noexcept
	{
		return std::time_t(std::chrono::duration_cast<std::chrono::seconds>(tp.time_since_epoch()).count());
	}

	static time_point from_time_t(const std::time_t& t) noexcept
	{
		return std::chrono::time_point_cast<duration>
		        (std::chrono::time_point<CommonEraClock, duration>(std::chrono::seconds(t)));
	}
};