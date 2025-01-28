using CrombieProytecto_V0._2.Models.dtos;
using System.Threading.Tasks;

namespace CrombieProytecto_V0._2.Service
{
    public interface ICognitoAuthService
    {
        Task<string> RegisterUserAsync(RegistroUsuarioDto registroUsuarioDto);
        Task<string> LoginAsync(string username, string password);
        Task<string> ChangePasswordAsync(string email, string oldPassword, string newPassword);
    }
}

