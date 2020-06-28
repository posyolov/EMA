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
        private string name;

        public bool IsValid
        {
            get
            {
                return true;
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                ShortName = ParseShortName(name);
                NotifyPropertyChanged("ShortName");
            }
        }
        //public int? ParentId { get; set; }
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
            //ParentId = model.ParentId;
            Name = model.Name;
            Title = model.Title;
            CatalogItemId = model.CatalogItemId;

            Parent = model.Parent;
        }

        public void ToModel(Position model)
        {
            //model.ParentId = ParentId;
            model.Name = NamePrefix + ShortName;
            model.Title = Title;
            model.CatalogItemId = CatalogItemId;

            model.ParentId = Parent?.Id;
        }

        private string ParseShortName(string name)
        {
            if (name == null) return name;

            int index = name.LastIndexOf(DELIMITER);
            return (index >= 0) ? name.Substring(index + 1) : name;
        }


    }
}
