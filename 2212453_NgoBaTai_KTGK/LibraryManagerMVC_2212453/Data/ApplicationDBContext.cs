using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
namespace Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Books> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>().HasData(
                new Categories { CategoryId = 1, CategoryName = "Lịch sử", Description ="aaaaa"},
                new Categories { CategoryId = 2, CategoryName = "Xã hội", Description = "bbbbbb" },
                new Categories { CategoryId = 3, CategoryName = "Lập trình", Description = "programming book" }
            );

            modelBuilder.Entity<Books>().HasData(
                new Books { BookId = 1, Title = "History", Author = "Nguyễn Văn A", ISBN = "aaa", PublishedYear= 1900, CategoryID = 1},
                new Books { BookId = 2, Title = "Agriculture", Author = "Nguyễn Văn B", ISBN = "bbb", PublishedYear = 2000, CategoryID = 2 },
                new Books { BookId = 3, Title = "C# master", Author = "Nguyễn Văn C", ISBN = "ccc", PublishedYear = 2009, CategoryID = 3 },
                new Books { BookId = 4, Title = "Cấu trúc dữ liệu và giải thuật", Author = "Nguyễn Văn D", ISBN = "ddd", PublishedYear = 1950, CategoryID = 3 },
                new Books { BookId = 5, Title = "EEEEE", Author = "Nguyễn Văn E", ISBN = "eee", PublishedYear = 1920, CategoryID = 1 }
            );
        }
    }
}
