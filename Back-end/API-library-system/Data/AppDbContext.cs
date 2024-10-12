using API_library_system.Models;
using Microsoft.EntityFrameworkCore;

namespace API_library_system.Data
{
	public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
	{
		public DbSet<Reservation> Reservations { get; set; }
		public DbSet<LibraryItem> LibraryItems { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Book>();
			modelBuilder.Entity<Audiobook>();

			modelBuilder.Entity<Reservation>().HasKey(r => r.Id);
			modelBuilder.Entity<LibraryItem>().HasKey(b => b.Id);

			modelBuilder.Entity<Reservation>().HasOne(e => e.SelectedItem);
		}
	}
}
