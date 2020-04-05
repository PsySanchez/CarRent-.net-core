using CarRent.BL.Helpers;
using CarRent.DAL.Logics;
using CarRent.Entities;
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
        public async Task<UserEntity> Registration(UserEntity user)
        {
            user.Password = Md5Hash.GetMd5Hash(user.Password);
            user.Role = "user"; // default
            return await _userLogicDAL.Registration(user);
        }
    }
}
