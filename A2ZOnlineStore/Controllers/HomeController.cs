using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A2ZOnlineStore.Data.Interfaces;
using A2ZOnlineStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace A2ZOnlineStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _ProductRepository;
        public HomeController(IProductRepository ProductRepository)
        {
            _ProductRepository = ProductRepository;
        }

        public ViewResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                PreferredProducts = _ProductRepository.PreferredProducts
            };
            return View(homeViewModel);
        }

        public ViewResult About()
        {
            return View();
        }
    }
}