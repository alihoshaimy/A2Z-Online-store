using A2ZOnlineStore.Data.Interfaces;
using A2ZOnlineStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A2ZOnlineStore.Data.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Product> Products => _appDbContext.Products.Include(c => c.Category);

        public IEnumerable<Product> PreferredProducts => _appDbContext.Products.Where(p => p.IsPreferredProduct).Include(c => c.Category);

        public Product GetProductById(int ProductId) => _appDbContext.Products.FirstOrDefault(p => p.ProductId == ProductId);
        public bool AddProduct(Product product)
        {
            _appDbContext.Products.Add(product);
            return _appDbContext.SaveChanges() > 0;
        }
    }
}

