using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Model;
using BookStore.Repository;
using System.Threading.Tasks;
using BookStore.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class BookRepository
    {
        private readonly BookStoreContext _context = null;


        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<int> AddNewBook (BookModel model)
        {
            var newBook = new Books()
            {
                Author = model.Author,
                CreatedOn = DateTime.UtcNow,
                Description = model.Description,
                Language = model.Language,
                Title = model.Title,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                UpdatedOn = DateTime.UtcNow,
                CoverPhotoUrl =model.CoverPhotoUrl,
             
            };

            newBook.bookPhotos = new List<BookPhotos>();
            foreach(var file in model.Photos)
            {
                newBook.bookPhotos.Add(new BookPhotos()
                {
                    Name= file.Name,
                    URL = file.URL,
                     
                });

            }

           await  _context.Books.AddAsync(newBook);
           await  _context.SaveChangesAsync();

            return newBook.Id;
        }
        public async Task<List<BookModel>> GetAllBooks()
        {
            var books = new List<BookModel>();
            var allbooks = await _context.Books.ToListAsync();
            if(allbooks?.Any() == true)
            {
                foreach (var book in allbooks)
                {
                    books.Add(new BookModel()
                    {
                        Author= book.Author,
                        Category = book.Category,
                        Description = book.Description,
                        Id = book.Id,
                        Language = book.Language,
                        Title = book.Title,
                        TotalPages = book.TotalPages,
                        CoverPhotoUrl =book.CoverPhotoUrl
                    });

                }
            }
            return books;
        }

        public async Task< BookModel >GetBookById(int id)
        {
            return await _context.Books.Where(x => x.Id == id)
                .Select(book => new BookModel()
                {
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    Id = book.Id,
                    Language = book.Language,
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                    CoverPhotoUrl = book.CoverPhotoUrl,
                    Photos = book.bookPhotos.Select(p => new PhotosModel()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        URL = p.URL
                    }).ToList()
                 }).FirstOrDefaultAsync();
                
            
        }


        public  void DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null) _context.Books.Remove(book);

            //_context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        //    public async task<bookmodel> deletebook(int id)
        //    {
        //        return await _context.books.where(x => x.id == id)
        //               .select(book => new bookmodel()
        //               {
        //                     context.books.remove(book);
        //        await _context.savechangesasync();


        //    }
        //}


        public List<BookModel> SearchBook(string title, string authorName)
        {
            return DataSource().Where(x => x.Title.Contains(title) || x.Author.Contains( authorName)).ToList();

        }

        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel() {Id = 1, Title="Software Design Patterns", Author="Mahesh", Description="This is excellent book and helps to understand from very basics", Category="Design Pattern", Language="English", TotalPages=455},
                new BookModel() {Id= 2, Title="Csharp", Author="JK subedi", Description="To study and understand all basic concept of programming.", Category="Programming", Language="Nepali", TotalPages=800},
                new BookModel(){Id=3, Title="C++", Author="RK kandel",Description="Object Oriented programming can be learned with this book",Category="Programming", Language="English and Neplai", TotalPages=675},
                new BookModel(){Id=4, Title="Java", Author="A Bhusal",Description="Object Oriented programming in JAVA can be learned ",Category="Programming", Language="English", TotalPages=335 },
                new BookModel(){Id=5, Title="HTML & CSS ", Author="B Gautam",Description="HTML and CSS can be learned ", Category="Design", Language="English", TotalPages=250 }

            };
        }
    }
}
