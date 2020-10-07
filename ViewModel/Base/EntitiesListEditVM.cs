using Model;
using System;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public class EntitiesListEditVM<TEntityVM> : NotifyViewModel where TEntityVM : class, new()
    {
        private ObservableCollection<TEntityVM> items;
        private int? selectedItemId;

        public event Action AddItemRequest;
        public event Action<int> EditItemRequest;
        public event Action<int> DeleteItemRequest;

        public DelegateCommand<object> AddItemRequestCommand { get; }
        public DelegateCommand<int> EditItemRequestCommand { get; }
        public DelegateCommand<int> DeleteItemRequestCommand { get; }

        public ObservableCollection<TEntityVM> Items
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

        public int? SelectedItemId
        {
            get => selectedItemId;
            set
            {
                selectedItemId = value;
                EditItemRequestCommand.RiseCanExecuteChanged();
                DeleteItemRequestCommand.RiseCanExecuteChanged();
            }
        }

        public EntitiesListEditVM(ObservableCollection<TEntityVM> entities)
        {
            Items = entities;

            AddItemRequestCommand = new DelegateCommand<object>(
                (obj) => AddItemRequest?.Invoke());

            EditItemRequestCommand = new DelegateCommand<int>(
                canExecute: (obj) => SelectedItemId != null,
                execute: (obj) => EditItemRequest?.Invoke(selectedItemId.GetValueOrDefault()));

            DeleteItemRequestCommand = new DelegateCommand<int>(
                canExecute: (obj) => SelectedItemId != null,
                execute: (obj) => DeleteItemRequest?.Invoke(selectedItemId.GetValueOrDefault()));
        }
    }
}
