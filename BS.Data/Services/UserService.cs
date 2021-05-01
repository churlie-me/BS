using BS.Core;
using BS.Data.IRepositories;
using BS.Data.IServices;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserAccountRepository _accountRepository;
        public UserService(IUserRepository _userRepository, IRoleRepository _roleRepository, IUserAccountRepository _accountRepository)
        {
            this._userRepository = _userRepository;
            this._roleRepository = _roleRepository;
            this._accountRepository = _accountRepository;
        }

        public async Task<bool> AddUser(User user)
        {
            try
            {
                return await _userRepository.AddUser(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<dboCustomer> Customers(dboCustomer dboCustomer)
        {
            try
            {
                return await _userRepository.Customers(dboCustomer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<User>> Users()
        {
            try
            {
                List<User> _users = new List<User>() ;
                /*foreach (var account in await _accountRepository.Accounts())
                {
                    if(account.User != null)
                        _users.Add(account.User);
                }*/

                return _users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<User>> Users(string search)
        {
            try
            {
                return await _userRepository.Users(search);
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
                return await _accountRepository.StaffAccounts(dboAccounts);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
