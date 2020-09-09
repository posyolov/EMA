using Microsoft.AspNetCore.Mvc;
using Model;

namespace WebEMA.Controllers
{
    public class GenericController<TEntity> : Controller where TEntity : class
    {
        private readonly IEntityManager<TEntity> entityManager;

        public GenericController(IEntityManager<TEntity> entityManager)
        {
            this.entityManager = entityManager;
        }

        public IActionResult Index()
        {
            return View(entityManager.Get());
        }
    }
}
