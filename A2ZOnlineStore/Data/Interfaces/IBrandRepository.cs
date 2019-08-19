using A2ZOnlineStore.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace A2ZOnlineStore.Data.Interfaces
{
    public interface IBrandRepository
    {
        bool AddBrand(Brand brand);
        IEnumerable<Brand> Brands { get; }
    }
}
