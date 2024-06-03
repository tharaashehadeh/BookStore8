using BookStore.Data;
using BookStore.Models;
using BookStore.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Controllers
{    
   
    public class BookController : Controller
    {
        private readonly ApplicationDbContext context;

        public  BookController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
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
            var book = new Book
            {
                Title = viewModel.Title,
                AuthorId = viewModel.AuthorId,
                Publisher = viewModel.Publisher,
                publishDate = viewModel.publishDate,
                Description = viewModel.Description,
                Categories = viewModel.SelectedCategories.Select(id => new BookCategory
                {
                    CatrgoryId = id,
                }).ToList(),
            };
            context.Books.Add(book);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
