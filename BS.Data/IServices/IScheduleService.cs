using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IServices
{
    public interface IScheduleService
    {
        Task<bool> Add(Holiday holiday);
        Task<Schedule> Schedule(Guid scheduleId);
        Task<bool> Update(Schedule schedule);
        Task<List<Schedule>> Schedules();
        Task<dboHoliday> Holidays(dboHoliday dboHoliday);
    }
}
