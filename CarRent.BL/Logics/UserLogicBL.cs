using CarRent.BL.Helpers;
using CarRent.DAL.Logics;
using CarRent.Entities;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace CarRent.BL.Logics
{
    public class UserLogicBL
    {
        private readonly UserLogicDAL _userLogicDAL = new UserLogicDAL();
        public async Task<UserEntity> GetUserByEmail(string email)
        {
            return await _userLogicDAL.GetUserByEmail(email);
        }
        public async Task<UserEntity> Registration(UserEntity user, IConfiguration config)
        {
            user.Password = Md5Hash.GetMd5Hash(user.Password);
            user.Role = config.GetValue<string>("UserRoleDefault:Role"); // default
            return await _userLogicDAL.Registration(user);
        }
    }
}
