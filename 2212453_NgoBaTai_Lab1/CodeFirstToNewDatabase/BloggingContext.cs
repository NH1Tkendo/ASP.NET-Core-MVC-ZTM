using Microsoft.EntityFrameworkCore;

namespace CodeFirstToNewDatabase
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=WIN-94BURSUISF0;Database=BloggingDB;User Id=sa;Password=1711;Encrypt=False;TrustServerCertificate=True;");
        }
    }
}
