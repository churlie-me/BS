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
    public class PageRepository : IPageRepository
    {
        private BSDBContext _bsdbContext;
        public PageRepository(BSDBContext _bsdbContext)
        {
            this._bsdbContext = _bsdbContext;
        }

        public async Task<bool> AddOrUpdate(Page Page)
        {
            try
            {
                if (_bsdbContext.Page.Any(p => p.Id == Page.Id))
                {
                    _bsdbContext.Entry(Page).State = EntityState.Modified;
                    //Rows
                    if(Page.Rows != null)
                        foreach (Row row in Page.Rows)
                            if (_bsdbContext.Row.Any(s => s.Id == row.Id && s.PageId == row.PageId))
                            {
                                _bsdbContext.Entry(row).State = EntityState.Modified;
                                //Columns
                                if(row.Columns != null)
                                    foreach (Column column in row.Columns)
                                        if (_bsdbContext.Column.Any(s => s.Id == column.Id && s.RowId == column.RowId))
                                        {
                                            AddOrUpdate(column);
                                        }
                            }
                            else
                                _bsdbContext.Row.Add(row);
                }
                else
                    _bsdbContext.Page.Add(Page);

                return await _bsdbContext.SaveChangesAsync() > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void AddOrUpdate(Column column)
        {
            try
            {
                if (_bsdbContext.Column.Any(s => s.Id == column.Id && s.RowId == column.RowId))
                {
                    _bsdbContext.Entry(column).State = EntityState.Modified;
                    //Sections
                    if(column.Contents != null)
                        foreach (Content content in column.Contents)
                            if (_bsdbContext.Content.Any(s => s.Id == content.Id && s.ColumnId == content.ColumnId))
                            {
                                _bsdbContext.Entry(content).State = EntityState.Modified;
                                if (content.Row != null)
                                {
                                    _bsdbContext.Entry(content.Row).State = EntityState.Modified;
                                    if(content.Row.Columns != null)
                                        foreach (var _column in content.Row.Columns)
                                            AddOrUpdate(_column);
                                }
                            }
                            else
                                _bsdbContext.Content.Add(content);
                }
                else
                    _bsdbContext.Column.Add(column);
            }
            catch(Exception ex)
            {

            }
        }

        public async Task<dboPages> Get(dboPages dboPages)
        {
            try
            {
                dboPages.Pages = await _bsdbContext.Page.Include(r => r.Rows)
                                                        .Where(o => !o.Deleted)
                                                        .Skip(dboPages.pageSize * (dboPages.page - 1))
                                                        .Take(dboPages.pageSize)
                                                        .ToListAsync();
                dboPages.pageCount = (int)Math.Ceiling((double)_bsdbContext.Page.Where(x => !x.Deleted).Count() / dboPages.pageSize);
                return dboPages;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Page> GetPage(Guid PageId)
        {
            try
            {
                return await _bsdbContext.Page.Include(o => o.Rows.OrderBy(c => c.Index))
                                              .ThenInclude(r => r.Columns.OrderBy(c => c.Index))
                                              .ThenInclude(c => c.Contents.OrderBy(c => c.Index))
                                              .ThenInclude(c => c.Row)
                                              .ThenInclude(r => r.Columns)
                                              .ThenInclude(c => c.Contents)
                                              .FirstOrDefaultAsync(o => o.Id == PageId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Page>> GetDetailPages()
        {
            try
            {
                return await _bsdbContext.Page.Include(o => o.Rows.OrderBy(c => c.Index))
                                              .ThenInclude(r => r.Columns.OrderBy(c => c.Index))
                                              .ThenInclude(c => c.Contents.OrderBy(c => c.Index))
                                              .ThenInclude(c => c.Row)
                                              .ThenInclude(r => r.Columns)  
                                              .ThenInclude(c => c.Contents)
                                              .Where(o => !o.Deleted).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<Content> GetContent(Guid ContentId)
        {
            throw new NotImplementedException();
        }
    }
}
