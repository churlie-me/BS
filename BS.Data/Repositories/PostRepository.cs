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
    public class PostRepository : IPostRepository
    {
        private BSDBContext _bsdbContext;
        public PostRepository(BSDBContext _bsdbContext)
        {
            this._bsdbContext = _bsdbContext;
        }

        public async Task<bool> AddOrModify(Post post)
        {
            try
            {
                if (_bsdbContext.Post.Any(h => h.Id == post.Id))
                    _bsdbContext.Entry(post).State = EntityState.Modified;
                else
                    _bsdbContext.Entry(post).State = EntityState.Added;

                return await _bsdbContext.SaveChangesAsync() > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Post> Post(Guid id)
        {
            try
            {
                return await _bsdbContext.Post.Include(b => b.Account).Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<dboPost> Posts(dboPost dboPost)
        {
            if (string.IsNullOrEmpty(dboPost.search))
            {
                dboPost.Posts = await _bsdbContext.Post.Include(b => b.Account)
                                                 .Where(x => !x.Deleted)
                                                 .Skip(dboPost.pageSize * (dboPost.page - 1))
                                                 .Take(dboPost.pageSize)
                                                 .ToListAsync();

                dboPost.pageCount = (int)Math.Ceiling((double)_bsdbContext.Post.Where(x => !x.Deleted).Count() / dboPost.pageSize);
            }
            else
            {
                dboPost.Posts = await _bsdbContext.Post.Include(b => b.Account)
                                                 .Where(y => y.Title.Contains(dboPost.search) && !y.Deleted)
                                                 .Skip(dboPost.pageSize * (dboPost.page - 1))
                                                 .Take(dboPost.pageSize)
                                                 .ToListAsync();

                dboPost.pageCount = (int)Math.Ceiling((double)_bsdbContext.Job.Where(y => y.Title.Contains(dboPost.search) && !y.Deleted).Count() / dboPost.pageSize);
            }

            return dboPost;
        }
    }
}
