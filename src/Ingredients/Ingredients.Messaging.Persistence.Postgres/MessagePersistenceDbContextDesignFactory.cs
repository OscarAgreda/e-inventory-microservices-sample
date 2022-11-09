using Ingredients.Messaging.Persistence.Postgres.MessagePersistence;
using Ingredients.Persistence.EfCore.Postgres;

namespace Ingredients.Messaging.Persistence.Postgres;

public class MessagePersistenceDbContextDesignFactory : DbContextDesignFactoryBase<MessagePersistenceDbContext>
{
    public MessagePersistenceDbContextDesignFactory() : base("ConnectionStrings:PostgresMessaging")
    {
    }
}
