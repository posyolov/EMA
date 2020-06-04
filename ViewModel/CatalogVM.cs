using Repository.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ViewModel
{
    public class CatalogVM
    {
        private CatalogItem selectedItem;
        public IEnumerable<CatalogItem> Items { get; }

        public CatalogItem SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                EditItemRequestCommand.RiseCanExecuteChanged();
                DeleteItemRequestCommand.RiseCanExecuteChanged();
            }
        }

        public event Action CreateItemRequest;
        public event Action<CatalogItem> EditItemRequest;
        public event Action<CatalogItem> DeleteItemRequest;

        public DelegateCommand<object> CreateItemRequestCommand { get; }
        public DelegateCommand<object> EditItemRequestCommand { get; }
        public DelegateCommand<object> DeleteItemRequestCommand { get; }

        public CatalogVM(IEnumerable<CatalogItem> items)
        {
            Items = items;

            CreateItemRequestCommand = new DelegateCommand<object>(
                (obj) => CreateItemRequest?.Invoke());

            EditItemRequestCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => EditItemRequest?.Invoke(selectedItem));

            DeleteItemRequestCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => DeleteItemRequest?.Invoke(selectedItem));
        }

    }
}
