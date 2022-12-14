using Ingredients.Abstractions.CQRS.Commands;
using MassTransit;
using EInventory.Services.Customers.RestockSubscriptions.Features.ProcessingRestockNotification;
using EInventory.Services.Shared.Catalogs.Products.Events.Integration;

namespace EInventory.Services.Customers.Products.Features.ReplenishingProductStock.Events.External;

public class ProductStockReplenishedConsumer : IConsumer<ProductStockReplenished>
{
    private readonly ICommandProcessor _commandProcessor;
    private readonly ILogger<ProductStockReplenishedConsumer> _logger;

    public ProductStockReplenishedConsumer(
        ICommandProcessor commandProcessor,
        ILogger<ProductStockReplenishedConsumer> logger)
    {
        _commandProcessor = commandProcessor;
        _logger = logger;
    }

    // If this handler is called successfully, it will send a ACK to rabbitmq for removing message from the queue and if we have an exception it send an NACK to rabbitmq
    // and with NACK we can retry the message with re-queueing this message to the broker
    public async Task Consume(ConsumeContext<ProductStockReplenished> context)
    {
        var productStockReplenished = context.Message;

        await _commandProcessor.SendAsync(
            new ProcessRestockNotification(productStockReplenished.ProductId, productStockReplenished.NewStock));

        _logger.LogInformation(
            "Sending restock notification command for product {ProductId}",
            productStockReplenished.ProductId);
    }
}
