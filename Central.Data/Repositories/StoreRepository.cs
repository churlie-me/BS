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
    public class StoreRepository : IStoreRepository
    {
        #region Private fields & constructor
        private readonly CDBContext _cdbContext;
        #endregion

        public StoreRepository(CDBContext _cdbContext)
        {
            this._cdbContext = _cdbContext;
        }

        public async Task<bool> AddOrUpdate(Store store)
        {
            try
            {
                if (_cdbContext.Store.Any(s => s.Id == store.Id))
                    _cdbContext.Entry(store).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                else
                    _cdbContext.Entry(store).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                return await _cdbContext.SaveChangesAsync() > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<dboStore> Stores(dboStore dboStore)
        {
            try
            {
                if (!string.IsNullOrEmpty(dboStore.search))
                {
                    dboStore.Stores = await _cdbContext.Store.Include(x => x.Branches)
                                                                    .Where(d => d.Name.Contains(dboStore.search) && !d.Deleted)
                                                                    .Skip(dboStore.pageSize * (dboStore.page - 1))
                                                                    .Take(dboStore.pageSize)
                                                                    .ToListAsync();
                    dboStore.pageCount = (int)Math.Ceiling((double)_cdbContext.Store.Where(d => d.Name.Contains(dboStore.search) && !d.Deleted).Count() / dboStore.pageSize);
                }
                else
                {
                    dboStore.Stores = await _cdbContext.Store.Include(x => x.Branches)
                                                                    .Where(d => !d.Deleted)
                                                                    .Skip(dboStore.pageSize * (dboStore.page - 1))
                                                                    .Take(dboStore.pageSize)
                                                                    .ToListAsync();
                    dboStore.pageCount = (int)Math.Ceiling((double)_cdbContext.Store.Where(d => !d.Deleted).Count() / dboStore.pageSize);
                }

                return dboStore;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
