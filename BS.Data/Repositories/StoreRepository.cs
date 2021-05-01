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
    public class StoreRepository : IStoreRepository
    {
        #region Private fields & constructor
        private readonly BSDBContext _bsdbContext;
        #endregion

        public StoreRepository(BSDBContext _bsdbContext)
        {
            this._bsdbContext = _bsdbContext;
        }

        public async Task<bool> Add(Store store)
        {
            try
            {
                _bsdbContext.Store.Add(store);
                var result = await _bsdbContext.SaveChangesAsync();
                return (result > -1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Branch> Branch(Guid BranchId)
        {
            try
            {
                return await _bsdbContext.Branch.Include(a => a.Address).Include(c => c.Contact)
                                         .Include(x => x.AccountBranchServices)
                                         .Include(v => v.Seats).FirstOrDefaultAsync(y => y.Id == BranchId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AddOrUpdateBranch(Branch branch)
        {
            try
            {

                if (_bsdbContext.Branch.Any(x => x.Id == branch.Id))
                {
                    _bsdbContext.Entry(branch).State = EntityState.Modified;
                    foreach (var seat in branch.Seats)
                    {
                        bool exists = _bsdbContext.Seat.Any(x => x.Id == seat.Id);
                        if (exists)   
                            _bsdbContext.Entry(seat).State = EntityState.Modified;
                        else
                            _bsdbContext.Entry(seat).State = EntityState.Added;
                    }

                    //branch address
                    if(_bsdbContext.Address.Any(a => a.Id ==  branch.Address.Id))
                        _bsdbContext.Entry(branch.Address).State = EntityState.Modified;
                    else
                        _bsdbContext.Entry(branch.Address).State = EntityState.Added;

                    //branch contact
                    if (_bsdbContext.Contact.Any(a => a.Id == branch.Contact.Id))
                        _bsdbContext.Entry(branch.Contact).State = EntityState.Modified;
                    else
                        _bsdbContext.Entry(branch.Contact).State = EntityState.Added;
                }
                else
                    _bsdbContext.Add(branch);

                var result = await _bsdbContext.SaveChangesAsync();
                return (result > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Branch>> Branches()
        {
            try
            {
                var branches = await _bsdbContext.Branch.AsNoTracking().Include(a => a.Address)
                                                .Include(c => c.Contact)
                                                .Include(x => x.AccountBranchServices)
                                                .Include(v => v.Seats)
                                                .ThenInclude(s => s.Account)
                                                .ThenInclude(u => u.User)
                                                .Where(x => !x.Deleted).ToListAsync();

                return branches;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Store> Store()
        {
            try
            {
                return await _bsdbContext.Store.Include(s => s.Schedules.OrderBy(x => x.Day))
                                                                .Include(b => b.Branches)
                                                                .ThenInclude(s => s.Address)
                                                                .Include(b => b.Branches)
                                                                .ThenInclude(s => s.Contact).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Store> StoreBySlug(string slug)
        {
            try
            {
                return await _bsdbContext.Store.AsNoTracking().Include(s => s.Schedules.OrderBy(x => x.Day))
                                                                .Include(b => b.Branches)
                                                                .ThenInclude(s => s.Address)
                                                                .Where(x => x.Slug == slug).FirstAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> Update(Store store)
        {
            try
            {
                if (_bsdbContext.Store.AsNoTracking().Any(s => s.Id == store.Id))
                {
                    _bsdbContext.Entry(store).State = EntityState.Modified;
                    foreach (var schedule in store.Schedules)
                        if (_bsdbContext.Schedule.Any(x => x.Id == schedule.Id))
                            _bsdbContext.Entry(schedule).State = EntityState.Modified;
                        else
                            _bsdbContext.Entry(schedule).State = EntityState.Added;
                }
                else
                    _bsdbContext.Entry(store).State = EntityState.Added;

                var result = _bsdbContext.SaveChanges();
                return Task.FromResult(result > -1);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Store>> UserStores(Guid accountId)
        {
            try
            {
                List<Store> stores =  await _bsdbContext.Store.Include(a => a.Branches).Where(x => x.AccountId == accountId).ToListAsync();

                return stores;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Seat>> GetSeats(Guid branchId)
        {
            try
            {
                return await _bsdbContext.Seat.Include(a => a.Account)
                                              .Where(x => x.BranchId == branchId)
                                              .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
