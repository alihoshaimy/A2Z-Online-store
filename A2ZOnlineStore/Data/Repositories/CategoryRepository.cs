using A2ZOnlineStore.Data.Interfaces;
using A2ZOnlineStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A2ZOnlineStore.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;
        public CategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IEnumerable<Category> Categories => _appDbContext.Categories;
        public bool AddCategory(Category category)
        {
            _appDbContext.Categories.Add(category);
            return _appDbContext.SaveChanges() > 0;
        }
    }
}
