using System.Collections.Generic;

namespace Repository
{
    public class Position
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int? CatalogItemId { get; set; }
        public string Description { get; set; }

        public virtual Position Parent { get; set; }
        public virtual ICollection<Position> Children { get; set; }
        public virtual CatalogItem CatalogItem { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
