using System.Threading.Tasks;
using ArchitectureProject.Domain.Dto;

namespace ArchitectureProject.Infrastructure.Interfaces
{
    public interface  IAccountService
    {
        Task<LoggedTokenDto> Login(LoginUserDto model);
        Task Register(RegisterUserDto model);

        Task<string> GetAccessToken(string refreshTokenKey, int userId);

        Task RevokeRefreshToken(string refreshTokenKey, int userId);
    }
}
