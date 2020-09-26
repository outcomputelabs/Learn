using Learn.Core.RandomGenerators;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RandomGeneratorDependencyInjectionExtensions
    {
        public static IServiceCollection AddRandomGenerator(this IServiceCollection services)
        {
            return services.AddSingleton<IRandomGenerator, RandomGenerator>();
        }
    }
}