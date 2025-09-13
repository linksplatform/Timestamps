using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Platform.Timestamps;

namespace Platform.Timestamps.Experiments
{
    /// <summary>
    /// Demonstrates the thread-safety issue with the current UniqueTimestampFactory
    /// </summary>
    public class ThreadSafetyTest
    {
        public static void DemonstrateRaceCondition()
        {
            var factory = new UniqueTimestampFactory();
            var timestamps = new ConcurrentBag<Timestamp>();
            var tasks = new Task[100];

            // Create 100 concurrent tasks, each generating 10 timestamps
            for (int i = 0; i < 100; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    for (int j = 0; j < 10; j++)
                    {
                        timestamps.Add(factory.Create());
                    }
                });
            }

            Task.WaitAll(tasks);

            // Check for duplicates
            var timestampList = new List<Timestamp>(timestamps);
            var uniqueTimestamps = new HashSet<Timestamp>(timestampList);
            
            Console.WriteLine($"Total timestamps generated: {timestampList.Count}");
            Console.WriteLine($"Unique timestamps: {uniqueTimestamps.Count}");
            Console.WriteLine($"Duplicates found: {timestampList.Count - uniqueTimestamps.Count}");
        }
    }
}