using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Central.Data.IRepositories
{
    public interface IAccountRepository
    {
        Task<bool> AddOrUpdate(Account account);
        Task<dboAccounts> StaffAccounts(dboAccounts dboAccounts);
    }
}
