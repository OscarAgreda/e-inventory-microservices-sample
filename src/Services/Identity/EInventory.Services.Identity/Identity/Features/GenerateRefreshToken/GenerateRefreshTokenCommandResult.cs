using EInventory.Services.Identity.Identity.Dtos;

namespace EInventory.Services.Identity.Identity.Features.GenerateRefreshToken;

public record GenerateRefreshTokenCommandResult(RefreshTokenDto RefreshToken);
