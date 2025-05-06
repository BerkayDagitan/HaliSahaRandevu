using EntityLayer.Entitys;
using System.IdentityModel.Tokens.Jwt;

namespace BusinessLayer.Interfaces.Tokens
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> CreateToken(User user, IList<string> roles);


    }
}
