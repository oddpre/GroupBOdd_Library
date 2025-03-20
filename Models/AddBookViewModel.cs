using System.ComponentModel.DataAnnotations;
namespace BookLibrarySystemB.Models
{
    public class AddBookViewModel
    {
        public Guid Id { get; set; }
        public string BookTitle { get; set; }
        public string Genre { get; set; }
        //public string AccessType { get; set; }
        public string Author { get; set; }
        //Cover Image
        [Display(Name = "CoverImage")]
        public IFormFile? CoverImage { get; set; }//File.
        //Holds the image for display
        public byte[]? ExistingCoverImage { get; set; }
        // PDF upload
        public IFormFile? PdfFile { get; set; } //For PDF upload
        public string? ExistingPdfFileName { get; set; } //For displaying the current PDF attached to the entry.
        public byte[]? ExistingEBook { get; set; } //Displays current EBook version
    }
}
