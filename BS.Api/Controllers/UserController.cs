using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BS.Core;
using BS.Data;
using BS.Data.IServices;
using BS.Data.Services;
using BS.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace BS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService _userService;
        public UserController(BSDBContext _bsdbContext, RoleManager<Role> _roleManger)
        {
            _userService = new UserService(new UserRepository(_bsdbContext), new RoleRepository(_bsdbContext, _roleManger), new UserAccountRepository(_bsdbContext));
        }

        //[HttpGet]
        //public async Task<List<UserDto>> GetAllUsers()
        //{
        //    //check if user has rights to see this page
        //    var bearerToken = Request.Headers["Authorization"].ToString().Split("Bearer ")[1];
        //    var principal = GetPrincipalFromExpiredToken(bearerToken);
        //    var username = principal.Claims.Where(c => c.Properties.First().Value.Equals("sub")).FirstOrDefault().Value;
        //    var user = _userManager.Users.SingleOrDefault(u => u.Email == username);

        //    if (user != null)
        //    {
        //        var roles = await _userManager.GetRolesAsync(user);
        //        var userRole = roles.FirstOrDefault();

        //        if (!userRole.Equals("Admin"))
        //        {
        //            throw new ApplicationException("NO PERMISSION");
        //        }
        //        else
        //        {
        //            return _userService.GetAllUsers();
        //        }
        //    }
        //    else
        //    {
        //        throw new ApplicationException("NO PERMISSIOn");
        //    }
        //}

        [HttpGet]
        public async Task<List<User>> Get()
        {
            try
            {
               return await _userService.Users();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("search/{search}")]
        public async Task<List<User>> Users(string search)
        {
            try
            {
                return await _userService.Users(search);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("customer")]
        public async Task<dboCustomer> Customers(dboCustomer dboCustomer)
        {
            try
            {
                return await _userService.Customers(dboCustomer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("staff")]
        public async Task<dboAccounts> GetStaff(dboAccounts dboAccounts)
        {
            try
            {
                return await _userService.StaffAccounts(dboAccounts);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task AddUser([FromBody] User user)
        {
            await _userService.AddUser(user);
        }
    }
}