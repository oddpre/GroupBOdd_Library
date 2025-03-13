using Microsoft.EntityFrameworkCore;

namespace BookLibrary_GroupB_Odd.Data
{
    public class BookDBContext: DbContext
    {
        public BookDBContext(DbContextOptions<BookDBContext> options): base(options)
        {
            
        }
    }
}
