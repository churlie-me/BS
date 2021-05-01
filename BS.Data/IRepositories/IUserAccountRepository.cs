using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IRepositories
{
    public interface IUserAccountRepository
    {
        Task<List<Account>> Accounts();
        Task<bool> Add(Account account);
        Task<bool> UpdateAccount(Account account);
        Task<dboAccounts> StaffAccounts(dboAccounts dboAccounts);
    }
}
