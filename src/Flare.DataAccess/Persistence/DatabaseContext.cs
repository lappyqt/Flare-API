using Microsoft.EntityFrameworkCore;

namespace Flare.DataAccess.Persistence;

public class DatabaseContext : DbContext
{
    public DbSet<Account>? Accounts { get; set; }
    public DbSet<Post>? Posts { get; set; }
    public DbSet<Comment>? Comments { get; set; }
    public DbSet<Category>? Categories { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().HasKey(x => x.Id);
        modelBuilder.Entity<Post>().OwnsOne(x => x.Urls);

        modelBuilder.Entity<Account>().HasIndex(x => x.Email).IsUnique();
        modelBuilder.Entity<Account>().HasIndex(x => x.Username).IsUnique();

        modelBuilder.Entity<Category>().HasIndex(x => x.Name).IsUnique();
    }
}