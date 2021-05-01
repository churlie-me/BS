using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IRepositories
{
    public interface IHolidayRepository
    {
        Task<List<Holiday>> Holidays(Guid StaffId);
        Task<bool> AddOrUpdate(Holiday holiday);
        Task<bool> StoreAvailability(DateTime datetime);
    }
}
