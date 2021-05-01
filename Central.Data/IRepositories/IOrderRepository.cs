using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Central.Data.IRepositories
{
    interface IOrderRepository
    {
        Task<bool> AddOrUpdate(Order order);
        Task<Order> GetOrder(Guid orderId);
    }
}
