using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;

namespace WebEMA.Controllers
{
    public class VendorsController : Controller
    {
        private readonly IEntityManager<Vendor> entityManager;

        public VendorsController(IEntityManager<Vendor> entityManager)
        {
            this.entityManager = entityManager;
        }

        public IActionResult Index()
        {
            return View(entityManager.Get());
        }
    }
}
