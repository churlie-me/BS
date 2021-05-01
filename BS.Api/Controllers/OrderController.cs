using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BS.Core;
using BS.Data;
using BS.Data.IRepositories;
using BS.Data.IServices;
using BS.Data.Repositories;
using BS.Data.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;
        public OrderController(BSDBContext _bsdbContext)
        {
            _orderService = new OrderService(new OrderRepository(_bsdbContext));
        }

        [HttpGet("store/{storeId}")]
        public async Task<List<Order>> GetStoreOrders(string storeId)
        {
            try
            {
                return await _orderService.StoreOrders(Guid.Parse(storeId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("filter")]
        public async Task<dboOrder> Orders(dboOrder dboOrder)
        {
            try
            {
                return await _orderService.Orders(dboOrder);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("customer/{userId}")]
        public async Task<List<Order>> GetCustomerOrders(string userId)
        {
            try
            {
                return await _orderService.UserOrders(Guid.Parse(userId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{orderId}")]
        public async Task<Order> Get(string orderId)
        {
            try
            {
                return await _orderService.GetOrder(Guid.Parse(orderId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("appointment/{applicationId}")]
        public async Task<Order> GetAppointmentOrder(Guid applicationId)
        {
            try
            {
                return await _orderService.GetAppointmentOrder(applicationId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<bool> Post([FromBody] Order _order)
        {
            return await _orderService.AddOrUpdate(_order);
        }

        [HttpPost("report/branch")]
        public async Task<List<dboBranchRevenue>> DboBranchRevenue(dboReport dboReport)
        {
            return await _orderService.DboBranchRevenue(dboReport);
        }

        [HttpPost("report/product")]
        public async Task<List<dboProductRevenue>> DboProductRevenue(dboReport dboReport)
        {
            return await _orderService.DboProductRevenue(dboReport);
        }

        [HttpPost("report/service")]
        public async Task<List<dboRevenuePerService>> DboServiceRevenue(dboReport dboReport)
        {
            return await _orderService.DboServiceRevenue(dboReport);
        }

        [HttpPost("report/seat")]
        public async Task<List<dboRevenuePerService>> DboSeatRevenue(dboServicePerSeatReport dboServicePerSeatReport)
        {
            return await _orderService.DboSeatRevenue(dboServicePerSeatReport);
        }
    }
}
