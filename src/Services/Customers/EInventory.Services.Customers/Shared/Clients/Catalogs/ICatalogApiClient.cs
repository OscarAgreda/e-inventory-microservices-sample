using EInventory.Services.Customers.Shared.Clients.Catalogs.Dtos;

namespace EInventory.Services.Customers.Shared.Clients.Catalogs;

public interface ICatalogApiClient
{
    Task<GetProductByIdResponse?> GetProductByIdAsync(long id, CancellationToken cancellationToken = default);
}
