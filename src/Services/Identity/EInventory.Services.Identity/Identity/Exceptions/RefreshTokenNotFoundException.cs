using Ingredients.Core.Exception.Types;
using EInventory.Services.Identity.Shared.Models;

namespace EInventory.Services.Identity.Identity.Exceptions;

public class RefreshTokenNotFoundException : NotFoundException
{
    public RefreshTokenNotFoundException(RefreshToken? refreshToken) : base("Refresh token not found.")
    {
    }
}
