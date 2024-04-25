using Microsoft.EntityFrameworkCore;
using MyTasks.Core.Models.Domains;
using System.Collections.ObjectModel;

namespace MyTasksWithCategories.Core.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories(string userId);
        IEnumerable<Category> GetCategories();
        Category Get(int id, string userId);
        void AddCategory(string name, string userId);

        void Delete(int id, string userId);

        void Update(Category category);
    }
}
