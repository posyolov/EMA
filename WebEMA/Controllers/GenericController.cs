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

        public IActionResult Items()
        {
            return View(entityManager.Get());
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult CreatePost(TEntity entity)
        {
            entityManager.Add(entity);
            return View("Items", entityManager.Get());
        }

        public IActionResult Update(TEntity entity)
        {
            return View(entity);
        }

        public IActionResult UpdatePost(TEntity entity)
        {
            entityManager.Update(entity);
            return View("Items", entityManager.Get());
        }

        public IActionResult Delete(TEntity entity)
        {
            return View(entity);
        }

        public IActionResult DeletePost(TEntity entity)
        {
            entityManager.Delete(entity);
            return View("Items", entityManager.Get());
        }

    }
}
