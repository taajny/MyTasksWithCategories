using Microsoft.EntityFrameworkCore;
using MyTasks.Core;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Repositories;
using System.Threading.Tasks;
using Task = MyTasks.Core.Models.Domains.Task;

namespace MyTasks.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private IApplicationDbContext _context;
        public TaskRepository(IApplicationDbContext context) 
        { 
            _context = context;
        }
        public IEnumerable<Task> Get(string userId, bool isExecuted = false, int categoryId = 0, string title = null)
        {

             var tasks = _context.Tasks
                .Include(x => x.Category)
                .Where(x => x.UserId == userId && x.IsExecuted == isExecuted);

            if (categoryId != 0)
            {
                tasks = tasks.Where(x => x.CategoryId == categoryId); 
            }

            if (!string.IsNullOrWhiteSpace(title))
            {
                tasks = tasks.Where(x => x.Title.Contains(title));
            }

            return tasks.OrderBy(x => x.Term).ToList();
        }
        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.OrderBy(x => x.Name).ToList();
        }

        public Task Get(int id, string userId)
        {
            return _context.Tasks.Single(x => x.UserId == userId && x.Id == id);
        }

        public void Add(Task task)
        {
            _context.Tasks.Add(task);
        }

        public void Update(Task task)
        {
            var taskToUpdate = _context.Tasks.Single(x => x.Id == task.Id);

            taskToUpdate.CategoryId = task.CategoryId;
            taskToUpdate.Title = task.Title;
            taskToUpdate.Description = task.Description;
            taskToUpdate.IsExecuted = task.IsExecuted;
            taskToUpdate.Term = task.Term;
        }

        public void Delete(int id, string userId)
        {
            var taskToDelete = _context.Tasks.Single(x => x.Id == id && x.UserId == userId);

            _context.Tasks.Remove(taskToDelete);
        }

        public void Finish(int id, string userId)
        {
            var taskToFinish = _context.Tasks.Single(x => x.Id == id && x.UserId == userId);

            taskToFinish.IsExecuted = true;
        }
    }
}
