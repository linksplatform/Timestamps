namespace Platform::Timestamps
{
    class UniqueTimestampFactory : public IFactory<Timestamp>
    {
        private: std::uint64_t _lastTicks = 0;

        public: Timestamp Create()
        {
            auto utcTicks = (std::uint64_t)DateTime.UtcNow.Ticks;
            _lastTicks = utcTicks > _lastTicks ? utcTicks : _lastTicks + 1;
            return this->Timestamp(_lastTicks);
        }
    };
}
