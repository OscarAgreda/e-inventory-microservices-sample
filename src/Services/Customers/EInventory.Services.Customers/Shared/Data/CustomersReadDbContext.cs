using Ingredients.Persistence.Mongo;
using Humanizer;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using EInventory.Services.Customers.Customers.Models.Reads;
using EInventory.Services.Customers.RestockSubscriptions.Models.Read;

namespace EInventory.Services.Customers.Shared.Data;

public class CustomersReadDbContext : MongoDbContext
{
    public CustomersReadDbContext(IOptions<MongoOptions> options) : base(options.Value)
    {
        RestockSubscriptions = GetCollection<RestockSubscriptionReadModel>(nameof(RestockSubscriptions).Underscore());
        Customers = GetCollection<CustomerReadModel>(nameof(Customers).Underscore());
    }

    public IMongoCollection<RestockSubscriptionReadModel> RestockSubscriptions { get; }
    public IMongoCollection<CustomerReadModel> Customers { get; }
}
