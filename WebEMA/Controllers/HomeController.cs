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
        private readonly IEntityManager<Position> positionManager;
        private readonly IEntityManager<CatalogItem> catalogManager;

        public HomeController(IEntityManager<Position> positionManager, IEntityManager<CatalogItem> catalogManager)
        {
            this.positionManager = positionManager;
            this.catalogManager = catalogManager;
        }

        public IActionResult Index()
        {
            ViewBag.PostionsTreeVM = positionManager.Get();
            ViewBag.CatalogVM = catalogManager.Get();
            return View();
        }

    }
}
