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
    public class RequestRepository : IRequestRepository
    {
        private BSDBContext _bsdbContext;
        public RequestRepository(BSDBContext _bsdbContext)
        {
            this._bsdbContext = _bsdbContext;
        }

        public async Task<bool> AddOrUpdate(Request request)
        {
            try
            {

                if (_bsdbContext.Request.Any(r => r.Id == request.Id))
                    _bsdbContext.Entry(request).State = EntityState.Modified;
                else
                    _bsdbContext.Entry(request).State = EntityState.Added;

                return await _bsdbContext.SaveChangesAsync() > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<dborequests> Requests(dborequests dborequests)
        {
            try
            {
                if (dborequests.from != DateTime.MinValue && dborequests.to != DateTime.MinValue && !string.IsNullOrEmpty(dborequests.search) && dborequests.ReasonId != Guid.Empty)
                {
                    dborequests.Requests = await _bsdbContext.Request.Include(r => r.Account)
                                                    .Include(r => r.Account.User)
                                                    .Include(r => r.Reason)
                                                    .Include(u => u.User)
                                                    .ThenInclude(c => c.Contact)
                                                    .Where(y => y.Account.User.FirstName.Contains(dborequests.search) || y.Account.User.LastName.Contains(dborequests.search) &&
                                                    dborequests.from <= y.RequestedOn && y.RequestedOn <= dborequests.to && y.ReasonId == dborequests.ReasonId)
                                                    .Skip(dborequests.pageSize * (dborequests.page - 1))
                                                    .Take(dborequests.pageSize)
                                                    .ToListAsync();

                    dborequests.pageCount = (int)Math.Ceiling((double)_bsdbContext.Request.Where(y => y.Account.User.FirstName.Contains(dborequests.search) || y.Account.User.LastName.Contains(dborequests.search) &&
                                                     dborequests.from <= y.RequestedOn && y.RequestedOn <= dborequests.to && y.ReasonId == dborequests.ReasonId).Count() / dborequests.pageSize);
                }
                else if (dborequests.from != DateTime.MinValue && dborequests.to != DateTime.MinValue && !string.IsNullOrEmpty(dborequests.search))
                {
                    dborequests.Requests = await _bsdbContext.Request.Include(r => r.Account)
                                                     .Include(r => r.Account.User)
                                                     .Include(r => r.Reason)
                                                     .Include(u => u.User)
                                                     .ThenInclude(c => c.Contact)
                                                     .Where(y => y.Account.User.FirstName.Contains(dborequests.search) || y.Account.User.LastName.Contains(dborequests.search) &&
                                                     dborequests.from <= y.RequestedOn && y.RequestedOn <= dborequests.to)
                                                     .Skip(dborequests.pageSize * (dborequests.page - 1))
                                                     .Take(dborequests.pageSize)
                                                     .ToListAsync();

                    dborequests.pageCount = (int)Math.Ceiling((double)_bsdbContext.Request.Where(y => y.Account.User.FirstName.Contains(dborequests.search) || y.Account.User.LastName.Contains(dborequests.search) &&
                                                     dborequests.from <= y.RequestedOn && y.RequestedOn <= dborequests.to).Count() / dborequests.pageSize);
                }
                else if (dborequests.from != DateTime.MinValue && dborequests.to != DateTime.MinValue && dborequests.ReasonId != Guid.Empty)
                {
                    dborequests.Requests = await _bsdbContext.Request.Include(r => r.Account)
                                                    .Include(r => r.Account.User)
                                                    .Include(r => r.Reason)
                                                    .Include(u => u.User)
                                                    .ThenInclude(c => c.Contact)
                                                    .Where(y => dborequests.from <= y.RequestedOn && y.RequestedOn <= dborequests.to && y.ReasonId == dborequests.ReasonId)
                                                    .Skip(dborequests.pageSize * (dborequests.page - 1))
                                                    .Take(dborequests.pageSize)
                                                    .ToListAsync();

                    dborequests.pageCount = (int)Math.Ceiling((double)_bsdbContext.Request.Where(y => dborequests.from <= y.RequestedOn && y.RequestedOn <= dborequests.to && y.ReasonId == dborequests.ReasonId).Count() / dborequests.pageSize);
                }
                else if(dborequests.from != DateTime.MinValue && dborequests.to != DateTime.MinValue)
                {
                    dborequests.Requests = await _bsdbContext.Request.Include(r => r.Account)
                                                     .Include(r => r.Account.User)
                                                     .Include(r => r.Reason)
                                                     .Include(u => u.User)
                                                     .ThenInclude(c => c.Contact)
                                                     .Where(y => dborequests.from <= y.RequestedOn && y.RequestedOn <= dborequests.to)
                                                     .Skip(dborequests.pageSize * (dborequests.page - 1))
                                                     .Take(dborequests.pageSize)
                                                     .ToListAsync();

                    dborequests.pageCount = (int)Math.Ceiling((double)_bsdbContext.Request.Where(y => dborequests.from <= y.RequestedOn && y.RequestedOn <= dborequests.to).Count() / dborequests.pageSize);
                }
                else if(dborequests.ReasonId != Guid.Empty)
                {
                    dborequests.Requests = await _bsdbContext.Request.Include(r => r.Account)
                                                    .Include(r => r.Account.User)
                                                    .Include(r => r.Reason)
                                                    .Include(u => u.User)
                                                    .ThenInclude(c => c.Contact)
                                                    .Where(y => y.ReasonId == dborequests.ReasonId)
                                                    .Skip(dborequests.pageSize * (dborequests.page - 1))
                                                    .Take(dborequests.pageSize)
                                                    .ToListAsync();

                    dborequests.pageCount = (int)Math.Ceiling((double)_bsdbContext.Request.Where(y => y.ReasonId == dborequests.ReasonId).Count() / dborequests.pageSize);
                }
                else if (!string.IsNullOrEmpty(dborequests.search))
                {
                    dborequests.Requests = await _bsdbContext.Request.Include(r => r.Account)
                                                     .Include(r => r.Account.User)
                                                     .Include(r => r.Reason)
                                                     .Include(u => u.User)
                                                     .ThenInclude(c => c.Contact)
                                                     .Where(y => y.Account.User.FirstName.Contains(dborequests.search) || y.Account.User.LastName.Contains(dborequests.search))
                                                     .Skip(dborequests.pageSize * (dborequests.page - 1))
                                                     .Take(dborequests.pageSize)
                                                     .ToListAsync();

                    dborequests.pageCount = (int)Math.Ceiling((double)_bsdbContext.Request.Where(y => y.Account.User.FirstName.Contains(dborequests.search) || y.Account.User.LastName.Contains(dborequests.search)).Count() / dborequests.pageSize);
                }
                else
                {
                    dborequests.Requests = await _bsdbContext.Request.Include(r => r.Account)
                                                     .Include(r => r.Account.User)
                                                     .Include(r => r.Reason)
                                                     .Include(u => u.User)
                                                     .ThenInclude(c => c.Contact)
                                                     .Skip(dborequests.pageSize * (dborequests.page - 1))
                                                     .Take(dborequests.pageSize).ToListAsync();

                    dborequests.pageCount = (int)Math.Ceiling((double)_bsdbContext.Request.Count() / dborequests.pageSize);
                }

                return dborequests;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
