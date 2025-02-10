using GPS.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace GPS.DBContext{

    public class AppDBContext : DbContext{
        public AppDBContext()
        {

        }
        public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserModel>().ToCollection("Users");
        }
    }
}