using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using BookStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;


namespace BookStore.Controllers
{
    public class BookController : Controller
    {

        private readonly BookRepository _bookRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(BookRepository bookRepository,
            IWebHostEnvironment webHostEnvironment
            )
        {
            _bookRepository = bookRepository;
            _webHostEnvironment = webHostEnvironment;

        }

        public async Task<ViewResult> GetAllBooks()
        {
            var data =await  _bookRepository.GetAllBooks();
            return View(data);
        }

        public async  Task<ViewResult> GetBook(int id)
        {
            var data = await _bookRepository.GetBookById(id);
            return View(data);
        }

        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName);
        }
        public ViewResult AddNewBook(bool isSucess = false, int bookId = 0)
        {
            ViewBag.IsSucess = isSucess;
            ViewBag.BookId = bookId;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                if(bookModel.CoverPhoto !=null)
                {
                    string folder = "books/cover";
                   bookModel.CoverPhotoUrl= await UploadPhoto(folder, bookModel.CoverPhoto);
                }

                if (bookModel.SamplePhoto != null)
                {
                    string folder = "books/photos";

                    bookModel.Photos = new List<PhotosModel>();

                    foreach (var file in bookModel.SamplePhoto)
                    {
                        var photos = new PhotosModel()
                        {
                            Name = file.FileName,
                            URL = await UploadPhoto(folder, file)
                        };
                        bookModel.Photos.Add(photos);
                        
                    }


                }


                int id = await _bookRepository.AddNewBook(bookModel);

                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSucess = true, bookId = id });
                }
            }
            

            return View();
        }



        [HttpGet]
       

        public async Task<ViewResult> DeleteBook(int id)
        {
            var data = await _bookRepository.GetBookById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            _bookRepository.DeleteBook(id);
            _bookRepository.Save();
            return RedirectToAction("Index", "Home");
        }



    private async Task<string> UploadPhoto(string folderPath, IFormFile file)
        {
           
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
            
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }
    }
}
