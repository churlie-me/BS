using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BS.Core;
using BS.Data.IRepositories;
using BS.Data.IServices;

namespace BS.Data.Services
{
    public class StoreService: IStoreService
    {
        private IStoreRepository _storeRepository;
        public StoreService(IStoreRepository _storeRepository)
        {
            this._storeRepository = _storeRepository;
        }

        public async Task<Store> Store()
        {
            return await _storeRepository.Store();
        }
        
        public async Task<Store> StoreBySlug(string slug)
        {
            return await _storeRepository.StoreBySlug(slug);
        }

        public async Task<bool> Add(Store store)
        {
            return await _storeRepository.Add(store);
        }

        public async Task<bool> Update(Store store)
        {
            return await _storeRepository.Update(store);
        }

        public async Task<List<Store>> UserStores(Guid accountId)
        {
            return await _storeRepository.UserStores(accountId);
        }

        public async Task<List<Branch>> Branches()
        {
            return await _storeRepository.Branches();
        }

        public async Task<bool> AddOrUpdateBranch(Branch branch)
        {
            return await _storeRepository.AddOrUpdateBranch(branch);
        }

        public async Task<List<Seat>> GetSeats(Guid branchId)
        {
            return await _storeRepository.GetSeats(branchId);
        }
    }
}
