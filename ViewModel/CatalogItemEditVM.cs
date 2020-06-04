using Repository.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class CatalogItemEditVM
    {
        public CatalogItem Item { get; set; }
        public IEnumerable<Vendor> Vendors { get; set; }

        public CatalogItemEditVM(CatalogItem item, IEnumerable<Vendor> vendors)
        {
            Item = item;
            Vendors = vendors;
        }
    }
}
