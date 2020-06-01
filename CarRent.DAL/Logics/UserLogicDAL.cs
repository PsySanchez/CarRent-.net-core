
using CarRent.DAL.Models;
using CarRent.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRent.DAL.Logics
{
    public class UserLogicDAL
    {
        public async Task<UserEntity> GetUserByEmail(string email)
        {
            using var db = new CarRentContext();
            return await db.Users.Where(user => user.Email == email)
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
        // registration new user, if user exist return null
        public async Task<UserEntity> Registration(UserEntity userEntity)
        {
            if (await GetUserByEmail(userEntity.Email) == null)
            {
                using var db = new CarRentContext();
                Users user = new Users
                {
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    PhoneNumber = userEntity.PhoneNumber,
                    Email = userEntity.Email,
                    Password = userEntity.Password,
                    DateOfBirth = userEntity.DateOfBirth,
                    Role = userEntity.Role,
                };
                await db.AddAsync(user);
                await db.SaveChangesAsync();
                return await GetUserByEmail(user.Email);
            }
            return null;
        }

        public async Task<UserEntity> CustSearch(CustSearchEntity cust)
        {
            if (cust.Email != null)
            {
                return await GetUserByEmail(cust.Email);
            }

            if (cust.PhoneNumber != null)
            {
                using var db = new CarRentContext();
                return await db.Users.Where(user => user.PhoneNumber == cust.PhoneNumber)
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

            return null;
        }
    }
}
