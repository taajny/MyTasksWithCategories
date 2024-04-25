using MyTasks.Core.Models.Domains;

namespace MyTasksWithCategories.Core.Service
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories(string userId);
        IEnumerable<Category> GetCategories();
        Category Get(int id, string userId);
        void AddCategory(string name, string userId);
        void Delete(int id, string userId);
        void Update(Category category);
    }
}
