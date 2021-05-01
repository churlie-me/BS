using BS.Core;
using BS.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.Repositories
{
    public class ServiceRepository: IServiceRepository
    {
        private BSDBContext _bsdbContext;
        public ServiceRepository(BSDBContext _bsdbContext)
        {
            this._bsdbContext = _bsdbContext;
        }

        public async Task<bool> AddOrUpdate(Service service)
        {
            try
            {
                if (_bsdbContext.Service.Any(s => s.Id == service.Id))
                {
                    _bsdbContext.Entry(service).State = EntityState.Modified;
                    foreach (var abs in service.AccountBranchServices)
                        if (_bsdbContext.AccountBranchService.Any(s => s.Id == abs.Id))
                        {
                            _bsdbContext.Entry(abs).State = EntityState.Modified;
                        }
                        else
                            _bsdbContext.Entry(abs).State = EntityState.Added;

                    foreach (var bs in service.BranchServices)
                        if (_bsdbContext.BranchService.Any(s => s.Id == bs.Id))
                        {
                            _bsdbContext.Entry(bs).State = EntityState.Modified;
                        }
                        else
                            _bsdbContext.Entry(bs).State = EntityState.Added;
                }
                else
                    _bsdbContext.Service.Add(service);

                return await _bsdbContext.SaveChangesAsync() > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateService(Service service)
        {
            try
            {
                var existingService = _bsdbContext.Service.Include(u => u.SaleItem)
                                   .Include(e => e.AccountBranchServices)
                                   .FirstOrDefault(b => b.Id == service.Id);
                if (existingService != null)
                {
                    foreach (var accountstoreService in service.AccountBranchServices)
                    {
                        var _existingAccountBranchService = existingService.AccountBranchServices.FirstOrDefault(p => p.AccountId == accountstoreService.AccountId && p.ServiceId == accountstoreService.ServiceId && p.BranchId == accountstoreService.BranchId);

                        if (_existingAccountBranchService == null)
                        {
                            existingService.AccountBranchServices.Add(accountstoreService);
                        }
                        else
                        {
                            _bsdbContext.Entry(_existingAccountBranchService).CurrentValues.SetValues(accountstoreService);
                        }
                    }

                    _bsdbContext.Entry(existingService).CurrentValues.SetValues(service);

                    var result = await _bsdbContext.SaveChangesAsync();

                    return result > 0;
                }
                else
                    return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Service> Service(Guid serviceId)
        {
            try
            {
                var service = await _bsdbContext.Service.Include(p => p.SaleItem).Include(s => s.AccountBranchServices)
                                                                                 .ThenInclude(a => a.Account)
                                                                                 .ThenInclude(u => u.User)
                                                                                 .Include(s => s.BranchServices)
                                                                                 .ThenInclude(s => s.Branch).FirstOrDefaultAsync(x => x.Id == serviceId);
                return service;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<dboService> Services(dboService dboService)
        {
            try
            {
                if (string.IsNullOrEmpty(dboService.search) && (dboService.CategoryId != Guid.Empty || dboService.TypeId != Guid.Empty))
                {
                    dboService.Services = await _bsdbContext.Service.Include(x => x.SaleItem)
                                                                    .Include(a => a.AccountBranchServices)
                                                                    .ThenInclude(v => v.Account.User)
                                                                    .Include(a => a.AccountBranchServices)
                                                                    .ThenInclude(v => v.Account.Seat)
                                                                    .Include(y => y.BranchServices)
                                                                    .Where(d => (d.CategoryId == dboService.CategoryId || d.ServiceTypeId == dboService.TypeId) && !d.Deleted)
                                                                    .Skip(dboService.pageSize * (dboService.page - 1))
                                                                    .Take(dboService.pageSize)
                                                                    .ToListAsync();
                    dboService.pageCount = (int)Math.Ceiling((double)_bsdbContext.Service.Where(d => (d.CategoryId == dboService.CategoryId || d.ServiceTypeId == dboService.TypeId) && !d.Deleted).Count() / dboService.pageSize);
                }
                else if (!string.IsNullOrEmpty(dboService.search) && (dboService.CategoryId != Guid.Empty || dboService.TypeId != Guid.Empty))
                {
                    dboService.Services = await _bsdbContext.Service.Include(x => x.SaleItem)
                                                                    .Include(a => a.AccountBranchServices)
                                                                    .ThenInclude(v => v.Account.User)
                                                                    .Include(a => a.AccountBranchServices)
                                                                    .ThenInclude(v => v.Account.Seat)
                                                                    .Include(y => y.BranchServices)
                                                                    .Where(d => d.Name.Contains(dboService.search) && (d.CategoryId == dboService.CategoryId || d.ServiceTypeId == dboService.TypeId) && !d.Deleted)
                                                                    .Skip(dboService.pageSize * (dboService.page - 1))
                                                                    .Take(dboService.pageSize)
                                                                    .ToListAsync();
                    dboService.pageCount = (int)Math.Ceiling((double)_bsdbContext.Service.Where(d => d.Name.Contains(dboService.search) && (d.CategoryId == dboService.CategoryId || d.ServiceTypeId == dboService.TypeId) && !d.Deleted).Count() / dboService.pageSize);
                }
                else if(!string.IsNullOrEmpty(dboService.search) && dboService.CategoryId == Guid.Empty && dboService.TypeId == Guid.Empty)
                {
                    dboService.Services = await _bsdbContext.Service.Include(x => x.SaleItem)
                                                                    .Include(a => a.AccountBranchServices)
                                                                    .ThenInclude(v => v.Account.User)
                                                                    .Include(a => a.AccountBranchServices)
                                                                    .ThenInclude(v => v.Account.Seat)
                                                                    .Include(y => y.BranchServices)
                                                                    .Where(s => s.Name.Contains(dboService.search) && !s.Deleted)
                                                                    .Skip(dboService.pageSize * (dboService.page - 1))
                                                                    .Take(dboService.pageSize)
                                                                    .ToListAsync();

                    dboService.pageCount = (int)Math.Ceiling((double)_bsdbContext.Service.Where(d => d.Name.Contains(dboService.search) && !d.Deleted).Count() / dboService.pageSize);
                }
                else
                {
                    dboService.Services = await _bsdbContext.Service.Include(x => x.SaleItem)
                                                                    .Include(a => a.AccountBranchServices)
                                                                    .ThenInclude(v => v.Account.User)
                                                                    .Include(a => a.AccountBranchServices)
                                                                    .ThenInclude(v => v.Account.Seat)
                                                                    .Include(y => y.BranchServices)
                                                                    .Where(s => !s.Deleted)
                                                                    .Skip(dboService.pageSize * (dboService.page - 1))
                                                                    .Take(dboService.pageSize)
                                                                    .ToListAsync();

                    dboService.pageCount = (int)Math.Ceiling((double)_bsdbContext.Service.Where(d => !d.Deleted).Count() / dboService.pageSize);
                }
                return dboService;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Service>> StoreServices(Guid storeId)
        {
            
            try
            {
                var services = await _bsdbContext.Service.Where(ass => ass.AccountBranchServices.Any(s => s.BranchId == storeId)).ToListAsync();
                return services;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
