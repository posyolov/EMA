using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ViewModel
{
    public class EntriesTreeVM : NotifyViewModel
    {
        private ObservableCollection<Entry> entries;
        private ObservableCollection<Entry> entriesTree;
        private ObservableCollection<Entry> entriesTreeFiltered;
        private Entry selectedItem;

        public event Action<Entry> ShowAddEntryRequest;
        public event Action<Entry> ShowAddChildEntryRequest;
        public event Action<Entry> ShowEditEntryRequest;
        public event Action<Entry> ShowDeleteEntryRequest;

        public DelegateCommand<object> ShowAddEntryCommand { get; }
        public DelegateCommand<object> ShowAddChildEntryCommand { get; }
        public DelegateCommand<object> ShowEditEntryCommand { get; }
        public DelegateCommand<object> ShowDeleteEntryCommand { get; }

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
                FilterEntriesTree();
            }
        }
        public ObservableCollection<Entry> EntriesTreeFiltered
        {
            get => entriesTreeFiltered;
            set
            {
                entriesTreeFiltered = value;
                NotifyPropertyChanged();
            }
        }
        public Entry SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                ShowAddChildEntryCommand.RiseCanExecuteChanged();
                ShowEditEntryCommand.RiseCanExecuteChanged();
                ShowDeleteEntryCommand.RiseCanExecuteChanged();
                NotifyPropertyChanged();
            }
        }
        public EntriesManager EntriesManager { get; }
        public EntriesFilter ItemsFilter { get; }

        public EntriesTreeVM(EntriesManager entriesManager)
        {
            EntriesManager = entriesManager;

            ItemsFilter = new EntriesFilter();
            ItemsFilter.CriteriasChanged += FilterEntriesTree;

            Entries = new ObservableCollection<Entry>(entriesManager.Get());
            EntriesTree = new ObservableCollection<Entry>(entriesManager.GetEntriesTree());

            entriesManager.EntitiesChanged += () =>
            {
                Entries = new ObservableCollection<Entry>(entriesManager.Get());
                EntriesTree = new ObservableCollection<Entry>(entriesManager.GetEntriesTree());
            };

            ShowAddEntryCommand = new DelegateCommand<object>(
                execute: (obj) => ShowAddEntryRequest?.Invoke(new Entry()));

            ShowAddChildEntryCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null && SelectedItem.Parent == null,
                execute: (obj) => ShowAddChildEntryRequest?.Invoke(new Entry() { ParentId = selectedItem.Id, PositionId = selectedItem.PositionId }));

            ShowEditEntryCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => ShowEditEntryRequest?.Invoke(selectedItem));

            ShowDeleteEntryCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => ShowDeleteEntryRequest?.Invoke(selectedItem));
        }

        private void FilterEntriesTree()
        {
            EntriesTreeFiltered = new ObservableCollection<Entry>(entriesTree.Where(e => ItemsFilter.Filter(e) || e.Children != null && e.Children.Any(ech => ItemsFilter.Filter(ech))));
        }
    }
}
