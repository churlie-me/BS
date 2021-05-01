using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IRepositories
{
    interface IOrderRepository
    {
        Task<bool> AddOrUpdate(Order order);
        Task<List<Order>> StoreOrders(Guid StoreId);
        Task<List<Order>> UserOrders(Guid UserId);
        Task<Order> GetOrder(Guid orderId);
        Task<dboOrder> Orders(dboOrder dboOrder);
        Task<List<dboBranchRevenue>> DboBranchRevenue(dboReport dboReport);
        Task<List<dboProductRevenue>> DboProductRevenue(dboReport dboReport);
        Task<List<dboRevenuePerService>> DboServiceRevenue(dboReport dboReport);
        Task<List<dboRevenuePerService>> DboSeatRevenue(dboServicePerSeatReport dboServicePerSeatReport);
        Task<Order> GetAppointmentOrder(Guid applicationId);
    }
}
