using Model;
using System;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public class EntitiesListEditVM<TEntityVM> : NotifyViewModel where TEntityVM : class, new()
    {
        public event Action<TEntityVM> AddItemRequest;
        public event Action<TEntityVM> EditItemRequest;
        public event Action<TEntityVM> DeleteItemRequest;

        public DelegateCommand<TEntityVM> AddItemRequestCommand { get; }
        public DelegateCommand<TEntityVM> EditItemRequestCommand { get; }
        public DelegateCommand<TEntityVM> DeleteItemRequestCommand { get; }

        private ObservableCollection<TEntityVM> items;
        private TEntityVM selectedItem;
        private readonly IEntityProxy<TEntityVM> entityProxy;

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

        public TEntityVM SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                EditItemRequestCommand.RiseCanExecuteChanged();
                DeleteItemRequestCommand.RiseCanExecuteChanged();
            }
        }

        public EntitiesListEditVM(IEntityProxy<TEntityVM> entityProxy)
        {
            this.entityProxy = entityProxy;
            this.entityProxy.EntitiesChanged += UpdateEntitiesList;

            UpdateEntitiesList();

            AddItemRequestCommand = new DelegateCommand<TEntityVM>(
                (obj) => AddItemRequest?.Invoke(new TEntityVM()));

            EditItemRequestCommand = new DelegateCommand<TEntityVM>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => EditItemRequest?.Invoke(selectedItem));

            DeleteItemRequestCommand = new DelegateCommand<TEntityVM>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => DeleteItemRequest?.Invoke(selectedItem));
        }

        private void UpdateEntitiesList()
        {
            Items = entityProxy.Get();
        }
    }
}
