using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IServices
{
    public interface IHolidayService
    {
        Task<List<Holiday>> Holidays(Guid StaffId);
        Task<bool> AddOrUpdate(Holiday holiday);
        Task<bool> StoreAvailability(DateTime datetime);
    }
}
