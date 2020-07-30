using Repository.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class PositionVM : NotifyViewModel, IEntityVM<Position>
    {
        const char DELIMITER = ';';

        private Position parent;

        public bool IsValid
        {
            get
            {
                return true;
            }
        }

        public string Name => NamePrefix + ShortName;
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public int? CatalogItemId { get; set; }

        public Position Parent
        {
            get => parent;
            set
            {
                parent = value;
                NamePrefix = (parent == null) ? "" : parent.Name + DELIMITER;
                NotifyPropertyChanged("NamePrefix");
            }
        }
        public string NamePrefix { get; set; }
        public string ShortName { get; set; }

        public void ToViewModel(Position model)
        {
            ParentId = model.ParentId;
            ShortName = ParseShortName(model.Name);
            NamePrefix = (model.Parent == null) ? "" : model.Parent.Name + DELIMITER;

            Title = model.Title;
            CatalogItemId = model.CatalogItemId;
        }

        public void ToModel(Position model)
        {
            model.ParentId = Parent?.Id;
            model.Parent = null;

            model.Name = Name;
            model.Title = Title;
            model.CatalogItemId = CatalogItemId;
        }

        private string ParseShortName(string name)
        {
            if (name == null) return name;

            int index = name.LastIndexOf(DELIMITER);
            return (index >= 0) ? name.Substring(index + 1) : name;
        }


    }
}
