namespace Platform::Timestamps::Tests
{
    TEST_CLASS(UniqueTimestampFactoryTests)
    {
        public: TEST_METHOD(UniqueTimestampTest)
        {
            auto factory = UniqueTimestampFactory();
            auto timestamp1 = factory.Create();
            auto timestamp2 = factory.Create();
            Assert::AreNotEqual(timestamp1, timestamp2);
        }
    };
}
