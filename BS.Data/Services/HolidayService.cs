using BS.Core;
using BS.Data.IRepositories;
using BS.Data.IServices;
using BS.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.Services
{
    public class HolidayService : IHolidayService
    {
        private IHolidayRepository holidayRepository;
        public HolidayService(HolidayRepository holidayRepository)
        {
            this.holidayRepository = holidayRepository;
        }

        public async Task<bool> AddOrUpdate(Holiday holiday)
        {
            return await holidayRepository.AddOrUpdate(holiday);
        }

        public async Task<List<Holiday>> Holidays(Guid StaffId)
        {
            return await holidayRepository.Holidays(StaffId);
        }

        public async Task<bool> StoreAvailability(DateTime datetime)
        {
            return await holidayRepository.StoreAvailability(datetime);
        }
    }
}
