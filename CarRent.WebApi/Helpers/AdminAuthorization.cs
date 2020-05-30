using CarRent.BL.Logics;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace CarRent.WebApi.Helpers
{
    public class AdminAuthorization
    {
        private readonly AuthService _authService = new AuthService();
        private readonly UserLogicBL _userLogicBL = new UserLogicBL();

        public async Task<bool> CheckIfAdmin(string accessToken, IConfiguration config)
        {
            var principal = _authService.GetPrincipalFromToken(accessToken, config);
            if (principal == null) return false;
            var email = principal.Identity.Name;
            var user = await _userLogicBL.GetUserByEmail(email);
            if (user.Role != "admin") return false;
            return true;
        }
    }
}
