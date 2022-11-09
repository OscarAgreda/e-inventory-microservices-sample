using Ingredients.Core.Exception.Types;
using EInventory.Services.Identity.Shared.Models;

namespace EInventory.Services.Identity.Identity.Features.RefreshingToken;

public class InvalidRefreshTokenException : BadRequestException
{
    public InvalidRefreshTokenException(RefreshToken? refreshToken) : base($"refresh token {refreshToken?.Token} is invalid!")
    {
    }
}
