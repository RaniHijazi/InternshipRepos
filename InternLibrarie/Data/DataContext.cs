using InternshipLibrarie.Models;
using Microsoft.EntityFrameworkCore;

namespace InternshipLibrarie.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Users> users { get; set; }
        public DbSet<Books> books { get; set; }
        
        public DbSet<Subscriptions> subscriptions { get; set; }
        public DbSet<Bills> bills { get; set; }
        
        public DbSet<BorrowedBooks> borrowedbooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BorrowedBooks>()
                    .HasKey(pc => new { pc.id });
            modelBuilder.Entity<BorrowedBooks>()
                    .HasOne(p => p.User)
                    .WithMany(pc => pc.BorrowedBooks)
                    .HasForeignKey(p => p.user_id);
            modelBuilder.Entity<BorrowedBooks>()
                    .HasOne(p => p.Book)
                    .WithMany(pc => pc.BorrowedBooks)
                    .HasForeignKey(c => c.book_id);
            modelBuilder.Entity<Users>()
                .HasOne(u => u.subscription)
                .WithOne(s => s.User)
                .HasForeignKey<Subscriptions>(s => s.user_id);

        }
    }
}

