using BS.Core;
using Central.Data.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Central.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private CDBContext _cdbContext;
        private UserManager<Account> _userManager;
        public AccountRepository(CDBContext _cdbContext, UserManager<Account> _userManager)
        {
            this._cdbContext = _cdbContext;
            this._userManager = _userManager;
        }


        public async Task<bool> AddOrUpdate(Account account)
        {
            try
            {
                if (_cdbContext.Users.Any(u => u.Id == account.Id))
                    _cdbContext.Users.Update(account);
                else
                {
                    var state = await _userManager.CreateAsync(account, "abcd@2021");
                    return state.Succeeded;
                }

                return _cdbContext.SaveChangesAsync().Result > 0;
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
                dboAccounts.Accounts = await _cdbContext.Users.AsNoTracking().Include(u => u.User)
                                                                      .Include(s => s.Seat)
                                                                      .Include(a => a.AccountBranchServices)
                                                                      .Where(x => x.Type == AccountType.Employee || x.Type == AccountType.Management)
                                                                      .ToListAsync();

                dboAccounts.pageCount = (int)Math.Ceiling((double)_cdbContext.Users.Where(x => x.Type == AccountType.Employee || x.Type == AccountType.Management).Count() / dboAccounts.pageSize);

                return dboAccounts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
