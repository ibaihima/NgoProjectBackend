using Microsoft.EntityFrameworkCore;

namespace NgoProject.Models
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		public DbSet<Ngo> NgoEvents{ get; set; }
		public DbSet<UserModel> Users { get; set; }
	}
}
