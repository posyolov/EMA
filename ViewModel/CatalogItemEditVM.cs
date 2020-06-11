using Repository.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class CatalogItemEditVM : DialogVM
    {
        private readonly CatalogItem original;

        public Guid GlobalId { get; set; }
        public int VendorId { get; set; }
        public string ProductCode { get; set; }
        public string Title { get; set; }

        public IEnumerable<Vendor> Vendors { get; set; }

        public CatalogItemEditVM(CatalogItem item, IEnumerable<Vendor> vendors)
        {
            original = item;

            GlobalId = original.GlobalId;
            VendorId = original.VendorId;
            ProductCode = original.ProductCode;
            Title = original.Title;

            Vendors = vendors;
        }

        public void Apply()
        {
            original.GlobalId = GlobalId;
            original.VendorId = VendorId;
            original.ProductCode = ProductCode;
            original.Title = Title;
        }
    }
}
