using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;


namespace WebEMA.Controllers
{
    public class PositionsController : GenericController<Position>
    {
        public PositionsController(IEntityManager<Position> entityManager)
            : base(entityManager)
        {
        }
    }
}
