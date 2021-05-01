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
    public class ScheduleService : IScheduleService
    {
        private IScheduleRepository _scheduleRepository;
        public ScheduleService(ScheduleRepository scheduleRepository)
        {
            this._scheduleRepository = scheduleRepository;
        }

        public async Task<bool> Add(Holiday holiday)
        {
            return await _scheduleRepository.Add(holiday);
        }

        public async Task<dboHoliday> Holidays(dboHoliday dboHoliday)
        {
            return await _scheduleRepository.Holidays(dboHoliday);
        }

        public async Task<Schedule> Schedule(Guid scheduleId)
        {
            return await _scheduleRepository.Schedule(scheduleId);
        }

        public async Task<List<Schedule>> Schedules()
        {
            return await _scheduleRepository.Schedules();
        }

        public async Task<bool> Update(Schedule schedule)
        {
            return await _scheduleRepository.Update(schedule);
        }
    }
}
