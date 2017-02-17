using System;

namespace QuanLyKho.Data.Infrastructure
{
    public interface IDbFactory:IDisposable
    {
        QuanLyKhoDbContext Init();
    }
}