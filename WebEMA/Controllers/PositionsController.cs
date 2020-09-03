using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;


namespace WebEMA.Controllers
{
    public class PositionsController : Controller
    {
        private readonly IEntityManager<Position> entityManager;

        public PositionsController(IEntityManager<Position> entityManager)
        {
            this.entityManager = entityManager;
        }

        public IActionResult Index()
        {
            return View(entityManager.Get());
        }
    }
}
