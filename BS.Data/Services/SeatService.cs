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
    public class SeatService : ISeatService
    {
        private ISeatRepository _seatRepository;
        public SeatService(SeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public async Task<List<Seat>> Seats()
        {
            return await _seatRepository.Seats();
        }
    }
}
