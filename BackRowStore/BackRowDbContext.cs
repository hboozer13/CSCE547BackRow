using System;
using Microsoft.EntityFrameworkCore;
using BackRowStore;

public class BackRowDbContext : DbContext
{
	public DbSet<Item> Items { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if(!optionsBuilder.IsConfigured)
		{
            optionsBuilder.UseSqlServer("Server=DESKTOP-9P05B39\\SQLEXPRESS;Database=BackRowStore;User Id=hboozer;Password=1Beginning;");
        }
	}
}
