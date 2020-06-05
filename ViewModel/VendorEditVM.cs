using Repository.EF;

namespace ViewModel
{
    public class VendorEditVM
    {
        public string Name { get; set; }
        public VendorEditVM(Vendor vendor)
        {
            Name = vendor.Name;
        }

    }
}
