using CarRent.DAL.Logics;
using CarRent.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRent.BL.Logics
{
    public class OrdersLogicBL
    {
        private readonly OrdersLogicDAL _ordersLogicDAL = new OrdersLogicDAL();
        public void AddUserOrder(OrderEntity orderEntity)
        {
            if (orderEntity.FromDate < orderEntity.ToDate)
            {
                _ordersLogicDAL.NewUserOrder(orderEntity);
            }
        }

        public async Task<List<OrderEntity>> GetUserOrdersHistory(int id)
        {
            return await _ordersLogicDAL.GetUserOrdersHistory(id);
        }

    }
}
