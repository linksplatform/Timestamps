using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Platform.Timestamps.Tests
{
    public class UniqueTimestampFactoryTests
    {
        [Fact]
        public void UniqueTimestampTest()
        {
            var factory = new UniqueTimestampFactory();
            var timestamp1 = factory.Create();
            var timestamp2 = factory.Create();
            Assert.NotEqual(timestamp1, timestamp2);
        }

        [Fact]
        public void ThreadSafetyTest()
        {
            var factory = new UniqueTimestampFactory();
            var timestamps = new ConcurrentBag<Timestamp>();
            const int taskCount = 50;
            const int timestampsPerTask = 100;
            
            var tasks = new Task[taskCount];
            for (int i = 0; i < taskCount; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    for (int j = 0; j < timestampsPerTask; j++)
                    {
                        timestamps.Add(factory.Create());
                    }
                });
            }
            
            Task.WaitAll(tasks);
            
            var timestampList = timestamps.ToList();
            var uniqueTimestamps = new HashSet<Timestamp>(timestampList);
            
            Assert.Equal(taskCount * timestampsPerTask, timestampList.Count);
            Assert.Equal(timestampList.Count, uniqueTimestamps.Count);
        }

        [Fact]
        public void SequentialOrderTest()
        {
            var factory = new UniqueTimestampFactory();
            var timestamps = new List<Timestamp>();
            
            for (int i = 0; i < 1000; i++)
            {
                timestamps.Add(factory.Create());
            }
            
            // Verify timestamps are in ascending order
            for (int i = 1; i < timestamps.Count; i++)
            {
                Assert.True(timestamps[i].Ticks > timestamps[i - 1].Ticks);
            }
        }

        [Fact]
        public void ConcurrentSequentialOrderTest()
        {
            var factory = new UniqueTimestampFactory();
            var timestamps = new ConcurrentBag<Timestamp>();
            const int taskCount = 10;
            const int timestampsPerTask = 50;
            
            var tasks = new Task[taskCount];
            for (int i = 0; i < taskCount; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    var localTimestamps = new List<Timestamp>();
                    for (int j = 0; j < timestampsPerTask; j++)
                    {
                        localTimestamps.Add(factory.Create());
                    }
                    
                    // Verify local sequence is ordered
                    for (int k = 1; k < localTimestamps.Count; k++)
                    {
                        Assert.True(localTimestamps[k].Ticks > localTimestamps[k - 1].Ticks);
                    }
                    
                    foreach (var ts in localTimestamps)
                    {
                        timestamps.Add(ts);
                    }
                });
            }
            
            Task.WaitAll(tasks);
            
            var allTimestamps = timestamps.ToList();
            var uniqueTimestamps = new HashSet<Timestamp>(allTimestamps);
            
            Assert.Equal(taskCount * timestampsPerTask, allTimestamps.Count);
            Assert.Equal(allTimestamps.Count, uniqueTimestamps.Count);
        }
    }
}
