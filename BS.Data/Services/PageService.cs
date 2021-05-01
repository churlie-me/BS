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
    public class PageService : IPageService
    {
        private IPageRepository _pageRepository;
        public PageService(PageRepository _pageRepository)
        {
            this._pageRepository = _pageRepository;
        }

        public async Task<bool> AddOrUpdate(Page Page)
        {
            return await _pageRepository.AddOrUpdate(Page);
        }

        public Task<dboPages> Get(dboPages dboPages)
        {
            return _pageRepository.Get(dboPages);
        }

        public async Task<Page> GetPage(Guid PageId)
        {
            return await _pageRepository.GetPage(PageId);
        }

        public async Task<Content> GetContent(Guid ContentId)
        {
            return await _pageRepository.GetContent(ContentId);
        }

        public async Task<List<Page>> GetDetailPages()
        {
            return await _pageRepository.GetDetailPages();
        }
    }
}
