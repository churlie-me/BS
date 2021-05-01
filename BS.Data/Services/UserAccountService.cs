using BS.Core;
using BS.Data.IRepositories;
using BS.Data.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.Services
{
    public class UserAccountService : IUserAccountService
    {
        #region Private variables and constructor
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserService _userService;
        private readonly IUserAccountRepository _accountRespository;
        private readonly BSDBContext _bsdbContext;
        public UserAccountService(UserManager<Account> _userManager, RoleManager<Role> _roleManager, IRoleRepository _roleRepository, IUserService _userService, IUserAccountRepository _accountRespository, BSDBContext _bsdbContext)
        {
            this._userManager = _userManager;
            this._roleManager = _roleManager;
            this._roleRepository = _roleRepository;
            this._userService = _userService;
            this._accountRespository = _accountRespository;
            this._bsdbContext = _bsdbContext;
        }
        #endregion

        public async Task<IdentityResult> Add(Account account)
        {
            try
            {
                if(account.User != null)
                    account.User.Id = Guid.NewGuid();

                var user = _bsdbContext.User.Include(c => c.Contact).Where(x => x.Contact.Email == account.Email).FirstOrDefaultAsync().Result;
                if (user != null)
                {
                    account.UserId = user.Id;
                    account.User = null;
                }

                account.UserName = account.Email;
                //await _userService.AddUser(account.User);
                var result = await _userManager.CreateAsync(account, account.Password);

                return await Task.FromResult(result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Task<List<Account>> Accounts()
        {
            try
            {
                return _accountRespository.Accounts();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task SaveRefreshToken(Account account)
        {
            try
            {
                await _accountRespository.UpdateAccount(account);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IdentityResult> ChangePassword(dboAccount dboAccount)
        {
            try
            {
                var r = await _userManager.ChangePasswordAsync(dboAccount.Account, dboAccount.currentPassword, dboAccount.newPassword);
                return r;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(Account account)
        {
            try
            {
                return await _accountRespository.UpdateAccount(account);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
