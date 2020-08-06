using Repository;

namespace ViewModel
{
    public class VendorVM : IEntityVM<Vendor>
    {
        public bool IsValid 
        { 
            get
            {
                return true;
            }
        }

        public string Name { get; set; }

        public void ToViewModel(Vendor model)
        {
            Name = model.Name;
        }

        public void ToModel(Vendor model)
        {
            model.Name = Name;
        }
    }
}
