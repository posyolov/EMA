using System;

namespace Repository.EF
{
    public class CatalogItem
    {
        public int Id { get; set; }
        public Guid GlobalId { get; set; }
        public Vendor Vendor { get; set; }
        public string ProductCode { get; set; }
        public string Title { get; set; }
    }
}
