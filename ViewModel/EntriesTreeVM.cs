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
                NotifyPropertyChanged();
            }
        }

        public EntriesTreeVM(Func<IEnumerable<Entry>> getListDelegate, Func<IEnumerable<Entry>> getTreeDelegate)
        {
            this.getListDelegate = getListDelegate;
            this.getTreeDelegate = getTreeDelegate;
            Entries = new ObservableCollection<Entry>(getListDelegate?.Invoke());
            EntriesTree = new ObservableCollection<Entry>(getTreeDelegate?.Invoke());
        }

        public void UpdateList()
        {
            Entries = new ObservableCollection<Entry>(getListDelegate?.Invoke());
            EntriesTree = new ObservableCollection<Entry>(getTreeDelegate?.Invoke());
        }
    }
}
