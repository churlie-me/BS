using BS.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.IRepositories
{
    public interface IPostRepository
    {
        Task<bool> AddOrModify(Post post);
        Task<Post> Post(Guid id);
        Task<dboPost> Posts(dboPost dboPost);
    }
}
