using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BS.Core;
using BS.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BS.Data.Repositories
{
    public class ServiceTypeRepository : IServiceTypeRepository
    {
        private BSDBContext _bsdbContext;
        public ServiceTypeRepository(BSDBContext _bsdbContext)
        {
            this._bsdbContext = _bsdbContext;
        }

        public Task<bool> AddOrUpdate(ServiceType type)
        {
            try
            {
                if(_bsdbContext.ServiceType.Any(t => t.Id == type.Id))
                    _bsdbContext.Entry(type).State = EntityState.Modified;
                else
                    _bsdbContext.Entry(type).State = EntityState.Added;

                var result = _bsdbContext.SaveChangesAsync().Result;
                return Task.FromResult(result > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<dboServiceType> Types(dboServiceType dboServiceType)
        {
            try
            {
                if(dboServiceType.ForReservation)
                    if (!string.IsNullOrEmpty(dboServiceType.search))
                    {
                        dboServiceType.Types = await _bsdbContext.ServiceType.Where(c => c.Type == dboServiceType.search && c.Services.Count() > 0 && !c.Deleted).ToListAsync();
                        dboServiceType.pageCount = (int)Math.Ceiling((double)_bsdbContext.ServiceType.Where(c => c.Type == dboServiceType.search && c.Services.Count() > 0 && !c.Deleted).Count() / dboServiceType.pageSize);
                    }
                    else
                    {
                        dboServiceType.Types = await _bsdbContext.ServiceType.Where(c => c.Services.Count() > 0 && !c.Deleted).ToListAsync();
                        dboServiceType.pageCount = (int)Math.Ceiling((double)_bsdbContext.ServiceType.Where(c => c.Services.Count() > 0 && !c.Deleted).Count() / dboServiceType.pageSize);
                    }
                else
                    if (!string.IsNullOrEmpty(dboServiceType.search))
                    {
                        dboServiceType.Types = await _bsdbContext.ServiceType.Where(c => c.Type == dboServiceType.search && !c.Deleted).ToListAsync();
                        dboServiceType.pageCount = (int)Math.Ceiling((double)_bsdbContext.ServiceType.Where(c => c.Type == dboServiceType.search && !c.Deleted).Count() / dboServiceType.pageSize);
                    }
                    else
                    {
                        dboServiceType.Types = await _bsdbContext.ServiceType.Where(c => !c.Deleted).ToListAsync();
                        dboServiceType.pageCount = (int)Math.Ceiling((double)_bsdbContext.ServiceType.Where(c => !c.Deleted).Count() / dboServiceType.pageSize);
                    }

                return dboServiceType;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
