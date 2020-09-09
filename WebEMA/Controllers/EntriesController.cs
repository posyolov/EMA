using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;

namespace WebEMA.Controllers
{
    public class EntriesController : GenericController<Entry>
    {
        public EntriesController(IEntityManager<Entry> entityManager)
            : base(entityManager)
        {
        }
    }
}
