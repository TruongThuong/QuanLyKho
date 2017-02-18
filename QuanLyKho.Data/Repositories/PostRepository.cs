using System;
using System.Collections.Generic;
using System.Linq;
using Data.Infrastructure;
using QuanLyKho.Data.Infrastructure;
using QuanLyKho.Model.Models;

namespace QuanLyKho.Data.Repositories
{

    public interface IPostRepository : IRepository<Post>
    {
        IEnumerable<Post> GetAllByTag(string tag, int pageIndex, int pageSize, out int totalRow);
    }

    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(DbFactory dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Post> GetAllByTag(string tag, int pageIndex, int pageSize, out int totalRow)
        {
            var query = from p in DbContext.Posts
                        join pt in DbContext.PostTags
                        on p.ID equals pt.PostID
                        where pt.TagID == tag && p.Status == true
                        orderby p.CreatedDate descending
                        select p;
            totalRow = query.Count();
            query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return query;
        }
    }
}