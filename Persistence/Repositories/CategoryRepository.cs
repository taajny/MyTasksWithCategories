using Microsoft.EntityFrameworkCore;
using MyTasks.Core;
using Task = MyTasks.Core.Models.Domains.Task;
using MyTasks.Core.Models.Domains;
using MyTasks.Persistence;
using System.Collections.ObjectModel;

namespace MyTasksWithCategories.Persistence.Repositories
{
    public class CategoryRepository
    {
        private ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Category> GetCategories(string userId)
        {
            return _context.Categories.Where(x => x.UserId == userId).ToList();
        }

        public Category Get(int id, string userId) 
        { 
            return _context.Categories.Single(x => x.Id == id && x.UserId == userId);
        }

        public void AddCategory(string name, string userId)
        { 
            Category category = new Category { Name = name, UserId = userId, Tasks = new Collection<Task>() };
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Delete(int id, string userId)
        {
            var CategoryToDelete = _context.Categories.Single(x => x.Id == id && x.UserId == userId);

            _context.Categories.Remove(CategoryToDelete);
            _context.SaveChanges();

        }

        public void Update(Category category)
        {
            var categoryToUpdate = _context.Categories.Single(x => x.Id == category.Id);

            categoryToUpdate.Name = category.Name;
            categoryToUpdate.UserId = category.UserId;
            
            _context.Categories.Update(categoryToUpdate);
            _context.SaveChanges();
        }
    }
}
