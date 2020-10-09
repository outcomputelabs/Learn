using Learn.Core.RandomGenerators;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Learn.Core.Tests
{
    public class RandomGeneratorTests
    {
        [Fact]
        public void NextMinMaxReturnsRandomNumbers()
        {
            // arrange
            var generator = new RandomGenerator();
            var rounds = 1000000;
            var min = 1000;
            var max = 10000;
            var frequency = new Dictionary<int, int>();

            // act
            for (var i = 0; i < rounds; ++i)
            {
                var number = generator.Next(min, max);

                if (frequency.TryGetValue(number, out var value))
                {
                    frequency[number] = value + 1;
                }
                else
                {
                    frequency[number] = 1;
                }
            }

            // assert
            var expected = max - ((max - min) / 2.0);
            var actual = frequency.Sum(x => (double)x.Key * x.Value) / rounds;
            Assert.True(actual >= expected - 1);
            Assert.True(actual <= expected + 1);
        }

        [Fact]
        public void NextMaxReturnsRandomNumbers()
        {
            // arrange
            var generator = new RandomGenerator();
            var rounds = 1000000;
            var max = 10000;
            var frequency = new Dictionary<int, int>();

            // act
            for (var i = 0; i < rounds; ++i)
            {
                var number = generator.Next(max);

                if (frequency.TryGetValue(number, out var value))
                {
                    frequency[number] = value + 1;
                }
                else
                {
                    frequency[number] = 1;
                }
            }

            // assert
            var expected = max / 2.0;
            var actual = frequency.Sum(x => (double)x.Key * x.Value) / rounds;
            Assert.True(actual >= expected - 1);
            Assert.True(actual <= expected + 1);
        }
    }
}