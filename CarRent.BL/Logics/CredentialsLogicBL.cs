using CarRent.BL.Helpers;
using CarRent.DAL.Logics;
using CarRent.Entities;
using System;
using System.Threading.Tasks;

namespace CarRent.BL.Logics
{
    public class CredentialsLogicBL
    {
        private readonly CredentialsLogicDAL _credentialsLogicDAL = new CredentialsLogicDAL();
        public async Task<UserEntity> Authentication(CredentialsEntity credentials)
        {
            credentials.Password = Md5Hash.GetMd5Hash(credentials.Password + credentials.Email);
            return await _credentialsLogicDAL.Authentication(credentials);
        }
        public async void SaveRefreshToken(string refreshToken, UserEntity user, int lifeTimeExpireNum)
        {
            var lifeTime = DateTime.UtcNow.Add(TimeSpan.FromMinutes(lifeTimeExpireNum));
            var token = new TokenEntity
            {
                UserId = (int)user.Id,
                Token = refreshToken,
                LifeTime = lifeTime
            };
            await _credentialsLogicDAL.SaveRefreshToken(token);
        }
        public async Task<bool> CompareUserToken(string token, int userId)
        {
            return await _credentialsLogicDAL.CompareUserToken(token, userId);
        }
    }
}
