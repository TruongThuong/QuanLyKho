using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyKho.Model.Models;

namespace QuanLyKho.Data
{
    public class QuanLyKhoDbContext : DbContext
    {
        public QuanLyKhoDbContext(DbContextOptions<QuanLyKhoDbContext> options) : base(options)
        {

            //this.Configuration.LazyLoadingEnabled = false;
        }

        public QuanLyKhoDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("DefaultConnection");
            base.OnConfiguring(builder);
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Error> Errors { get; set; }

    }
}
