using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ViewModel
{
    public class EntitiesListEditVM<TEntity> : INotifyPropertyChanged
    {
        public event Action AddItemRequest;
        public event Action<TEntity> EditItemRequest;
        public event Action<TEntity> DeleteItemRequest;
        public event PropertyChangedEventHandler PropertyChanged;

        public DelegateCommand<object> AddItemRequestCommand { get; }
        public DelegateCommand<object> EditItemRequestCommand { get; }
        public DelegateCommand<object> DeleteItemRequestCommand { get; }

        private TEntity selectedItem;
        private ObservableCollection<TEntity> items;
        private Func<IEnumerable<TEntity>> getListDelegate;

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
                (obj) => AddItemRequest?.Invoke());

            EditItemRequestCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => EditItemRequest?.Invoke(selectedItem));

            DeleteItemRequestCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => DeleteItemRequest?.Invoke(selectedItem));
        }

        public EntitiesListEditVM(Func<IEnumerable<TEntity>> getListDelegate, Action addItemDelegate, Action<TEntity> editItemDelegate, Action<TEntity> deleteItemDelegate)
            : this(getListDelegate)
        {
            AddItemRequest += addItemDelegate;
            EditItemRequest += editItemDelegate;
            DeleteItemRequest += deleteItemDelegate;
        }

        public void UpdateList()
        {
            Items = new ObservableCollection<TEntity>(getListDelegate());
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
