using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BS.Core;
using BS.Data;
using BS.Data.Repositories;
using BS.Data.Services;
using BS.Data.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BS.Data.IRepositories;
using UserAccountService = BS.Data.Services.UserAccountService;

namespace BS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        IServiceService _serviceService;
        UserManager<Account> _userManager;
        IUserAccountService _accountService;
        IRoleRepository _roleRepository;
        IUserService _userService;
        BSDBContext _bsdbContext;
        public ServiceController(BSDBContext _bsdbContext, UserManager<Account> _userManager, RoleManager<Role> _roleManager, SignInManager<Account> _signInManager)
        {
            this._bsdbContext = _bsdbContext;
            _serviceService = new ServiceService(new ServiceRepository(_bsdbContext));
            this._userManager = _userManager;
            _roleRepository = new RoleRepository(_bsdbContext, _roleManager);
            _userService = new UserService(new UserRepository(_bsdbContext), _roleRepository, new UserAccountRepository(_bsdbContext));
            _accountService = new UserAccountService(_userManager, _roleManager, _roleRepository, _userService, new UserAccountRepository(_bsdbContext), _bsdbContext);
        }

        [HttpPost("store")]
        public async Task<dboService> Get(dboService dboService)
        {
            try
            {
                var services = await _serviceService.Services(dboService);
                
                return services;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("store/{storeId}")]
        public async Task<List<Service>> GetServices(string storeId)
        {
            try
            {
                List<Service> services = await _serviceService.StoreServices(Guid.Parse(storeId));

                return services;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("detail/{serviceId}")]
        public async Task<Service> GetService(string serviceId)
        {
            try
            {
                var service = await _serviceService.Service(Guid.Parse(serviceId));

                return service;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<bool> Post(Service service)
        {
            try
            {
                return await _serviceService.AddOrUpdate(service);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task AddStylistsAccounts(Service service)
        {
            try
            {
                foreach (var stylist in service.AccountBranchServices)
                {
                    var account = _userManager.Users.Include(u => u.User).SingleOrDefault(a => a.Email == stylist.Account.Email);

                    if (account == null)
                    {
                        stylist.Account.Password = "Abcdef@123";

                        await _accountService.Add(stylist.Account);
                        account = _userManager.Users.Include(u => u.User).SingleOrDefault(a => a.Email == stylist.Account.Email);
                    }

                    stylist.Account = account;
                    stylist.AccountId = account.Id;
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}