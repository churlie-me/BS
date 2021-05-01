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
    public class ApplicationRepository : IApplicationRepository
    {
        private BSDBContext _bsdbContext;
        public ApplicationRepository(BSDBContext _bsdbContext)
        {
            this._bsdbContext = _bsdbContext;
        }

        public async Task<bool> AddOrModify(Application application)
        {
            try
            {
                if (_bsdbContext.Application.Any(a => a.Id == application.Id))
                    _bsdbContext.Entry(application).State = EntityState.Modified;
                else
                    _bsdbContext.Entry(application).State = EntityState.Added;

                return await _bsdbContext.SaveChangesAsync() > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Application> Application(Guid id)
        {
            try
            {
                return await _bsdbContext.Application.Include(j => j.Job)
                                                     .Include(u => u.User)
                                                     .Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<dboApplication> Applications(dboApplication dboApplication)
        {
            if (string.IsNullOrEmpty(dboApplication.search))
            {
                dboApplication.Applications = await _bsdbContext.Application.Include(j => j.Job)
                                                 .Include(u => u.User)
                                                 .ThenInclude(c => c.Contact)
                                                 .Where(y => y.JobId == dboApplication.JobId)
                                                 .Skip(dboApplication.pageSize * (dboApplication.page - 1))
                                                 .Take(dboApplication.pageSize)
                                                 .ToListAsync();

                dboApplication.pageCount = (int)Math.Ceiling((double)_bsdbContext.Application.Where(y => y.JobId == dboApplication.JobId).Count() / dboApplication.pageSize);
            }
            else
            {
                dboApplication.Applications = await _bsdbContext.Application.Include(j => j.Job)
                                                 .Include(u => u.User)
                                                 .ThenInclude(c => c.Contact)
                                                 .Where(y => (y.User.FirstName.Contains(dboApplication.search) || y.User.LastName.Contains(dboApplication.search)) && y.JobId == dboApplication.JobId)
                                                 .Skip(dboApplication.pageSize * (dboApplication.page - 1))
                                                 .Take(dboApplication.pageSize)
                                                 .ToListAsync();

                dboApplication.pageCount = (int)Math.Ceiling((double)_bsdbContext.Application.Where(y => (y.User.FirstName.Contains(dboApplication.search) || y.User.LastName.Contains(dboApplication.search)) && y.JobId == dboApplication.JobId).Count() / dboApplication.pageSize);
            }

            return dboApplication;
        }
    }
}
