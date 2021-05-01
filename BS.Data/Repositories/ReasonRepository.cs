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
    public class ReasonRepository : IReasonRepository
    {
        private BSDBContext _bsdbContext;
        public ReasonRepository(BSDBContext _bsdbContext)
        {
            this._bsdbContext = _bsdbContext;
        }

        public async Task<bool> AddOrUpdate(Reason reason)
        {
            if (_bsdbContext.Reason.Any(r => r.Id == reason.Id))
                _bsdbContext.Entry(reason).State = EntityState.Modified;
            else
                _bsdbContext.Entry(reason).State = EntityState.Added;

            return await _bsdbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Reason>> Reasons()
        {
            try
            {
                return await _bsdbContext.Reason.Where(y => !y.Deleted).ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
