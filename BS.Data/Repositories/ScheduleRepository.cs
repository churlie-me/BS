using BS.Core;
using BS.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private BSDBContext _bsdbContext;
        public ScheduleRepository(BSDBContext _bsdbContext)
        {
            this._bsdbContext = _bsdbContext;
        }

        public Task<bool> Add(Holiday holiday)
        {
            try
            {
                _bsdbContext.Holiday.Add(holiday);
                var result = _bsdbContext.SaveChangesAsync().Result;
                return Task.FromResult(result > -1);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(Schedule schedule)
        {
            try
            {
                var _schedule = await Schedule(schedule.Id);

                _bsdbContext.Entry(_schedule).CurrentValues.SetValues(schedule);
                var result = await _bsdbContext.SaveChangesAsync();
                return (result > -1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Schedule> Schedule(Guid scheduleId)
        {
            try
            {
                if (_bsdbContext.Schedule.Count() > 0)
                    return await _bsdbContext.Schedule.Where(x => x.Id == scheduleId).FirstAsync();
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Schedule>> Schedules()
        {
            try
            {
                return await _bsdbContext.Schedule.ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<dboHoliday> Holidays(dboHoliday dboHoliday)
        {
            try
            {
                dboHoliday.Holidays = await _bsdbContext.Holiday.Where(h => h.AccountId == null && !h.Deleted).ToListAsync();
                dboHoliday.pageCount = (int)Math.Ceiling((double)_bsdbContext.Holiday.Where(h => h.AccountId == null && !h.Deleted).Count() / dboHoliday.pageSize);
                return dboHoliday;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
