using Repository.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ViewModel
{
    public class CatalogVM
    {
        public ObservableCollection<CatalogItem> CatalogItems { get; }

        public CatalogVM(IEnumerable<CatalogItem> catalogItems)
        {
            CatalogItems = new ObservableCollection<CatalogItem>(catalogItems);
            //CatalogItems.Add(new CatalogItem { Id = 1, GlobalId = new Guid(), Vendor = new Vendor { Name = "Siemens" }, ProductCode = "111", Title = "aaa" });
            //CatalogItems.Add(new CatalogItem { Id = 2, GlobalId = new Guid(), Vendor = new Vendor { Name = "Sick" }, ProductCode = "222", Title = "bbb" });
        }
    }
}
