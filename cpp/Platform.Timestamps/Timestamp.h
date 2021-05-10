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
	    Timestamp(const uint64_t &t) : _ticks(t) { }
        Timestamp(const std::chrono::time_point<CommonEraClock> &tp) : _ticks(tp.time_since_epoch().count()) { }
        Timestamp() : _ticks(0) { }

        bool operator ==(const Timestamp &other) const { return _ticks == other.Ticks(); }

		uint64_t Ticks() const noexcept { return _ticks; }
		uint64_t Ticks(const uint64_t& t)
		{
	    	_ticks = t;
	    	return t;
		}

        operator std::string() const
        {
        	std::time_t timer = CommonEraClock::to_time_t(CommonEraClock::from_ticks(_ticks));
        	std::string temp = std::to_string(_ticks);
        	std::stringstream stringStream;
        	stringStream << std::put_time(std::gmtime(&timer), _defaultFormat.c_str()) << temp.substr(temp.length() - 7);
        	return stringStream.str();
        }

        friend std::ostream& operator<<(std::ostream& out, const Timestamp& timestamp)
        {
			return out << std::string(timestamp);
        }

	private:
        uint64_t _ticks;
	    const std::string _defaultFormat = "%Y.%m.%d %H:%M:%S.";
    };
}

namespace std
{
	std::string to_string(const Platform::Timestamps::Timestamp& timestamp) noexcept
	{
		return std::string(timestamp);
	}

	template<>
	struct hash<Platform::Timestamps::Timestamp>
	{
		size_t operator()(const Platform::Timestamps::Timestamp& timestamp) noexcept
		{
			return std::hash<uint64_t>{}(timestamp.Ticks());
		}
	};
}
