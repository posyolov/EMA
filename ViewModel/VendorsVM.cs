using Repository.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ViewModel
{
    public class VendorsVM
    {
        public ObservableCollection<Vendor> Vendors { get; }

        public VendorsVM(IEnumerable<Vendor> vendorItems)
        {
            Vendors = new ObservableCollection<Vendor>(vendorItems);
            //Vendors.Add(new Vendor { Id = 1, Name = "Siemens"});
            //Vendors.Add(new Vendor { Id = 2, Name = "Sick" });
        }
    }
}
