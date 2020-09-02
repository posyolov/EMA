using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;

namespace WebEMA.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEntityManager<CatalogItem> catalogManager;

        public HomeController(IEntityManager<CatalogItem> catalogManager)
        {
            this.catalogManager = catalogManager;
        }

        public IActionResult Index()
        {
            var catalog = catalogManager.Get();
            return View(catalog);
        }
    }
}
