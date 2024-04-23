using Microsoft.AspNetCore.Mvc;
using MyTasks.Persistence;
using MyTasks.Persistence.Extensions;
using MyTasksWithCategories.Core.ViewModels;
using MyTasksWithCategories.Persistence.Repositories;

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

            var vm = new CategoryViewModel { Categories = categories };

            return View(vm);
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
