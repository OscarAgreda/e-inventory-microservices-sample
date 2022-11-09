using EInventory.Services.Identity.Shared.Models;

namespace EInventory.Services.Identity.Users.Features.UpdatingUserState;

public record UpdateUserStateRequest
{
    public UserState UserState { get; init; }
}
