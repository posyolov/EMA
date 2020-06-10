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


        public EntitiesListEditVM(IEnumerable<TEntity> items)
        {
            Items = new ObservableCollection<TEntity>(items);

            AddItemRequestCommand = new DelegateCommand<object>(
                (obj) => AddItemRequest?.Invoke());

            EditItemRequestCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => EditItemRequest?.Invoke(selectedItem));

            DeleteItemRequestCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => DeleteItemRequest?.Invoke(selectedItem));
        }

        public void UpdateList(IEnumerable<TEntity> items)
        {
            Items = new ObservableCollection<TEntity>(items);
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
