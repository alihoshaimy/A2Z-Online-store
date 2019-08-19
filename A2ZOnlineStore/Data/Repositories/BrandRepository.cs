using A2ZOnlineStore.Data.Interfaces;
using A2ZOnlineStore.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A2ZOnlineStore.Data.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly AppDbContext _appDbContext;
        public BrandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public bool AddBrand(Brand brand)
        {
            _appDbContext.Brands.Add(brand);
            return _appDbContext.SaveChanges() > 0;
        }

        public IEnumerable<Brand> Brands => _appDbContext.Brands;

       
    }
}
