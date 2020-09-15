using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;
using System.Collections.Generic;
using System.Linq;

namespace WebEMA.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEntityManager<Position> positionManager;
        private readonly IEntityManager<CatalogItem> catalogManager;

        IEnumerable<Position> positions;

        public HomeController(IEntityManager<Position> positionManager, IEntityManager<CatalogItem> catalogManager)
        {
            this.positionManager = positionManager;
            this.catalogManager = catalogManager;

            positions = positionManager.Get();
        }

        public IActionResult Index()
        {
            ViewBag.PostionsTreeVM = positions;
            ViewBag.CatalogVM = catalogManager.Get();
            return View();
        }

        public IActionResult PositionData(int id)
        {
            Position pos = positions.FirstOrDefault(p => p.Id == id);
            return PartialView("../Partial/_PositionData", pos);
        }
    }
}
