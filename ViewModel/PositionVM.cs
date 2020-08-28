using Repository;

namespace ViewModel
{
    public class PositionVM : NotifyViewModel, IEntityVM<Position>
    {
        public bool IsValid
        {
            get
            {
                return true;
            }
        }

        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int? CatalogItemId { get; set; }

        //public Position Parent { get; set; }

        public void ToViewModel(Position model)
        {
            ParentId = model.ParentId;
            Name = model.Name;
            Title = model.Title;
            CatalogItemId = model.CatalogItemId;
        }

        public void ToModel(Position model)
        {
            //model.ParentId = Parent?.Id;
            //model.Parent = null;

            model.ParentId = ParentId;
            model.Name = Name;
            model.Title = Title;
            model.CatalogItemId = CatalogItemId;
        }
    }
}
