using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class PositionEdit
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int? CatalogItemId { get; set; }
    }
}
