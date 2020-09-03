using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;

namespace WebEMA.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IEntityManager<CatalogItem> entityManager;

        public CatalogController(IEntityManager<CatalogItem> entityManager)
        {
            this.entityManager = entityManager;
        }

        public IActionResult Index()
        {
            return View(entityManager.Get());
        }
    }
}
