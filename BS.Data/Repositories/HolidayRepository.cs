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
    public class HolidayRepository : IHolidayRepository
    {
        private BSDBContext bsdbContext;
        public HolidayRepository(BSDBContext bsdbContext)
        {
            this.bsdbContext = bsdbContext;
        }

        public async Task<bool> AddOrUpdate(Holiday holiday)
        {
            try
            {
                if (bsdbContext.Holiday.Any(h => h.Id == holiday.Id))
                    bsdbContext.Entry(holiday).State = EntityState.Modified;
                else
                    bsdbContext.Entry(holiday).State = EntityState.Added;

                return await bsdbContext.SaveChangesAsync() > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Holiday>> Holidays(Guid StaffId)
        {
            return await bsdbContext.Holiday.Where(x => x.AccountId == StaffId && x.Deleted == false).ToListAsync();
        }

        public async Task<bool> StoreAvailability(DateTime datetime)
        {
            return bsdbContext.Holiday.Where(x => (x.From <= datetime.Date || x.To >= datetime.Date) && x.Deleted == false).Count() == 0;
        }
    }
}
