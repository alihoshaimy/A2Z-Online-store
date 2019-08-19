using A2ZOnlineStore.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace A2ZOnlineStore.Data.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        IEnumerable<Product> PreferredProducts { get; }
        Product GetProductById(int productId);
        bool AddProduct(Product product);
    }
}
