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
    public class UserAccountRepository : IUserAccountRepository
    {
        private BSDBContext _bsdbContext;
        public UserAccountRepository(BSDBContext _bsdbContext)
        {
            this._bsdbContext = _bsdbContext;
        }

        public async Task<List<Account>> Accounts()
        {
            try
            {
                return await _bsdbContext.Users.Include(u => u.User).ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<dboAccounts> StaffAccounts(dboAccounts dboAccounts)
        {
            try
            {
                dboAccounts.Accounts = await _bsdbContext.Users.AsNoTracking().Include(u => u.User)
                                                                      .Include(s => s.Seat)
                                                                      .Include(a => a.AccountBranchServices)
                                                                      .Where(x => x.Type == AccountType.Employee || x.Type == AccountType.Management)
                                                                      .ToListAsync();

                dboAccounts.pageCount = (int)Math.Ceiling((double)_bsdbContext.Post.Where(x => !x.Deleted).Count() / dboAccounts.pageSize);

                return dboAccounts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> Add(Account account)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAccount(Account account)
        {
            try
            {
                if (_bsdbContext.Users.AsNoTracking().Any(a => account.Id == account.Id))
                {
                    _bsdbContext.Entry(account).State = EntityState.Modified;

                    //update user
                    if(account.User != null)
                    {
                        _bsdbContext.Entry(account.User).State = EntityState.Modified;

                        //update user contact
                        if (account.User.Contact != null)
                            if (_bsdbContext.Contact.Any(c => c.Id == account.User.Contact.Id))
                                _bsdbContext.Entry(account.User.Contact).State = EntityState.Modified;
                            else
                                _bsdbContext.Contact.Add(account.User.Contact);

                        //update user address
                        if (account.User.Address != null)
                            if (_bsdbContext.Address.Any(a => a.Id == account.User.Address.Id))
                                _bsdbContext.Entry(account.User.Address).State = EntityState.Modified;
                            else
                                _bsdbContext.Address.Add(account.User.Address);
                    }

                    //update Account Branch Services
                    foreach (var accountBranchService in account.AccountBranchServices)
                        if (_bsdbContext.AccountBranchService.Any(x => x.AccountId == accountBranchService.AccountId && x.BranchId == accountBranchService.BranchId && x.ServiceId == accountBranchService.ServiceId))
                            _bsdbContext.Entry(accountBranchService).State = EntityState.Modified;
                        else
                            _bsdbContext.Add(accountBranchService);

                    var result = await _bsdbContext.SaveChangesAsync();
                    return result > 0;
                }

                return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
