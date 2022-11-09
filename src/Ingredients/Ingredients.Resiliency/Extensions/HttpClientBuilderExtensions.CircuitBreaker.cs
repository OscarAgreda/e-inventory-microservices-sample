using Ardalis.GuardClauses;
using Ingredients.Core.Extensions;
using Ingredients.Resiliency.CircuitBreaker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Ingredients.Resiliency.Extensions;

public static partial class HttpClientBuilderExtensions
{
    public static IHttpClientBuilder AddCircuitBreakerHandler(
        this IHttpClientBuilder httpClientBuilder)
    {
        return httpClientBuilder.AddPolicyHandler((sp, _) =>
        {
            var options = sp.GetRequiredService<IConfiguration>().GetOptions<PolicyOptions>(nameof(PolicyOptions));

            Guard.Against.Null(options, nameof(options));

            var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
            var circuitBreakerLogger = loggerFactory.CreateLogger("PollyHttpCircuitBreakerPoliciesLogger");

            return HttpCircuitBreakerPolicies.GetHttpCircuitBreakerPolicy(
                circuitBreakerLogger,
                options);
        });
    }
}
