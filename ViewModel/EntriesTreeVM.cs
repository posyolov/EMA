using Repository.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ViewModel
{
    public class EntriesTreeVM : NotifyViewModel
    {
        private readonly Func<IEnumerable<Entry>> getListDelegate;
        private readonly Func<IEnumerable<Entry>> getTreeDelegate;

        private ObservableCollection<Entry> entries;
        private ObservableCollection<Entry> entriesTree;
        private Entry selectedItem;

        public event Action<Entry> AddEntryRequest;
        public event Action<Entry> AddChildEntryRequest;
        public event Action<Entry> EditEntryRequest;
        public event Action<Entry> DeleteEntryRequest;

        public DelegateCommand<object> AddEntryRequestCommand { get; }
        public DelegateCommand<object> AddChildEntryRequestCommand { get; }
        public DelegateCommand<object> EditEntryRequestCommand { get; }
        public DelegateCommand<object> DeleteEntryRequestCommand { get; }

        public ObservableCollection<Entry> Entries
        {
            get => entries;
            private set
            {
                entries = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Entry> EntriesTree
        {
            get => entriesTree;
            set
            { 
                entriesTree = value;
                NotifyPropertyChanged();
            }
        }


        public Entry SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                AddChildEntryRequestCommand.RiseCanExecuteChanged();
                EditEntryRequestCommand.RiseCanExecuteChanged();
                DeleteEntryRequestCommand.RiseCanExecuteChanged();
                NotifyPropertyChanged();
            }
        }

        public EntriesTreeVM(Func<IEnumerable<Entry>> getListDelegate, Func<IEnumerable<Entry>> getTreeDelegate)
        {
            this.getListDelegate = getListDelegate;
            this.getTreeDelegate = getTreeDelegate;
            Entries = new ObservableCollection<Entry>(getListDelegate?.Invoke());
            EntriesTree = new ObservableCollection<Entry>(getTreeDelegate?.Invoke());

            AddEntryRequestCommand = new DelegateCommand<object>(
                execute: (obj) => AddEntryRequest?.Invoke(new Entry()));

            AddChildEntryRequestCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => AddChildEntryRequest?.Invoke(new Entry() { ParentId = selectedItem.Id }));

            EditEntryRequestCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => EditEntryRequest?.Invoke(selectedItem));

            DeleteEntryRequestCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => DeleteEntryRequest?.Invoke(selectedItem));
        }

        public void UpdateList()
        {
            Entries = new ObservableCollection<Entry>(getListDelegate?.Invoke());
            EntriesTree = new ObservableCollection<Entry>(getTreeDelegate?.Invoke());
        }
    }
}
