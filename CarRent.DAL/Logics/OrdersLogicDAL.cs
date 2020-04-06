using CarRent.DAL.Models;
using CarRent.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRent.DAL.Logics
{
    public class OrdersLogicDAL
    {
        public async Task<OrderEntity> GetUserOrder(int id)
        {
            using var db = new CarRentContext();
            return await db.Orders
              .Where(order => order.Id == id)
              .Select(order => new OrderEntity
              {
                  Id = order.Id,
                  UserId = order.UserId,
                  CarId = order.CarId,
                  FromDate = order.FromDate,
                  ToDate = order.ToDate,
                  TotalCost = order.TotalCost
              }).FirstOrDefaultAsync();
        }
        public async void NewUserOrder(OrderEntity orderModel)
        {
            using var db = new CarRentContext();
            Orders order = new Orders
            {
                UserId = orderModel.UserId,
                CarId = orderModel.CarId,
                FromDate = orderModel.FromDate,
                ToDate = orderModel.ToDate,
                TotalCost = orderModel.TotalCost
            };
            db.Orders.Add(order);
            await db.SaveChangesAsync();
        }
        public async Task<List<OrderEntity>> GetUserOrdersHistory(int id)
        {
            using var db = new CarRentContext();
            return await db.Orders.Where(order => order.UserId == id)
                .Select(order => new OrderEntity
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    CarId = order.CarId,
                    FromDate = order.FromDate,
                    ToDate = order.ToDate,
                    TotalCost = order.TotalCost
                }).ToListAsync();
        }
    }
}
