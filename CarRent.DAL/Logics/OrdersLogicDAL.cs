using CarRent.DAL.Models;
using CarRent.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CarRent.DAL.Logics
{
    public class OrdersLogicDAL
    {
        public async Task<OrderEntity> GetOrder(int id)
        {
            using var db = new CarRentContext();
            return await db.Orders
              .Where(o => o.Id == id)
              .Select(o => new OrderEntity
              {
                  UserId = o.UserId,
                  CarId = o.CarId,
                  FromDate = o.FromDate,
                  ToDate = o.ToDate,
                  TotalCost = o.TotalCost
              }).FirstOrDefaultAsync();
        }
        public async Task<OrderEntity> AddOrder(OrderEntity orderModel)
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
            db.SaveChanges();
            return await GetOrder(order.Id);
        }
    }
}
