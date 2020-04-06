using CarRent.DAL.Logics;
using CarRent.Entities;

namespace CarRent.BL.Logics
{
    public class OrdersLogicBL
    {
        private readonly OrdersLogicDAL _ordersLogicDAL = new OrdersLogicDAL();
        public void AddUserOrder(OrderEntity orderEntity)
        {
            _ordersLogicDAL.NewUserOrder(orderEntity);
        }

    }
}
