using System;
using Microsoft.EntityFrameworkCore;

namespace QuanLyKho.Data.Infrastructure
{
    public class DbFactory : Disposable,IDbFactory
    {
        private QuanLyKhoDbContext _dbcontext;
        public QuanLyKhoDbContext Init()
        {
            return _dbcontext ?? (_dbcontext = new QuanLyKhoDbContext());
        }

        protected override void DisposeCore()
        {
            if (_dbcontext!=null)
            {
                _dbcontext.Dispose();
            }
            
        }
    }
}