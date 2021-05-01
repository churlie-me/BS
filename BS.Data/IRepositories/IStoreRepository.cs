using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IRepositories
{
    public interface IStoreRepository
    {
        Task<Store> Store();
        Task<Store> StoreBySlug(string slug);
        Task<bool> Add(Store store);
        Task<bool> Update(Store store);
        Task<List<Store>> UserStores(Guid accountId);
        Task<List<Branch>> Branches();
        Task<bool> AddOrUpdateBranch(Branch branch);
        Task<List<Seat>> GetSeats(Guid branchId);
    }
}
