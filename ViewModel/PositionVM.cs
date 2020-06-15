using Repository.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class PositionVM : IEntityVM<Position>
    {
        public bool IsValid
        {
            get
            {
                return true;
            }
        }

        public string Name { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public int? CatalogItemId { get; set; }

        public void ToViewModel(Position model)
        {
            ParentId = model.ParentId;
            Name = model.Name;
            Title = model.Title;
            CatalogItemId = model.CatalogItemId;
        }

        public void ToModel(Position model)
        {
            model.ParentId = ParentId;
            model.Name = Name;
            model.Title = Title;
            model.CatalogItemId = CatalogItemId;
        }
    }
}
