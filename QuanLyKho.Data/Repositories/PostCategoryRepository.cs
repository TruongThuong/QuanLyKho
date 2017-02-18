using Data.Infrastructure;
using QuanLyKho.Data.Infrastructure;
using QuanLyKho.Model.Models;

namespace QuanLyKho.Data.Repositories
{
    public interface IPostCategoryRepository : IRepository<PostCategory>
    {
    }

    public class PostCategoryRepository : RepositoryBase<PostCategory>, IPostCategoryRepository
    {
        public PostCategoryRepository(DbFactory dbContext) : base(dbContext)
        {
        }
    }
}