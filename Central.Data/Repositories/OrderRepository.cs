using BS.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Central.Data.IRepositories;

namespace Central.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        #region Private fields & constructor
        private readonly CDBContext _cdbContext;
        #endregion

        public OrderRepository(CDBContext _cdbContext)
        {
            this._cdbContext = _cdbContext;
        }

        public async Task<bool> AddOrUpdate(Order order)
        {
            try
            {
                if (_cdbContext.Order.AsNoTracking().Any(o => o.Id == order.Id))
                {
                    _cdbContext.Entry(order).State = EntityState.Modified;

                    foreach (var item in order.OrderItems)
                        if (_cdbContext.OrderItem.Any(ot => ot.Id == item.Id))
                        {
                            _cdbContext.Entry(item).State = EntityState.Modified;
                        }
                        else
                            _cdbContext.Entry(item).State = EntityState.Added;
                }
                else
                {
                    if (order.UserId == Guid.Empty)
                        if (_cdbContext.User.Include(y => y.Contact).Any(x => x.Contact.Email == order.User.Contact.Email))
                        {
                            order.UserId = _cdbContext.User.Where(x => x.Contact.Email == order.User.Contact.Email).FirstOrDefaultAsync().Result.Id;
                            order.User = null;
                        }

                    _cdbContext.Order.Add(order);
                }

                return await _cdbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Order> GetOrder(Guid orderId)
        {
            return await _cdbContext.Order.Include(u => u.User)
                                           .Include(o => o.User.Contact)
                                           .Include(o => o.User.Address)
                                           .Include(o => o.OrderItems.Where(ot => !ot.Deleted)).FirstOrDefaultAsync(o => o.Id == orderId);
        }


        public async Task<dboOrder> Orders(dboOrder dboOrder)
        {
            try
            {
                if (dboOrder.from != DateTime.MinValue && dboOrder.to != DateTime.MinValue && dboOrder.UserId != Guid.Empty && dboOrder.BranchId != Guid.Empty)
                {
                    if (dboOrder.Status == OrderStatus.All)
                    {
                        dboOrder.Orders = await _cdbContext.Order.Include(u => u.User)
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
                        dboOrder.pageCount = (int)Math.Ceiling((double)_cdbContext.Order
                                                            .Where(x => x.OrderDate >= dboOrder.from && x.OrderDate <= dboOrder.to
                                                                                    && x.UserId == dboOrder.UserId
                                                                                    && x.BranchId == dboOrder.BranchId)
                                                            .Count() / dboOrder.pageSize);
                    }
                    else
                    {
                        dboOrder.Orders = await _cdbContext.Order.Include(u => u.User)
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
                        dboOrder.pageCount = (int)Math.Ceiling((double)_cdbContext.Order
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
                        dboOrder.Orders = await _cdbContext.Order.Include(u => u.User)
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
                        dboOrder.pageCount = (int)Math.Ceiling((double)_cdbContext.Order
                                                            .Where(x => x.OrderDate >= dboOrder.from && x.OrderDate <= dboOrder.to
                                                                                                        && (x.UserId == dboOrder.UserId
                                                                                                        || x.BranchId == dboOrder.BranchId)).Count() / dboOrder.pageSize);
                    }
                    else
                    {
                        dboOrder.Orders = await _cdbContext.Order.Include(u => u.User)
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
                        dboOrder.pageCount = (int)Math.Ceiling((double)_cdbContext.Order
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
                        dboOrder.Orders = await _cdbContext.Order.Include(u => u.User)
                                                                .Include(u => u.User.Contact)
                                                                .Include(s => s.OrderItems)
                                                                .ThenInclude(b => b.Article)
                                                                .Include(b => b.Branch)
                                                                .Where(x => x.OrderDate >= dboOrder.from && x.OrderDate <= dboOrder.to)
                                                                .OrderByDescending(d => d.OrderDate)
                                                                .Skip(dboOrder.pageSize * (dboOrder.page - 1))
                                                                .Take(dboOrder.pageSize)
                                                                .ToListAsync();
                        dboOrder.pageCount = (int)Math.Ceiling((double)_cdbContext.Order
                                                            .Where(x => x.OrderDate >= dboOrder.from && x.OrderDate <= dboOrder.to).Count() / dboOrder.pageSize);
                    }
                    else
                    {
                        dboOrder.Orders = await _cdbContext.Order.Include(u => u.User)
                                                                .Include(u => u.User.Contact)
                                                                .Include(s => s.OrderItems)
                                                                .ThenInclude(b => b.Article)
                                                                .Include(b => b.Branch)
                                                                .Where(x => x.OrderDate >= dboOrder.from && x.OrderDate <= dboOrder.to && x.Status == dboOrder.Status)
                                                                .OrderByDescending(d => d.OrderDate)
                                                                .Skip(dboOrder.pageSize * (dboOrder.page - 1))
                                                                .Take(dboOrder.pageSize)
                                                                .ToListAsync();
                        dboOrder.pageCount = (int)Math.Ceiling((double)_cdbContext.Order
                                                            .Where(x => x.OrderDate >= dboOrder.from && x.OrderDate <= dboOrder.to && x.Status == dboOrder.Status).Count() / dboOrder.pageSize);
                    }
                }
                else if (dboOrder.UserId != Guid.Empty || dboOrder.BranchId != Guid.Empty)
                {
                    if (dboOrder.Status == OrderStatus.All)
                    {
                        dboOrder.Orders = await _cdbContext.Order.Include(u => u.User)
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
                        dboOrder.pageCount = (int)Math.Ceiling((double)_cdbContext.Order
                                                            .Where(x => x.UserId == dboOrder.UserId
                                                                     || x.BranchId == dboOrder.BranchId).Count() / dboOrder.pageSize);
                    }
                    else
                    {
                        dboOrder.Orders = await _cdbContext.Order.Include(u => u.User)
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
                        dboOrder.pageCount = (int)Math.Ceiling((double)_cdbContext.Order
                                                            .Where(x => x.UserId == dboOrder.UserId
                                                                     || x.BranchId == dboOrder.BranchId
                                                                     && x.Status == dboOrder.Status).Count() / dboOrder.pageSize);
                    }
                }
                else
                {
                    if (dboOrder.Status == OrderStatus.All)
                    {
                        dboOrder.Orders = await _cdbContext.Order.Include(u => u.User)
                                                                            .Include(u => u.User.Contact)
                                                                            .Include(s => s.OrderItems)
                                                                            .ThenInclude(b => b.Article)
                                                                            .Include(b => b.Branch)
                                                                            .OrderByDescending(d => d.OrderDate)
                                                                            .Skip(dboOrder.pageSize * (dboOrder.page - 1))
                                                                            .Take(dboOrder.pageSize)
                                                                            .ToListAsync();
                        dboOrder.pageCount = (int)Math.Ceiling((double)_cdbContext.Order.Count() / dboOrder.pageSize);
                    }
                    else
                    {
                        dboOrder.Orders = await _cdbContext.Order.Include(u => u.User)
                                                                            .Include(u => u.User.Contact)
                                                                            .Include(s => s.OrderItems)
                                                                            .ThenInclude(b => b.Article)
                                                                            .Include(b => b.Branch)
                                                                            .Where(x => x.Status == dboOrder.Status)
                                                                            .OrderByDescending(d => d.OrderDate)
                                                                            .Skip(dboOrder.pageSize * (dboOrder.page - 1))
                                                                            .Take(dboOrder.pageSize)
                                                                            .ToListAsync();
                        dboOrder.pageCount = (int)Math.Ceiling((double)_cdbContext.Order.Where(x => x.Status == dboOrder.Status).Count() / dboOrder.pageSize);
                    }
                }

                return dboOrder;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
