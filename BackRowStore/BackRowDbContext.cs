using System;
using Microsoft.EntityFrameworkCore;
using BackRowStore;

namespace BackRowStore
{
    public class BackRowDbContext : DbContext
    {
        public BackRowDbContext(DbContextOptions<BackRowDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }

        public DbSet<Item> Items { get; set; }
    }
}

