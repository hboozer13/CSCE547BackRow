using System;
using Microsoft.EntityFrameworkCore;
using BackRowStore;

public class BackRowDbContext : DbContext
{
	private const string ConnectionString = "Server=localhost;PORT=5432;Database=backrowreal;User Id=hboozer;Password=Hayden10;";

    public DbSet<Item> Items { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);
		optionsBuilder.UseNpgsql(ConnectionString);
	}
}
