using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IRepositories
{
    public interface IUserRepository
    {
        Task<bool> AddUser(User user);
        Task<dboCustomer> Customers(dboCustomer dboCustomer);
        Task<List<User>> Users(string search);
    }
}
