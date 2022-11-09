using Ingredients.Messaging.Persistence.Postgres.Extensions;

namespace EInventory.Services.Customers.Shared.Extensions.ApplicationBuilderExtensions;

public static partial class ApplicationBuilderExtensions
{
    public static async Task UseInfrastructure(this IApplicationBuilder app, ILogger logger)
    {
        await app.UsePostgresPersistenceMessage(logger);
    }
}
