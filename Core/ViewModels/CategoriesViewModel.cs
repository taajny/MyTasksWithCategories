using MyTasks.Core.Models.Domains;

namespace MyTasksWithCategories.Core.ViewModels
{
    public class CategoriesViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}