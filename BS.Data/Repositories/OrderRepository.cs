using BS.Core;
using BS.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private BSDBContext _bsdbContext;
        public OrderRepository(BSDBContext _bsdbContext)
        {
            this._bsdbContext = _bsdbContext;
        }


        public async Task<bool> AddOrUpdate(Order order)
        {
            try
            {
                if (_bsdbContext.Order.AsNoTracking().Any(o => o.Id == order.Id))
                {
                    _bsdbContext.Entry(order).State = EntityState.Modified;

                    foreach (var item in order.OrderItems)
                        if (_bsdbContext.OrderItem.Any(ot => ot.Id == item.Id))
                        {
                            _bsdbContext.Entry(item).State = EntityState.Modified;
                        }
                        else
                            _bsdbContext.Entry(item).State = EntityState.Added;
                }
                else
                {
                    if (order.UserId == Guid.Empty)
                        if (_bsdbContext.User.Include(y => y.Contact).Any(x => x.Contact.Email == order.User.Contact.Email))
                        {
                            order.UserId = _bsdbContext.User.Where(x => x.Contact.Email == order.User.Contact.Email).FirstOrDefaultAsync().Result.Id;
                            order.User = null;
                        }

                    _bsdbContext.Order.Add(order);
                }

                return await _bsdbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Order> GetOrder(Guid orderId)
        {
            return await _bsdbContext.Order.Include(u => u.User)
                                           .Include(o => o.User.Contact)
                                           .Include(o => o.User.Address)
                                           .Include(o => o.OrderItems.Where(ot => !ot.Deleted)).FirstOrDefaultAsync(o => o.Id == orderId);
        }
        
        public async Task<Order> GetAppointmentOrder(Guid appointmentId)
        {
            return await _bsdbContext.Order.Include(u => u.User)
                                           .Include(o => o.User.Contact)
                                           .Include(o => o.User.Address)
                                           .Include(o => o.OrderItems.Where(ot => !ot.Deleted)).FirstOrDefaultAsync(o => o.AppointmentId == appointmentId);
        }

        public async Task<List<Order>> StoreOrders(Guid StoreId)
        {
            return await _bsdbContext.Order.Include(o => o.User)
                                           .Include(o => o.User.Contact)
                                           .Include(o => o.User.Address)
                                           .Include(o => o.OrderItems)
                                           .ThenInclude(o => o.Article)
                                           .ThenInclude(a => a.SaleItem)
                                           .Where(o => o.OrderType == OrderType.Product).ToListAsync();
        }

        public async Task<List<Order>> UserOrders(Guid UserId)
        {
            return await _bsdbContext.Order.Include(o => o.User)
                                           .Include(o => o.User.Contact)
                                           .Include(o => o.User.Address)
                                           .Include(o => o.OrderItems)
                                           .ThenInclude(o => o.Article)
                                           .ThenInclude(a => a.SaleItem)
                                           .Where(o => o.UserId == UserId && o.OrderType == OrderType.Product).ToListAsync();
        }

        public async Task<dboOrder> Orders(dboOrder dboOrder)
        {
            try
            {
                if (dboOrder.from != DateTime.MinValue && dboOrder.to != DateTime.MinValue && dboOrder.UserId != Guid.Empty && dboOrder.BranchId != Guid.Empty)
                {
                    if (dboOrder.Status == OrderStatus.All)
                    {
                        dboOrder.Orders = await _bsdbContext.Order.Include(u => u.User)
                                                        .Include(u => u.User.Contact)
                                                        .Include(s => s.OrderItems)
                                                        .ThenInclude(b => b.Article)
                                                        .Include(b => b.Branch)
                                                        .Where(x => x.OrderDate >= dboOrder.from && x.OrderDate <= dboOrder.to
                                                                                                            && x.UserId == dboOrder.UserId
                                                                                                            && x.BranchId == dboOrder.BranchId)
                                                        .OrderByDescending(d => d.OrderDate)
                                                        .Skip(dboOrder.pageSize * (dboOrder.page - 1))
                                                        .Take(dboOrder.pageSize)
                                                        .ToListAsync();
                        dboOrder.pageCount = (int)Math.Ceiling((double)_bsdbContext.Order
                                                            .Where(x => x.OrderDate >= dboOrder.from && x.OrderDate <= dboOrder.to
                                                                                    && x.UserId == dboOrder.UserId
                                                                                    && x.BranchId == dboOrder.BranchId)
                                                            .Count() / dboOrder.pageSize);
                    }
                    else
                    {
                        dboOrder.Orders = await _bsdbContext.Order.Include(u => u.User)
                                                        .Include(u => u.User.Contact)
                                                        .Include(s => s.OrderItems)
                                                        .ThenInclude(b => b.Article)
                                                        .Include(b => b.Branch)
                                                        .Where(x => x.OrderDate >= dboOrder.from && x.OrderDate <= dboOrder.to
                                                                                                            && x.UserId == dboOrder.UserId
                                                                                                            && x.BranchId == dboOrder.BranchId
                                                                                                            && x.Status == dboOrder.Status)
                                                        .OrderByDescending(d => d.OrderDate)
                                                        .Skip(dboOrder.pageSize * (dboOrder.page - 1))
                                                        .Take(dboOrder.pageSize)
                                                        .ToListAsync();
                        dboOrder.pageCount = (int)Math.Ceiling((double)_bsdbContext.Order
                                                            .Where(x => x.OrderDate >= dboOrder.from && x.OrderDate <= dboOrder.to
                                                                                    && x.UserId == dboOrder.UserId
                                                                                    && x.BranchId == dboOrder.BranchId
                                                                                    && x.Status == dboOrder.Status)
                                                            .Count() / dboOrder.pageSize);
                    }
                }
                else if (dboOrder.from != DateTime.MinValue && dboOrder.to != DateTime.MinValue && (dboOrder.UserId != Guid.Empty || dboOrder.BranchId != Guid.Empty))
                {
                    if (dboOrder.Status == OrderStatus.All)
                    {
                        dboOrder.Orders = await _bsdbContext.Order.Include(u => u.User)
                                                                                .Include(u => u.User.Contact)
                                                                                .Include(s => s.OrderItems)
                                                                                .ThenInclude(b => b.Article)
                                                                                .Include(b => b.Branch)
                                                                                .Where(x => x.OrderDate >= dboOrder.from && x.OrderDate <= dboOrder.to
                                                                                                        && (x.UserId == dboOrder.UserId
                                                                                                        || x.BranchId == dboOrder.BranchId))
                                                                                .OrderByDescending(d => d.OrderDate)
                                                                                .Skip(dboOrder.pageSize * (dboOrder.page - 1))
                                                                                .Take(dboOrder.pageSize)
                                                                                .ToListAsync();
                        dboOrder.pageCount = (int)Math.Ceiling((double)_bsdbContext.Order
                                                            .Where(x => x.OrderDate >= dboOrder.from && x.OrderDate <= dboOrder.to
                                                                                                        && (x.UserId == dboOrder.UserId
                                                                                                        || x.BranchId == dboOrder.BranchId)).Count() / dboOrder.pageSize);
                    }
                    else
                    {
                        dboOrder.Orders = await _bsdbContext.Order.Include(u => u.User)
                                                                               .Include(u => u.User.Contact)
                                                                               .Include(s => s.OrderItems)
                                                                               .ThenInclude(b => b.Article)
                                                                               .Include(b => b.Branch)
                                                                               .Where(x => x.OrderDate >= dboOrder.from && x.OrderDate <= dboOrder.to
                                                                                                       && (x.UserId == dboOrder.UserId
                                                                                                       || x.BranchId == dboOrder.BranchId)
                                                                                                       && x.Status == dboOrder.Status)
                                                                               .OrderByDescending(d => d.OrderDate)
                                                                               .Skip(dboOrder.pageSize * (dboOrder.page - 1))
                                                                               .Take(dboOrder.pageSize)
                                                                               .ToListAsync();
                        dboOrder.pageCount = (int)Math.Ceiling((double)_bsdbContext.Order
                                                            .Where(x => x.OrderDate >= dboOrder.from && x.OrderDate <= dboOrder.to
                                                                                                        && (x.UserId == dboOrder.UserId
                                                                                                        || x.BranchId == dboOrder.BranchId)
                                                                                                        && x.Status == dboOrder.Status).Count() / dboOrder.pageSize);
                    }
                }
                else if (dboOrder.from != DateTime.MinValue && dboOrder.to != DateTime.MinValue)
                {
                    if (dboOrder.Status == OrderStatus.All)
                    {
                        dboOrder.Orders = await _bsdbContext.Order.Include(u => u.User)
                                                                .Include(u => u.User.Contact)
                                                                .Include(s => s.OrderItems)
                                                                .ThenInclude(b => b.Article)
                                                                .Include(b => b.Branch)
                                                                .Where(x => x.OrderDate >= dboOrder.from && x.OrderDate <= dboOrder.to)
                                                                .OrderByDescending(d => d.OrderDate)
                                                                .Skip(dboOrder.pageSize * (dboOrder.page - 1))
                                                                .Take(dboOrder.pageSize)
                                                                .ToListAsync();
                        dboOrder.pageCount = (int)Math.Ceiling((double)_bsdbContext.Order
                                                            .Where(x => x.OrderDate >= dboOrder.from && x.OrderDate <= dboOrder.to).Count() / dboOrder.pageSize);
                    }
                    else
                    {
                        dboOrder.Orders = await _bsdbContext.Order.Include(u => u.User)
                                                                .Include(u => u.User.Contact)
                                                                .Include(s => s.OrderItems)
                                                                .ThenInclude(b => b.Article)
                                                                .Include(b => b.Branch)
                                                                .Where(x => x.OrderDate >= dboOrder.from && x.OrderDate <= dboOrder.to && x.Status == dboOrder.Status)
                                                                .OrderByDescending(d => d.OrderDate)
                                                                .Skip(dboOrder.pageSize * (dboOrder.page - 1))
                                                                .Take(dboOrder.pageSize)
                                                                .ToListAsync();
                        dboOrder.pageCount = (int)Math.Ceiling((double)_bsdbContext.Order
                                                            .Where(x => x.OrderDate >= dboOrder.from && x.OrderDate <= dboOrder.to && x.Status == dboOrder.Status).Count() / dboOrder.pageSize);
                    }
                }
                else if (dboOrder.UserId != Guid.Empty || dboOrder.BranchId != Guid.Empty)
                {
                    if (dboOrder.Status == OrderStatus.All)
                    {
                        dboOrder.Orders = await _bsdbContext.Order.Include(u => u.User)
                                                                            .Include(u => u.User.Contact)
                                                                            .Include(s => s.OrderItems)
                                                                            .ThenInclude(b => b.Article)
                                                                            .Include(b => b.Branch)
                                                                            .Where(x => x.UserId == dboOrder.UserId
                                                                            || x.BranchId == dboOrder.BranchId)
                                                                            .OrderByDescending(d => d.OrderDate)
                                                                            .Skip(dboOrder.pageSize * (dboOrder.page - 1))
                                                                            .Take(dboOrder.pageSize)
                                                                            .ToListAsync();
                        dboOrder.pageCount = (int)Math.Ceiling((double)_bsdbContext.Order
                                                            .Where(x => x.UserId == dboOrder.UserId
                                                                     || x.BranchId == dboOrder.BranchId).Count() / dboOrder.pageSize);
                    }
                    else
                    {
                        dboOrder.Orders = await _bsdbContext.Order.Include(u => u.User)
                                                                            .Include(u => u.User.Contact)
                                                                            .Include(s => s.OrderItems)
                                                                            .ThenInclude(b => b.Article)
                                                                            .Include(b => b.Branch)
                                                                            .Where(x => x.UserId == dboOrder.UserId
                                                                            || x.BranchId == dboOrder.BranchId
                                                                            && x.Status == dboOrder.Status)
                                                                            .OrderByDescending(d => d.OrderDate)
                                                                            .Skip(dboOrder.pageSize * (dboOrder.page - 1))
                                                                            .Take(dboOrder.pageSize)
                                                                            .ToListAsync();
                        dboOrder.pageCount = (int)Math.Ceiling((double)_bsdbContext.Order
                                                            .Where(x => x.UserId == dboOrder.UserId
                                                                     || x.BranchId == dboOrder.BranchId
                                                                     && x.Status == dboOrder.Status).Count() / dboOrder.pageSize);
                    }
                }
                else
                {
                    if (dboOrder.Status == OrderStatus.All)
                    {
                        dboOrder.Orders = await _bsdbContext.Order.Include(u => u.User)
                                                                            .Include(u => u.User.Contact)
                                                                            .Include(s => s.OrderItems)
                                                                            .ThenInclude(b => b.Article)
                                                                            .Include(b => b.Branch)
                                                                            .OrderByDescending(d => d.OrderDate)
                                                                            .Skip(dboOrder.pageSize * (dboOrder.page - 1))
                                                                            .Take(dboOrder.pageSize)
                                                                            .ToListAsync();
                        dboOrder.pageCount = (int)Math.Ceiling((double)_bsdbContext.Order.Count() / dboOrder.pageSize);
                    }
                    else
                    {
                        dboOrder.Orders = await _bsdbContext.Order.Include(u => u.User)
                                                                            .Include(u => u.User.Contact)
                                                                            .Include(s => s.OrderItems)
                                                                            .ThenInclude(b => b.Article)
                                                                            .Include(b => b.Branch)
                                                                            .Where(x => x.Status == dboOrder.Status)
                                                                            .OrderByDescending(d => d.OrderDate)
                                                                            .Skip(dboOrder.pageSize * (dboOrder.page - 1))
                                                                            .Take(dboOrder.pageSize)
                                                                            .ToListAsync();
                        dboOrder.pageCount = (int)Math.Ceiling((double)_bsdbContext.Order.Where(x => x.Status == dboOrder.Status).Count() / dboOrder.pageSize);
                    }
                }

                return dboOrder;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<dboProductRevenue>> DboProductRevenue(dboReport dboReport)
        {
            try
            {
                var re = await _bsdbContext.Order.Include(o => o.OrderItems)
                                               .ThenInclude(o => o.Article)
                                               .Where(o => dboReport.From <= o.OrderDate && dboReport.To >= o.OrderDate)
                                               .SelectMany(o => o.OrderItems.Where(oi => oi.ArticleId != null))
                                               .Select(orderItem => new
                                               {
                                                   Article = orderItem.Article,
                                                   ArticleId = orderItem.ArticleId,
                                                   SubTotal = (orderItem.Price * orderItem.Quantity),
                                                   Quantity = orderItem.Quantity
                                               })
                                               .ToListAsync();


                return re.GroupBy(x => x.ArticleId).Select(x => new dboProductRevenue
                {
                    Article = x.First().Article,
                    Quantity = x.Sum(x => x.Quantity),
                    Revenue = x.Sum(x => x.SubTotal)
                })
                .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<dboBranchRevenue>> DboBranchRevenue(dboReport dboReport)
        {
            try
            {
                var results = await _bsdbContext.Order.Include(o => o.Branch)
                                               .Where(o => dboReport.From <= o.OrderDate && o.OrderDate <= dboReport.To && o.BranchId != null)
                                               .ToListAsync();

                var after_results = results.GroupBy(o => o.BranchId).Select(x => new dboBranchRevenue
                {
                    Branch = x.First().Branch,
                    Revenue = x.Sum(x => x.OrderTotal)
                }).ToList();

                //Add branches without revenue
                var _branches = _bsdbContext.Branch.Where(b => !b.Deleted).ToList();
                _branches.ForEach(b =>
                {
                    if (!after_results.Any(af => af.Branch.Id == b.Id))
                        after_results.Add(new dboBranchRevenue
                        {
                            Branch = b,
                            Revenue = 0
                        });
                });


                return after_results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<dboRevenuePerService>> DboSeatRevenue(dboServicePerSeatReport dboServicePerSeatReport)
        {
            try
            {
                var    results = await _bsdbContext.Order.Include(u => u.Appointment)
                                                .ThenInclude(s => s.Seat)
                                                .Include(o => o.OrderItems)
                                               .ThenInclude(o => o.Service)
                                               .Where(o => dboServicePerSeatReport.From <= o.OrderDate && o.OrderDate <= dboServicePerSeatReport.To && o.OrderType == OrderType.Service && o.Appointment.SeatId == dboServicePerSeatReport.SeatId)
                                               .SelectMany(o => o.OrderItems.Where(oi => oi.ServiceId != null))
                                               .Select(orderItem => new
                                               {
                                                   Service = orderItem.Service,
                                                   ServiceId = orderItem.ServiceId,
                                                   Appointments = orderItem.Quantity,
                                                   SubTotal = (orderItem.Price * orderItem.Quantity),
                                               })
                                               .ToListAsync();

                return results.GroupBy(s => s.ServiceId)
                                                .Select(x => new dboRevenuePerService
                                                {
                                                    Service = x.First().Service,
                                                    NoOfAppointments = (int)x.Sum(y => y.Appointments),
                                                    Revenue = x.Sum(y => y.SubTotal)
                                                }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<dboRevenuePerService>> DboServiceRevenue(dboReport dboReport)
        {
            try
            {
                var re = await _bsdbContext.Order.Include(o => o.OrderItems)
                                               .ThenInclude(o => o.Service)
                                               .Where(o => dboReport.From <= o.OrderDate && dboReport.To >= o.OrderDate)
                                               .SelectMany(o => o.OrderItems.Where(oi => oi.ServiceId != null))
                                               .Select(orderItem => new
                                               {
                                                   Service = orderItem.Service,
                                                   ServiceId = orderItem.ServiceId,
                                                   Appointments = orderItem.Quantity,
                                                   SubTotal = (orderItem.Price * orderItem.Quantity),
                                               })
                                               .ToListAsync();


                return re.GroupBy(x => x.ServiceId).Select(x => new dboRevenuePerService
                {
                    Service = x.First().Service,
                    NoOfAppointments = (int)x.Sum( y => y.Appointments),
                    Revenue = x.Sum(y => y.SubTotal)
                })
                .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}