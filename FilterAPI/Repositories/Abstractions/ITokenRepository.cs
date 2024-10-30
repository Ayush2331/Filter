using Microsoft.AspNetCore.Identity;

namespace FilterAPI.Repositories.Abstractions
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
