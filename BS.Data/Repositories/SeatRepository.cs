using BS.Core;
using BS.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        public BSDBContext _bsdbContext;
        public SeatRepository(BSDBContext _bsdbContext)
        {
            this._bsdbContext = _bsdbContext;
        }

        public async Task<List<Seat>> Seats()
        {
            try
            {
                var seats = await _bsdbContext.Seat.ToListAsync();
                return seats;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
