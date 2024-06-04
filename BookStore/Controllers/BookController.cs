using BookStore.Data;
using BookStore.Models;
using BookStore.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{    
   
    public class BookController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public  BookController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var bookVMs = context.Books.
                Include(book => book.Author).
                Include(book => book.Categories).
                ThenInclude(book=>book.category).
                ToList().Select(book => new BookVM
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author.Name,
                Publisher = book.Publisher,
                publishDate = book.publishDate,
                ImageUrl = book.ImageUrl,
                Categories = book.Categories.Select(book=>book.category.Name).ToList(),
                }).ToList();
            
            return View(bookVMs);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var authors    = context.Authors.OrderBy(author=>author.Name).ToList();
            var categories = context.Categories.OrderBy(author => author.Name).ToList();

            var authorList = new List<SelectListItem>();
            foreach(var author in authors)
            {
                authorList.Add(new SelectListItem
                {
                    Value = author.Id.ToString(),
                    Text = author.Name,
                });
            }
            var categoryList = new List<SelectListItem>();
            foreach (var category in categories)
            {
                categoryList.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name,
                });
            }
            var viewModel = new BookFormVM
            {
                Authors = authorList,
                Categories =categoryList,
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Create(BookFormVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            string ImageName = null;
            if(viewModel.ImageUrl != null)
            {
                ImageName = Path.GetFileName(viewModel.ImageUrl.FileName);
                var path = Path.Combine($"{webHostEnvironment.WebRootPath}/img/Books",ImageName);
                var stream = System.IO.File.Create(path);
                viewModel.ImageUrl.CopyTo(stream);
            }
            var book = new Book
            {
                Title = viewModel.Title,
                AuthorId = viewModel.AuthorId,
                Publisher = viewModel.Publisher,
                publishDate = viewModel.publishDate,
                Description = viewModel.Description,
                ImageUrl = ImageName,
                Categories = viewModel.SelectedCategories.Select(id => new BookCategory
                {
                    CatrgoryId = id,
                }).ToList(),
            };
            context.Books.Add(book);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var book = context.Books.Find(id);
            if(book is null)
            {
                return NotFound();
            }
            var path = Path.Combine(webHostEnvironment.WebRootPath,"img/Books", book.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);

            }
           

            context.Books.Remove(book);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
