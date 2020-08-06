using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public class EntitiesListEditVM<TEntity> : NotifyViewModel where TEntity : new()
    {
        public event Action<TEntity> AddItemRequest;
        public event Action<TEntity> EditItemRequest;
        public event Action<TEntity> DeleteItemRequest;

        public DelegateCommand<object> AddItemRequestCommand { get; }
        public DelegateCommand<object> EditItemRequestCommand { get; }
        public DelegateCommand<object> DeleteItemRequestCommand { get; }

        private TEntity selectedItem;
        private ObservableCollection<TEntity> items;
        private readonly Func<IEnumerable<TEntity>> getListDelegate;

        public ObservableCollection<TEntity> Items
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

        public TEntity SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                EditItemRequestCommand.RiseCanExecuteChanged();
                DeleteItemRequestCommand.RiseCanExecuteChanged();
            }
        }


        public EntitiesListEditVM(Func<IEnumerable<TEntity>> getListDelegate)
        {
            this.getListDelegate = getListDelegate;

            Items = new ObservableCollection<TEntity>(getListDelegate());

            AddItemRequestCommand = new DelegateCommand<object>(
                (obj) => AddItemRequest?.Invoke(new TEntity()));

            EditItemRequestCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => EditItemRequest?.Invoke(selectedItem));

            DeleteItemRequestCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => DeleteItemRequest?.Invoke(selectedItem));
        }

        public EntitiesListEditVM(Func<IEnumerable<TEntity>> getListDelegate, Action<TEntity> addDelegate, Action<TEntity> editDelegate, Action<TEntity> deleteDelegate)
            : this(getListDelegate)
        {
            AddItemRequest += addDelegate;
            EditItemRequest += editDelegate;
            DeleteItemRequest += deleteDelegate;
        }

        public void UpdateList()
        {
            Items = new ObservableCollection<TEntity>(getListDelegate());
        }
    }
}
