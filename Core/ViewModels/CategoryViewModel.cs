using MyTasks.Core.Models.Domains;

namespace MyTasksWithCategories.Core.ViewModels
{
    public class CategoryViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}
