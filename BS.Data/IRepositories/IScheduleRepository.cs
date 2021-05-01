using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IRepositories
{
    public interface IScheduleRepository
    {
        Task<bool> Add(Holiday holiday);
        Task<Schedule> Schedule(Guid scheduleId);
        Task<bool> Update(Schedule schedule);
        Task<List<Schedule>> Schedules();
        Task<dboHoliday> Holidays(dboHoliday dboHoliday);
    }
}
