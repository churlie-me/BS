using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IServices
{
    public interface IUserService
    {
        Task<bool> AddUser(User user);
        Task<List<User>> Users();
        Task<dboAccounts> StaffAccounts(dboAccounts dboAccounts);
        Task<dboCustomer> Customers(dboCustomer dboCustomer);
        Task<List<User>> Users(string search);
    }
}
