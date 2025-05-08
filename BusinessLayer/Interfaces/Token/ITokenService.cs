using EntityLayer.Entitys;

namespace BusinessLayer.Interfaces.Token
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
        int GetUserIdFromToken(string token);
    }
}
