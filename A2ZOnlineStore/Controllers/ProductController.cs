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
    public class ProductController : Controller
    {
        private readonly IProductRepository _ProductRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;

        public ProductController(IProductRepository ProductRepository, ICategoryRepository categoryRepository, IBrandRepository brandRepository)
        {
            _ProductRepository = ProductRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
        }

        public ViewResult List(string category = null)
        {
            string _category = category;
            IEnumerable<Product> Products;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(category))
            {
                Products = _ProductRepository.Products.OrderBy(p => p.ProductId);
                currentCategory = "All Products";
            }
            else
            {
                Products = _ProductRepository.Products.Where(p => p.Category.CategoryId.ToString() == category).OrderBy(p => p.Name);
                currentCategory = _category;
            }

            return View(new ProductsListViewModel
            {
                Products = Products,
                CurrentCategory = currentCategory
            });
        }

        public ViewResult Search(string searchString)
        {
            string _searchString = searchString;
            IEnumerable<Product> Products;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(_searchString))
            {
                Products = _ProductRepository.Products.OrderBy(p => p.ProductId);
            }
            else
            {
                Products = _ProductRepository.Products.Where(p => p.Name.ToLower().Contains(_searchString.ToLower()));
            }

            return View("~/Views/Product/List.cshtml", new ProductsListViewModel { Products = Products, CurrentCategory = "All Products" });
        }

        public ViewResult Details(int ProductId)
        {
            var Product = _ProductRepository.Products.FirstOrDefault(d => d.ProductId == ProductId);
            if (Product == null)
            {
                return View("~/Views/Error/Error.cshtml");
            }
            return View(Product);
        }

        public ViewResult AddProduct(int ProductId = 0)
        {
            ViewBag.Categories = _categoryRepository.Categories;
            ViewBag.brand = _brandRepository.Brands;
            Product model = new Product();
            if (ProductId == 0)
            {
                return View(model);
            }
            model = _ProductRepository.GetProductById(ProductId);
            if(model == null)
                return View(new Product());
            return View(model);
        }

        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _categoryRepository.Categories;
                ViewBag.brand = _brandRepository.Brands;
                return View(product);
            }
           _ProductRepository.AddProduct(product);

            return RedirectToAction(nameof(List));
        }


        public ViewResult AddBrand()
        {            
            return View(new Brand());
        }

        [HttpPost]
        public ActionResult AddBrand(Brand brand)
        {
            if (!ModelState.IsValid)
            {
                return View(brand);
            }
            _brandRepository.AddBrand(brand);
            return RedirectToAction(nameof(List));
        }

        public ViewResult AddCategory()
        {
            return View(new Category());
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _categoryRepository.AddCategory(category);
            return RedirectToAction(nameof(List));
        }
    }
}