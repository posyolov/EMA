using Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ViewModel
{
    public class EntriesTreeVM : NotifyViewModel
    {
        private ObservableCollection<EntryVM> entries;
        private ObservableCollection<EntryVM> entriesTree;
        private ObservableCollection<EntryVM> entriesTreeFiltered;
        private EntryVM selectedItem;
        private readonly EntriesProxy entriesProxy;

        public event Action<EntryVM> ShowAddEntryRequest;
        public event Action<EntryVM> ShowAddChildEntryRequest;
        public event Action<EntryVM> ShowEditEntryRequest;
        public event Action<EntryVM> ShowDeleteEntryRequest;

        public DelegateCommand<EntryVM> ShowAddEntryCommand { get; }
        public DelegateCommand<EntryVM> ShowAddChildEntryCommand { get; }
        public DelegateCommand<EntryVM> ShowEditEntryCommand { get; }
        public DelegateCommand<EntryVM> ShowDeleteEntryCommand { get; }

        public ObservableCollection<EntryVM> Entries
        {
            get => entries;
            private set
            {
                entries = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<EntryVM> EntriesTree
        {
            get => entriesTree;
            set
            {
                entriesTree = value;
                NotifyPropertyChanged();
                FilterEntriesTree();
            }
        }
        public ObservableCollection<EntryVM> EntriesTreeFiltered
        {
            get => entriesTreeFiltered;
            set
            {
                entriesTreeFiltered = value;
                NotifyPropertyChanged();
            }
        }
        public EntryVM SelectedItem
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

        public EntriesFilter ItemsFilter { get; }

        public EntriesTreeVM(EntriesProxy entriesProxy)
        {
            this.entriesProxy = entriesProxy;

            ItemsFilter = new EntriesFilter();
            ItemsFilter.CriteriasChanged += FilterEntriesTree;

            entriesProxy.EntitiesChanged += UpdateEntries;
            UpdateEntries();

            ShowAddEntryCommand = new DelegateCommand<EntryVM>(
                execute: (obj) => ShowAddEntryRequest?.Invoke(new EntryVM()));

            ShowAddChildEntryCommand = new DelegateCommand<EntryVM>(
                canExecute: (obj) => SelectedItem != null && SelectedItem.ParentId == null,
                execute: (obj) => ShowAddChildEntryRequest?.Invoke(new EntryVM() { ParentId = selectedItem.Id, PositionId = selectedItem.PositionId }));

            ShowEditEntryCommand = new DelegateCommand<EntryVM>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => ShowEditEntryRequest?.Invoke(selectedItem));

            ShowDeleteEntryCommand = new DelegateCommand<EntryVM>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => ShowDeleteEntryRequest?.Invoke(selectedItem));
        }

        private void UpdateEntries()
        {
            Entries = entriesProxy.Get();
            EntriesTree = entriesProxy.Get();
        }

        private void FilterEntriesTree()
        {
            EntriesTreeFiltered = new ObservableCollection<EntryVM>(entriesTree.Where(e => ItemsFilter.Filter(e) || e.Children != null && e.Children.Any(ech => ItemsFilter.Filter(ech))));
        }
    }
}
