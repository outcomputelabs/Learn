using System.Diagnostics.CodeAnalysis;

namespace Learn.Core.RandomGenerators
{
    [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords")]
    public interface IRandomGenerator
    {
        int Next(int minValue, int maxValue);

        int Next(int maxValue);
    }
}