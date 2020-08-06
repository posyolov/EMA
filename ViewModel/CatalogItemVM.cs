using Repository;
using System;

namespace ViewModel
{
    public class CatalogItemVM : IEntityVM<CatalogItem>
    {
        public bool IsValid
        {
            get
            {
                return true;
            }
        }

        public Guid GlobalId { get; set; }
        public int VendorId { get; set; }
        public string ProductCode { get; set; }
        public string Title { get; set; }

        public void ToViewModel(CatalogItem model)
        {
            GlobalId = model.GlobalId;
            VendorId = model.VendorId;
            ProductCode = model.ProductCode;
            Title = model.Title;
        }

        public void ToModel(CatalogItem model)
        {
            model.GlobalId = GlobalId;
            model.VendorId = VendorId;
            model.ProductCode = ProductCode;
            model.Title = Title;
        }
    }
}
