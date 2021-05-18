namespace Platform::Timestamps
{
    class UniqueTimestampFactory
    {
    private: std::uint64_t _lastTicks = 0;
    public: Timestamp Create()
        {
            uint64_t utcTicks = common_era_clock::to_ticks(common_era_clock::now());
            _lastTicks = utcTicks > _lastTicks ? utcTicks : _lastTicks + 1;
            return Timestamp(_lastTicks);
        }
    };
}
