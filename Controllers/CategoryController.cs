using Microsoft.AspNetCore.Mvc;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.ViewModels;
using MyTasks.Persistence;
using MyTasks.Persistence.Extensions;
using MyTasksWithCategories.Core.ViewModels;
using MyTasksWithCategories.Persistence.Repositories;
using System.Threading.Tasks;

namespace MyTasksWithCategories.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryRepository _categoryRepository;
        public CategoryController(ApplicationDbContext context)
        {
            _categoryRepository = new CategoryRepository(context);
        }
        public IActionResult Categories()
        {
            var userId = User.GetUserId();
            var categories = _categoryRepository.GetCategories(userId);
            
            if ( categories == null || !categories.Any())
            {
                _categoryRepository.AddCategory("Ogólna", userId);
            }

            var vm = new CategoriesViewModel { Categories = categories };

            return View(vm);
        }

        public IActionResult Category(int id = 0)
        {
            var userId = User.GetUserId();
            
            var category = id == 0 ?
                new Category { Id = 0, UserId = userId } :
                _categoryRepository.Get(id, userId);

            var vm = new CategoryViewModel
            {
                Category = category,
                Heading = id == 0 ? "Dodawanie nowej kategorii" : "Edycja kategorii",
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Category(Category category) 
        {
            var userId = User.GetUserId();
            category.UserId = userId;

            if (!ModelState.IsValid)
            {
                var vm = new CategoryViewModel
                {
                    Category = category,
                    Heading = category.Id == 0 ? "Dodawanie nowej kategorii" : "Edycja kategorii"
                };

                return View("Category", vm);
            }

            if ( category.Id == 0)
            {
                _categoryRepository.AddCategory(category.Name, userId);
            }
            else
            {
                _categoryRepository.Update(category);
            }
            return RedirectToAction("Categories");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _categoryRepository.Delete(id, userId);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });

            }
            return Json(new { success = true });
        }

    }
}
