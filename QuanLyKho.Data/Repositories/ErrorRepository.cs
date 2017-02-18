using Data.Infrastructure;
using QuanLyKho.Data.Infrastructure;
using QuanLyKho.Model.Models;

namespace QuanLyKho.Data.Repositories
{
    public interface IErrorRepository : IRepository<Error>
    {
    }

    public class ErrorRepository : RepositoryBase<Error>,IErrorRepository
    {
        public ErrorRepository(DbFactory dbFactory):base(dbFactory)
        {

        }
    }
}