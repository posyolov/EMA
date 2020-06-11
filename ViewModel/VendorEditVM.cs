using Repository.EF;

namespace ViewModel
{
    public class VendorEditVM : DialogVM
    {
        private readonly Vendor original;

        public string Name { get; set; }

        public VendorEditVM(Vendor vendor)
        {
            original = vendor;

            Name = original.Name;
        }

        public void Apply()
        {
            original.Name = Name;
        }

    }
}
