namespace BookLibrarySystemB.Models.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string BookTitle { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        //public string AccessType { get; set; }

        //Cover image for the books.
        public byte[]? CoverImage { get; set; }

        //Books
        public byte[]? EBook { get; set; }
        public string? BookName { get; set; }

    }
}
