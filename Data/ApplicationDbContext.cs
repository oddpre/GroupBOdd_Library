using BookLibrarySystemB.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookLibrarySystemB.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        public DbSet<Book> Books { get; set; }
    }
}
