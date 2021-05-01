using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IServices
{
    public interface IPostService
    {
        Task<bool> AddOrModify(Post post);
        Task<Post> Post(Guid id);
        Task<dboPost> Posts(dboPost dboPost);
    }
}
