using BookStore.Data;
using BookStore.Models;
using BookStore.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AuthorController : Controller
    {
        public readonly ApplicationDbContext context;

        public AuthorController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var authors = context.Authors.ToList();
            var authorsVM = new List<AuthorVM>();

            foreach (var author in authors)
            {
                var authorVM = new AuthorVM()
                {
                    Id = author.Id,
                    Name = author.Name,
                    CreatedOn = author.CreatedOn,
                    UpdatedOn = author.UpdatedOn,
                };
                authorsVM.Add(authorVM);
            }
            return View(authorsVM);
        }
        public IActionResult Create()
        {
            return View("Form");
        }
        [HttpPost]
        public IActionResult Create(AuthorFormVM authorFormVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", authorFormVM);

            }
            var author = new Author
            {
                Name = authorFormVM.Name,
            };
            try
            {
                context.Authors.Add(author);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("Name", "category name already Exists");
                return View(authorFormVM);
            }

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {

            var author = context.Authors.Find(id);
            if (author is null)
            {
                return NotFound();
            }
            var viewModel = new AuthorFormVM
            {
                Id = id,
                Name = author.Name,


            };
            return View("Form", viewModel);
        }
        [HttpPost]
        public IActionResult Edit(AuthorFormVM authorformVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", authorformVM);
            }
            var author = context.Authors.Find(authorformVM.Id);

            if (author is null)
            {
                return NotFound();
            }
            author.Name = authorformVM.Name;

            author.UpdatedOn = DateTime.Now;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var author = context.Authors.Find(id);
            if (author is null)
            {
                return NotFound();
            }
            var viewModel = new AuthorVM
            {
                Id = author.Id,
                Name = author.Name,
                CreatedOn = author.CreatedOn,
                UpdatedOn = author.UpdatedOn,
            };
            return View(viewModel);
        }
        public IActionResult Delete(int id)
        {
            var author = context.Authors.Find(id);
            if (author is null)
            {
                return NotFound();
            }
            context.Authors.Remove(author);
            context.SaveChanges(true);

            return Ok();
        }

        public IActionResult CheckName(AuthorVM authorVM)
        {
            var isExists = context.Authors.Any(author => author.Name == authorVM.Name);
            return Json(!isExists);
        }
    }

}
