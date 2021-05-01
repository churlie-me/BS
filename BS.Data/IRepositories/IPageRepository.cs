﻿using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IRepositories
{
    public interface IPageRepository
    {
        Task<Page> GetPage(Guid PageId);
        Task<bool> AddOrUpdate(Page Page);
        Task<dboPages> Get(dboPages dboPages);
        Task<Content> GetContent(Guid ContentId);
        Task<List<Page>> GetDetailPages();
    }
}
