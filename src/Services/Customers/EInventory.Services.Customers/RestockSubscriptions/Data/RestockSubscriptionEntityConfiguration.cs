using Ingredients.Core.Domain.ValueObjects;
using Ingredients.Core.Persistence.EfCore;
using EInventory.Services.Customers.Customers.Models;
using EInventory.Services.Customers.RestockSubscriptions.Models.Write;
using EInventory.Services.Customers.Shared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EInventory.Services.Customers.RestockSubscriptions.Data;

public class RestockSubscriptionEntityConfiguration : IEntityTypeConfiguration<RestockSubscription>
{
    public void Configure(EntityTypeBuilder<RestockSubscription> builder)
    {
        builder.ToTable("restock_subscriptions", CustomersDbContext.DefaultSchema);

        builder.HasKey(c => c.Id);
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, id => id);

        builder.Property(x => x.Processed).HasDefaultValue(false);

        builder.Property(c => c.CustomerId)
            .HasConversion(id => id.Value, id => id);

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(x => x.CustomerId);

        builder.OwnsOne(x => x.ProductInformation, p =>
        {
            p.Property(x => x.Id)
                .HasConversion(id => id.Value, id => id);

            p.Property(x => x.Name)
                .HasMaxLength(EfConstants.Lenght.Normal);
        });

        builder.Property(x => x.Email)
            .HasConversion(email => email.Value, email => Email.Create(email));

        builder.Property(x => x.Created).HasDefaultValueSql(EfConstants.DateAlgorithm);
    }
}
