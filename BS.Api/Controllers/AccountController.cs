using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BS.Api.Providers;
using BS.Core;
using BS.Data;
using BS.Data.IRepositories;
using BS.Data.IServices;
using BS.Data.Repositories;
using BS.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using UserAccountService = BS.Data.Services.UserAccountService;

namespace BS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserAccountService _accountService;
        private IRoleRepository _roleRepository;
        private IUserService _userService;
        UserManager<Account> _userManager;
        RoleManager<Role> _roleManager;
        SignInManager<Account> _signInManager;
        private Account account;
        private BSDBContext _bsdbContext;
        public AccountController(BSDBContext _bsdbContext, UserManager<Account> _userManager, RoleManager<Role> _roleManager, SignInManager<Account> _signInManager)
        {
            this._userManager = _userManager;
            this._roleManager = _roleManager;
            this._signInManager = _signInManager;
            this._bsdbContext = _bsdbContext;
            _roleRepository = new RoleRepository(_bsdbContext, _roleManager);
            _userService = new UserService(new UserRepository(_bsdbContext), _roleRepository, new UserAccountRepository(_bsdbContext));
            _accountService = new UserAccountService(_userManager, _roleManager, _roleRepository, _userService, new UserAccountRepository(_bsdbContext), _bsdbContext);
        }

        [HttpPost("register")]
        public async Task<IdentityResult> Post([FromBody] Account account)
        {
            try
            {
                return await _accountService.Add(account);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("password/change")]
        public async Task<IdentityResult> ChangePassword(dboAccount dboAccount)
        {
            try
            {
                return await _accountService.ChangePassword(dboAccount);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("update")]
        public async Task<bool> Update([FromBody] Account account)
        {
            try
            {
                account.AccountBranchServices.ForEach(x => { x.Service = null; });
                return await _accountService.Update(account);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{mail}")]
        public async Task<Account> Get(string mail)
        {
            try
            {
                var account = await _bsdbContext.Users.AsNoTracking().Include(y => y.AccountBranchServices).FirstOrDefaultAsync(z => z.UserName == mail);

                return account;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("registration/invite")]
        public async Task<bool> SendInvitation([FromBody] Account account)
        {
            Store store = await _bsdbContext.Store.FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(store.Url) || string.IsNullOrEmpty(store.SenderEmail) || string.IsNullOrEmpty(store.SenderEmailPassword) || string.IsNullOrEmpty(store.Host))
                return false;

            var subject = "Account Registration";
            var message = "<h5>Greetings sir / madam,</h5>"
                        + "<p> You have been invited to register an account for the role of <strong>" 
                        + account.Type.ToString() + "</strong> at " + store.Name 
                        + "<br> Please complete your registration <a href='" + (Char.IsLetterOrDigit(store.Url.Last())? store.Url : store.Url.Remove(store.Url.Length - 1) + "/#/signup?role=" + (int)account.Type) + "&email="+ account.Email +"'>here</a></p>";
            return MailProvider.SendMail(store, account.Email, true, subject, message);
        }

        [HttpPost("authenticate")]
        public async Task<Account> Post([FromBody] Credentials credentials)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(credentials.Email, credentials.Password, false, false);
                if (result.Succeeded)
                {
                    account = _userManager.Users.AsNoTracking().Include(u => u.User)
                                                .Include(c => c.User.Contact)
                                                .Include(c => c.User.Address)
                                                .Include(y => y.AccountBranchServices)
                                                .AsNoTracking()
                                                .SingleOrDefault(a => a.Email == credentials.Email);
                    account.Password = credentials.Password;

                    if (account.Active != null && account.Active.Value == false)
                    {
                        throw new ApplicationException("USER NOT ACTIVE");
                    }

                    account.RefreshToken = GenerateJwtToken(credentials.Email, account);
                    account.RefreshTokenExpirationDate = DateTime.Now.AddDays(1);

                    //update account
                    await _accountService.Update(account);

                    return account;
                }
                else
                    throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
            }
            catch (Exception ex)
            {
                throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
            }
        }

        private string GenerateJwtToken(string email, Account account)
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        [HttpGet("staff/{id}")]
        public async Task<Account> GetStaff(Guid id)
        {
            try
            {
                var account = await _bsdbContext.Users.AsNoTracking()
                                                      .Include(u => u.User)
                                                      .Include(u => u.User.Address)
                                                      .Include(u => u.User.Contact)
                                                      .Include(b => b.Branch)
                                                      .Include(s => s.Seat)
                                                      .Include(y => y.AccountBranchServices.Where(abs => !abs.Deleted))
                                                      .ThenInclude(a => a.Service)
                                                      .FirstOrDefaultAsync(z => z.Id == id);

                return account;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}