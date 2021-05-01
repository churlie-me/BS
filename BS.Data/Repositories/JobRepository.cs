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
    public class JobRepository : IJobRepository
    {
        private BSDBContext _bsdbContext;
        public JobRepository(BSDBContext _bsdbContext)
        {
            this._bsdbContext = _bsdbContext;
        }

        public async Task<bool> AddOrModify(Job job)
        {
            try
            {
                if (_bsdbContext.Job.Any(h => h.Id == job.Id))
                    _bsdbContext.Entry(job).State = EntityState.Modified;
                else
                    _bsdbContext.Entry(job).State = EntityState.Added;

                return await _bsdbContext.SaveChangesAsync() > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Job> Job(Guid id)
        {
            try
            {
                return await _bsdbContext.Job.Include(b => b.Branch).Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<dboJob> Jobs(dboJob dboJob)
        {
            if (string.IsNullOrEmpty(dboJob.search))
            {
                dboJob.Jobs = await _bsdbContext.Job.Include(b => b.Branch)
                                                 .Where(x => !x.Deleted)
                                                 .Skip(dboJob.pageSize * (dboJob.page - 1))
                                                 .Take(dboJob.pageSize)
                                                 .ToListAsync();

                dboJob.pageCount = (int)Math.Ceiling((double)_bsdbContext.Job.Where(x => !x.Deleted).Count() / dboJob.pageSize);
            }
            else
            {
                dboJob.Jobs = await _bsdbContext.Job.Include(b => b.Branch)
                                                 .Where(y => y.Title.Contains(dboJob.search) && !y.Deleted)
                                                 .Skip(dboJob.pageSize * (dboJob.page - 1))
                                                 .Take(dboJob.pageSize)
                                                 .ToListAsync();

                dboJob.pageCount = (int)Math.Ceiling((double)_bsdbContext.Job.Where(y => y.Title.Contains(dboJob.search) && !y.Deleted).Count() / dboJob.pageSize);
            }

            return dboJob;
        }
    }
}
