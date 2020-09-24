using Repository;

namespace ViewModel
{
    public class VendorVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Vendor FillModel(Vendor model)
        {
            model.Id = Id;
            model.Name = Name;

            return model;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
