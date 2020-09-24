using Repository;
using System;
using System.Reflection.Metadata.Ecma335;

namespace ViewModel
{
    public static class ViewModelMapper
    {
        static public VendorVM ToViewModel(Vendor model)
        {
            if (model is null)
                return null;

            var vm = new VendorVM
            {
                Id = model.Id,
                Name = model.Name
            };
            return vm;
        }

        static public CatalogItemVM ToViewModel(CatalogItem model)
        {
            if (model is null)
                return null;

            var vm = new CatalogItemVM
            {
                Id = model.Id,
                GlobalId = model.GlobalId,
                VendorId = model.VendorId,
                ProductCode = model.ProductCode,
                Title = model.Title,
                Vendor = ToViewModel(model.Vendor)
            };
            return vm;
        }

        static public PositionVM ToViewModel(Position model)
        {
            if (model is null)
                return null;

            var vm = new PositionVM
            {
                Id = model.Id,
                ParentId = model.ParentId,
                Name = model.Name,
                Title = model.Title,
                CatalogItemId = model.CatalogItemId,
                Parent = ToViewModel(model.Parent),
                CatalogItem = ToViewModel(model.CatalogItem)
            };
            //        if (model.Children != null)
            //{
            //    Children = new ObservableCollection<PositionVM>();
            //    foreach (var item in model.Children)
            //    {
            //        Children.Add(new PositionVM(item));
            //    }
            return vm;
        }

        static public EntryVM ToViewModel(Entry model)
        {
            if (model is null)
                return null;

            var vm = new EntryVM
            {
                Id = model.Id,
                ParentId = model.ParentId,
                IsComplete = model.IsComplete,
                OccurDateTime = model.OccurDateTime != default ? model.OccurDateTime : DateTime.Now,
                Title = model.Title,
                Description = model.Description,
                PositionId = model.PositionId,
                ReasonId = model.ReasonId,
                ContinuationCriteriaId = model.ContinuationCriteriaId,
                Priority = model.Priority,
                PlannedStartDate = model.PlannedStartDate,
                EstimatedResources = model.EstimatedResources,
                Parent = ToViewModel(model.Parent),
                Position = ToViewModel(model.Position)
            };
            //        if (model.Children != null)
            //{
            //    Children = new ObservableCollection<EntryVM>();
            //    foreach (var item in model.Children)
            //    {
            //        Children.Add(new EntryVM(item));
            //    }
            return vm;
        }

    }
}
