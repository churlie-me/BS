using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BS.Core;
using BS.Data;
using BS.Data.IServices;
using BS.Data.Repositories;
using BS.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace BS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageController : ControllerBase
    {
        private IPageService _pageService;
        public PageController(BSDBContext _bsdbContext)
        {
            _pageService = new PageService(new PageRepository(_bsdbContext));
        }

        [HttpPost]
        public async Task<bool> Post([FromBody] Page _page)
        {
            return await _pageService.AddOrUpdate(_page);
        }

        [HttpPost("pages")]
        public async Task<dboPages> Get(dboPages dboPages)
        {
            try
            {
                return await _pageService.Get(dboPages);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("detail/{PageId}")]
        public async Task<Page> Page(Guid PageId)
        {
            try
            {
                return await _pageService.GetPage(PageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("content/{contentId}")]
        public async Task<Content> Section(Guid contentId)
        {
            try
            {
                return await _pageService.GetContent(contentId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("detailed")]
        public async Task<List<Page>> GetDetailPages()
        {
            return await _pageService.GetDetailPages();
        }
    }
}
