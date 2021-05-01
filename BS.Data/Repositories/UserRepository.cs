using BS.Core;
using BS.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace BS.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region Private fields & constructor
        private readonly BSDBContext _bsdbContext;
        public UserRepository(BSDBContext _bsdbContext)
        {
            this._bsdbContext = _bsdbContext;
        }
        #endregion

        public async Task<bool> AddUser(User user)
        {
            try
            {
                _bsdbContext.User.Add(user);
                var result = await _bsdbContext.SaveChangesAsync();
                return result > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<User>> Users(string search)
        {
            try
            {
                return await _bsdbContext.User.Include(x => x.Address).Include(y => y.Contact).Where(x => x.FirstName.Contains(search) || x.LastName.Contains(search)).ToListAsync();
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
                if(!string.IsNullOrEmpty(dboCustomer.search))
                    dboCustomer.Customers = await _bsdbContext.User.Include(u => u.Address)
                                                  .Include(u => u.Contact)
                                                  .Where(u => (u.Appointments.Count() > 0 || u.Orders.Count() > 0) && (u.FirstName.Contains(dboCustomer.search) || u.LastName.Contains(dboCustomer.search)))
                                                  .Skip(dboCustomer.pageSize * (dboCustomer.page - 1))
                                                  .Take(dboCustomer.pageSize)
                                                  .ToListAsync();
                else
                    dboCustomer.Customers = await _bsdbContext.User.Include(u => u.Address)
                                                  .Include(u => u.Contact)
                                                  .Where(u => (u.Appointments.Count() > 0 || u.Orders.Count() > 0))
                                                  .Skip(dboCustomer.pageSize * (dboCustomer.page - 1))
                                                  .Take(dboCustomer.pageSize)
                                                  .ToListAsync();


                dboCustomer.pageCount = (int)Math.Ceiling((double)_bsdbContext.User.Where(u => (u.Appointments.Count() > 0 || u.Orders.Count() > 0)).Count() / dboCustomer.pageSize);

                return dboCustomer;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
