using Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public class PositionFullData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string ParentName { get; set; }
        public string CatalogItemName { get; set; }
    }
}
