using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IServices
{
    public interface IOrderService
    {
        Task<bool> AddOrUpdate(Order order);
        Task<List<Order>> StoreOrders(Guid StoreId);
        Task<List<Order>> UserOrders(Guid UserId);
        Task<Order> GetOrder(Guid OrderId);
        Task<dboOrder> Orders(dboOrder dboOrder);
        Task<List<dboBranchRevenue>> DboBranchRevenue(dboReport dboReport);
        Task<List<dboProductRevenue>> DboProductRevenue(dboReport dboReport);
        Task<List<dboRevenuePerService>> DboServiceRevenue(dboReport dboReport);
        Task<List<dboRevenuePerService>> DboSeatRevenue(dboServicePerSeatReport dboServicePerSeatReport);
        Task<Order> GetAppointmentOrder(Guid applicationId);
    }
}
