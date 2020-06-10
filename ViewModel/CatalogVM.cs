using Repository.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ViewModel
{
    public class CatalogVM : INotifyPropertyChanged
    {
        private CatalogItem selectedItem;
        private ObservableCollection<CatalogItem> items;

        public ObservableCollection<CatalogItem> Items
        {
            get
            {
                return items;
            }
            private set
            {
                items = value;
                NotifyPropertyChanged();
            }
        }


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

        public event Action AddItemRequest;
        public event Action<CatalogItem> EditItemRequest;
        public event Action<CatalogItem> DeleteItemRequest;
        public event PropertyChangedEventHandler PropertyChanged;

        public DelegateCommand<object> AddItemRequestCommand { get; }
        public DelegateCommand<object> EditItemRequestCommand { get; }
        public DelegateCommand<object> DeleteItemRequestCommand { get; }

        public CatalogVM(IEnumerable<CatalogItem> items)
        {
            Items = new ObservableCollection<CatalogItem>(items);

            AddItemRequestCommand = new DelegateCommand<object>(
                (obj) => AddItemRequest?.Invoke());

            EditItemRequestCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => EditItemRequest?.Invoke(selectedItem));

            DeleteItemRequestCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => DeleteItemRequest?.Invoke(selectedItem));
        }

        public void UpdateCatalogList(IEnumerable<CatalogItem> items)
        {
            Items = new ObservableCollection<CatalogItem>(items);
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
