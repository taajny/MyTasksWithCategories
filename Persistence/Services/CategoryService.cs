using Microsoft.EntityFrameworkCore;
using MyTasks.Core;
using MyTasks.Core.Models.Domains;
using MyTasksWithCategories.Core.Service;
using System.Collections.ObjectModel;

namespace MyTasksWithCategories.Persistence.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Category> GetCategories(string userId)
        {
            return _unitOfWork.Category.GetCategories(userId);
        }
        public IEnumerable<Category> GetCategories()
        {
            return _unitOfWork.Category.GetCategories();
        }
        public Category Get(int id, string userId)
        {
            return _unitOfWork.Category.Get(id, userId);
        }

        public void AddCategory(string name, string userId)
        {
            _unitOfWork.Category.AddCategory(name, userId);
            _unitOfWork.Complete();
        }

        public void Delete(int id, string userId)
        {
            _unitOfWork.Category.Delete(id, userId);
            _unitOfWork.Complete();
        }

        public void Update(Category category)
        {
            _unitOfWork.Category.Update(category);
            _unitOfWork.Complete();
        }
    }
}
