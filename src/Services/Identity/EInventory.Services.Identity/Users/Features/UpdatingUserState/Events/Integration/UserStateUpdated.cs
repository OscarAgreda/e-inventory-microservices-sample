using Ingredients.Core.Messaging;
using EInventory.Services.Identity.Shared.Models;

namespace EInventory.Services.Identity.Users.Features.UpdatingUserState.Events.Integration;

public record UserStateUpdated(Guid UserId, UserState OldUserState, UserState NewUserState) : IntegrationEvent;
