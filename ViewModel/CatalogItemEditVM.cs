using Repository.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class CatalogItemEditVM
    {
        public Guid GlobalId { get; set; }
        public int VendorId { get; set; }
        public string ProductCode { get; set; }
        public string Title { get; set; }
        public IEnumerable<Vendor> Vendors { get; set; }

        public CatalogItemEditVM(CatalogItem item, IEnumerable<Vendor> vendors)
        {
            GlobalId = item.GlobalId;
            VendorId = item.VendorId;
            ProductCode = item.ProductCode;
            Title = item.Title;

            Vendors = vendors;
        }
    }
}
