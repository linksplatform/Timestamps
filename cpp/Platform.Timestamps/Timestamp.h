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
	    Timestamp(const uint64_t &ticks) : ticks_(ticks) { }
        Timestamp(const std::chrono::time_point<CommonEraClock> &tp) : ticks_(tp.time_since_epoch().count()) { }
        Timestamp() : ticks_(0) { }
        bool operator ==(const Timestamp &other) const { return ticks_ == other.get_ticks(); }
		uint64_t get_ticks() const noexcept { return this->ticks_; }
        std::string to_string()
        {
        	std::time_t timer = CommonEraClock::to_time_t(CommonEraClock::from_ticks(ticks_));
        	std::string temp = std::to_string(ticks_);
        	std::stringstream string_stream;
        	string_stream << std::put_time(std::gmtime(&timer), default_format.c_str()) << temp.substr(temp.length() - 7);
        	return string_stream.str();
        }
	private:
        uint64_t ticks_;
	    const std::string default_format = "%Y.%m.%d %H:%M:%S.";

    };
}

namespace std
{
	template<>
	struct hash<Platform::Timestamps::Timestamp>
	{
		size_t operator()(const Platform::Timestamps::Timestamp& timestamp) noexcept
		{
			return std::hash<uint64_t>{}(timestamp.get_ticks());
		}
	};
}
