namespace Ingredients.Persistence.EfCore.Postgres;

public class PostgresOptions
{
    public string ConnectionString { get; set; } = null!;
    public bool UseInMemory { get; set; }
}
