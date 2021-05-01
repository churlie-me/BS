using BS.Core;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IServices
{
    public interface IUserAccountService
    {
        Task<IdentityResult> Add(Account account);
        Task<IdentityResult> ChangePassword(dboAccount dboAccount);
        Task<bool> Update(Account account);
        Task<List<Account>> Accounts();
        Task SaveRefreshToken(Account account);
    }
}
