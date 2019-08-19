using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A2ZOnlineStore.Data.Interfaces;
using A2ZOnlineStore.Data.Models;
using A2ZOnlineStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace A2ZOnlineStore.Controllers
{
    [Route("api/[controller]")]
    public class ProductDataController : Controller
    {
        private readonly IProductRepository _ProductRepository;

        public ProductDataController(IProductRepository ProductRepository)
        {
            _ProductRepository = ProductRepository;
        }

        [HttpGet]
        public IEnumerable<ProductViewModel> LoadMoreProducts()
        {
            IEnumerable<Product> dbProducts = null;

            dbProducts = _ProductRepository.Products.OrderBy(p => p.ProductId).Take(10);

            List<ProductViewModel> Products = new List<ProductViewModel>();

            foreach (var dbProduct in dbProducts)
            {
                Products.Add(MapDbProductToProductViewModel(dbProduct));
            }
            return Products;
        }

        private ProductViewModel MapDbProductToProductViewModel(Product dbProduct) => new ProductViewModel()
        {
            ProductId = dbProduct.ProductId,
            Name = dbProduct.Name,
            Price = dbProduct.Price,
            ShortDescription = dbProduct.ShortDescription,
            ImageThumbnailUrl = dbProduct.ImageThumbnailUrl
        };

    }
}