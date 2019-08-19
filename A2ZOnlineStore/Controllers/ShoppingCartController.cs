using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A2ZOnlineStore.Data.Interfaces;
using A2ZOnlineStore.Data.Models;
using A2ZOnlineStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace A2ZOnlineStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository _ProductRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IProductRepository ProductRepository, ShoppingCart shoppingCart)
        {
            _ProductRepository = ProductRepository;
            _shoppingCart = shoppingCart;
        }

        [Authorize]
        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(shoppingCartViewModel);
        }

        [Authorize]
        public RedirectToActionResult AddToShoppingCart(int ProductId)
        {
            var selectedProduct = _ProductRepository.Products.FirstOrDefault(p => p.ProductId == ProductId);
            if (selectedProduct != null)
            {
                _shoppingCart.AddToCart(selectedProduct, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int ProductId)
        {
            var selectedProduct = _ProductRepository.Products.FirstOrDefault(p => p.ProductId == ProductId);
            if (selectedProduct != null)
            {
                _shoppingCart.RemoveFromCart(selectedProduct);
            }
            return RedirectToAction("Index");
        }

    }

}