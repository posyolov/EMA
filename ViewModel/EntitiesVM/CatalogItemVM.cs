using Repository;
using System;
using System.Runtime.InteropServices.ComTypes;

namespace ViewModel
{
    public class CatalogItemVM
    {
        public int Id { get; set; }
        public Guid GlobalId { get; set; }
        public int VendorId { get; set; }
        public string ProductCode { get; set; }
        public string Title { get; set; }
        public virtual VendorVM Vendor { get; set; }

        public CatalogItem FillModel(CatalogItem model)
        {
            model.Id = Id;
            model.GlobalId = GlobalId;
            model.VendorId = VendorId;
            model.ProductCode = ProductCode;
            model.Title = Title;

            return model;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
