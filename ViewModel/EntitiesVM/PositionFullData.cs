using Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public class PositionFullData
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int? CatalogItemId { get; set; }
        public PositionFullData Parent { get; set; }
        public ObservableCollection<PositionFullData> Children { get; set; }
        public CatalogItemVM CatalogItem { get; set; }

        public Position FillModel(Position model)
        {
            model.Id = Id;
            model.ParentId = ParentId;
            model.Name = Name;
            model.Title = Title;
            model.CatalogItemId = CatalogItemId;

            return model;
        }
    }
}
