using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;

namespace WebEMA.Controllers
{
    public class CatalogController : GenericController<CatalogItem>
    {
        public CatalogController(IEntityManager<CatalogItem> entityManager)
            : base(entityManager)
        {
        }
    }
}
