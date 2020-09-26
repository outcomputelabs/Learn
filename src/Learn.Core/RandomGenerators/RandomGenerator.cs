using System;
using System.Diagnostics.CodeAnalysis;

namespace Learn.Core.RandomGenerators
{
    [SuppressMessage("Critical Code Smell", "S2696:Instance members should not write to \"static\" fields")]
    internal class RandomGenerator : IRandomGenerator
    {
        private static readonly Random _global = new Random();

        [ThreadStatic]
        private static Random? _local;

        public int Next(int minValue, int maxValue)
        {
            EnsureLocal();

            return _local!.Next(minValue, maxValue);
        }

        public int Next(int maxValue)
        {
            EnsureLocal();

            return _local!.Next(maxValue);
        }

        private static void EnsureLocal()
        {
            if (_local is null)
            {
                lock (_global)
                {
                    _local = new Random(_global.Next());
                }
            }
        }
    }
}