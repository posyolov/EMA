using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;

namespace WebEMA.Controllers
{
    public class VendorsController  : GenericController<Vendor>
    {
        public VendorsController(IEntityManager<Vendor> entityManager)
            : base(entityManager)
        {
        }
    }
}
