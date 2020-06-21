using Repository.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public class EntriesListVM : NotifyViewModel
    {
        private readonly Func<IEnumerable<Entry>> getListDelegate;

        private ObservableCollection<Entry> entries;
        private Entry selectedItem;

        public ObservableCollection<Entry> Entries
        {
            get
            {
                return entries;
            }
            private set
            {
                entries = value;
                NotifyPropertyChanged();
            }
        }

        public Entry SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                NotifyPropertyChanged();
            }
        }

        public EntriesListVM(Func<IEnumerable<Entry>> getListDelegate)
        {
            this.getListDelegate = getListDelegate;
            Entries = new ObservableCollection<Entry>(getListDelegate?.Invoke());
        }

        public void UpdateList()
        {
            Entries = new ObservableCollection<Entry>(getListDelegate?.Invoke());
        }
    }
}
