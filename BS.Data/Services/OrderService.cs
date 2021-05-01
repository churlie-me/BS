using BS.Core;
using BS.Data.IRepositories;
using BS.Data.IServices;
using BS.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository orderRepository;
        public OrderService(OrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<bool> AddOrUpdate(Order order)
        {
            return await orderRepository.AddOrUpdate(order);
        }

        public async Task<Order> GetOrder(Guid OrderId)
        {
            return await orderRepository.GetOrder(OrderId);
        }
        public async Task<Order> GetAppointmentOrder(Guid applicationId)
        {
            return await orderRepository.GetAppointmentOrder(applicationId);
        }

        public async Task<List<Order>> StoreOrders(Guid StoreId)
        {
            return await orderRepository.StoreOrders(StoreId);
        }

        public async Task<List<Order>> UserOrders(Guid UserId)
        {
            return await orderRepository.UserOrders(UserId);
        }

        public async Task<dboOrder> Orders(dboOrder dboOrder)
        {
            return await orderRepository.Orders(dboOrder);
        }

        public async Task<List<dboBranchRevenue>> DboBranchRevenue(dboReport dboReport)
        {
            return await orderRepository.DboBranchRevenue(dboReport);
        }

        public async Task<List<dboProductRevenue>> DboProductRevenue(dboReport dboReport)
        {
            return await orderRepository.DboProductRevenue(dboReport);
        }

        public async Task<List<dboRevenuePerService>> DboServiceRevenue(dboReport dboReport)
        {
            return await orderRepository.DboServiceRevenue(dboReport);
        }

        public async Task<List<dboRevenuePerService>> DboSeatRevenue(dboServicePerSeatReport dboServicePerSeatReport)
        {
            return await orderRepository.DboSeatRevenue(dboServicePerSeatReport);
        }
    }
}
