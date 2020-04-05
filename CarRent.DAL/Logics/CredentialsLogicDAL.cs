using CarRent.DAL.Models;
using CarRent.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarRent.DAL.Logics
{
    public class CredentialsLogicDAL
    {
        public async Task<UserEntity> Authentication(CredentialsEntity credentials)
        {
            using var db = new CarRentContext();
            return await db.Users.Where(user => user.Email == credentials.Email && user.Password == credentials.Password)
                    .Select(user => new UserEntity
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
                        DateOfBirth = user.DateOfBirth,
                        Role = user.Role
                    }).FirstOrDefaultAsync();

        }

        public async Task<TokenEntity> SaveRefreshToken(TokenEntity tokenEntity)
        {
            using var db = new CarRentContext();
            var token = await db.Tokens.FirstOrDefaultAsync(token => token.UserId == tokenEntity.UserId);
            if (token != null)
            {
                token.Token = tokenEntity.Token;
                token.LifeTime = tokenEntity.LifeTime;
                token.UserId = tokenEntity.UserId;
                await db.SaveChangesAsync();
                return await GetTokenById(token.Id);
            }
            else
            {
                var tokenId = await AddNewToken(tokenEntity);
                return await GetTokenById(tokenId);
            }
        }

        public async Task<int> AddNewToken(TokenEntity tokenEntity)
        {
            using var db = new CarRentContext();
            Tokens token = new Tokens
            {
                Token = tokenEntity.Token,
                LifeTime = tokenEntity.LifeTime,
                UserId = tokenEntity.UserId
            };
            await db.Tokens.AddAsync(token);
            await db.SaveChangesAsync();
            return token.Id;
        }
        public async Task<bool> CompareUserToken(string refreshToken, int userId)
        {
            var token = await GetTokenByUserId(userId);
            if (token != null)
            {
                if (token.Token == refreshToken && token.LifeTime > DateTime.UtcNow)
                {
                    return true;
                }
                ClearToken((int)token.Id);
            }
            return false;
        }
        public async Task<TokenEntity> GetTokenByUserId(int userId)
        {
            using var db = new CarRentContext();
            return await db.Tokens.Where(token => token.UserId== userId)
                .Select(token => new TokenEntity
                {
                    Id = token.Id,
                    Token = token.Token,
                    LifeTime = (DateTime)token.LifeTime,
                    UserId = token.UserId
                }).FirstOrDefaultAsync();
        }
        public async Task<TokenEntity> GetTokenById(int id)
        {
            using var db = new CarRentContext();
            return await db.Tokens.Where(token => token.Id == id)
                .Select(token => new TokenEntity
                {
                    Id = token.Id,
                    Token = token.Token,
                    LifeTime = (DateTime)token.LifeTime,
                    UserId = token.UserId
                }).FirstOrDefaultAsync();
        }
        public async void ClearToken(int id)
        {
            using var db = new CarRentContext();
            var token = await db.Tokens.FirstOrDefaultAsync(token => token.Id == id);
            token.Token = null;
            token.LifeTime = null;
            await db.SaveChangesAsync();
        }
    }
}
