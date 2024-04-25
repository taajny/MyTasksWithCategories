using MyTasks.Core.Repositories;
using MyTasks.Persistence.Repositories;
using MyTasksWithCategories.Core.Repositories;

namespace MyTasks.Core
{
    public interface IUnitOfWork
    {
        ITaskRepository Task { get; }
        ICategoryRepository Category { get; }

        void Complete();
    }
}
