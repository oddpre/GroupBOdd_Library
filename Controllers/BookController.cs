using BookLibrarySystemB.Data;
using BookLibrarySystemB.Models;
using BookLibrarySystemB.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookLibrarySystemB.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public BookController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel viewModel)
        {
            var book = new Book
            {
                BookTitle = viewModel.BookTitle,
                Genre = viewModel.Genre,
                Author = viewModel.Author,
            };
            //Handle file upload.
            if (viewModel.CoverImage != null)
            {
                using var memoryStream = new MemoryStream();
                await viewModel.CoverImage.CopyToAsync(memoryStream);
                book.CoverImage = memoryStream.ToArray();
            }
            //PDF file upload handle
            if (viewModel.PdfFile != null)
            {
                using var memoryStream = new MemoryStream();
                await viewModel.PdfFile.CopyToAsync(memoryStream);
                book.EBook = memoryStream.ToArray();
                book.BookName = viewModel.PdfFile.FileName;
            }
            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();
            // Corrected RedirectToAction
            return RedirectToAction("List", "Book");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var books = await dbContext.Books.ToListAsync();
            return View(books);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddBookViewModel viewModel)
        {
            var book = await dbContext.Books.FindAsync(viewModel.Id);
            if (book is not null)
            {
                book.BookTitle = viewModel.BookTitle;
                book.Genre = viewModel.Genre;
                book.Author = viewModel.Author;

                // Check if a new cover image was uploaded.
                if (viewModel.CoverImage != null)
                {
                    using var memoryStream = new MemoryStream();
                    await viewModel.CoverImage.CopyToAsync(memoryStream);
                    book.CoverImage = memoryStream.ToArray();
                }

                //Check if a new book was uploaded:
                if (viewModel.PdfFile != null)
                {
                    using var memoryStream = new MemoryStream();
                    await viewModel.PdfFile.CopyToAsync(memoryStream);
                    book.EBook = memoryStream.ToArray();
                    book.BookName = viewModel.PdfFile.FileName;
                }

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Book");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var book = await dbContext.Books.FindAsync(id);
            //if (book == null)
            //{
            //    return NotFound();
            //}

            var viewModel = new AddBookViewModel
            {
                Id = book.Id,
                BookTitle = book.BookTitle,
                Genre = book.Genre,
                Author = book.Author,
                ExistingCoverImage = book.CoverImage,
                ExistingEBook = book.EBook, //Existing Book
                ExistingPdfFileName = book.BookName
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Book viewModel)
        {
            var book = await dbContext.Books.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);
            if (book is not null)
            {
                dbContext.Books.Remove(book);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Book");
        }

        [HttpGet]
        public async Task<IActionResult> DownloadPdf(Guid id)
        {
            var book = await dbContext.Books.FindAsync(id);
            if (book == null || book.EBook == null)
            {
                return NotFound();
            }
            return File(book.EBook, "application/pdf", book.BookName);
        }
        [HttpGet]
        public async Task<IActionResult> PreviewPdf(Guid id)
        {
            var book = await dbContext.Books.FindAsync(id);
            if (book == null || book.EBook == null)
            {
                return NotFound();
            }
            return File(book.EBook, "application/pdf");
        }


    }
}
