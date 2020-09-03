using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;

namespace WebEMA.Controllers
{
    public class EntriesController : Controller
    {
        private readonly IEntityManager<Entry> entityManager;

        public EntriesController(IEntityManager<Entry> entityManager)
        {
            this.entityManager = entityManager;
        }

        public IActionResult Index()
        {
            return View(entityManager.Get());
        }
    }
}
