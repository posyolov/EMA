using System;

namespace Repository
{
    public class CatalogItem
    {
        public int Id { get; set; }
        public Guid GlobalId { get; set; }
        public int VendorId { get; set; }
        public string ProductCode { get; set; }
        public string Title { get; set; }

        public virtual Vendor Vendor { get; set; }

    }
}
