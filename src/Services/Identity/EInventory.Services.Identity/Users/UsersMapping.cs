using AutoMapper;
using EInventory.Services.Identity.Shared.Models;
using EInventory.Services.Identity.Users.Dtos;

namespace EInventory.Services.Identity.Users;

public class UsersMapping : Profile
{
    public UsersMapping()
    {
        CreateMap<ApplicationUser, IdentityUserDto>()
            .ForMember(x => x.RefreshTokens, opt => opt.MapFrom(x => x.RefreshTokens.Select(r => r.Token)));
    }
}
