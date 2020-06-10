using Repository.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ViewModel
{
    public class VendorsVM : INotifyPropertyChanged
    {
        private Vendor selectedVendor;
        private ObservableCollection<Vendor> vendors;

        public ObservableCollection<Vendor> Vendors
        {
            get
            {
                return vendors;
            }
            private set
            {
                vendors = value;
                NotifyPropertyChanged();
            }
        }

        public Vendor SelectedVendor
        {
            get => selectedVendor;
            set
            {
                selectedVendor = value;
                EditVendorRequestCommand.RiseCanExecuteChanged();
                DeleteVendorRequestCommand.RiseCanExecuteChanged();
            }
        }

        public event Action AddVendorRequest;
        public event Action<Vendor> EditVendorRequest;
        public event Action<Vendor> DeleteVendorRequest;
        public event PropertyChangedEventHandler PropertyChanged;

        public DelegateCommand<object> AddVendorRequestCommand { get; }
        public DelegateCommand<object> EditVendorRequestCommand { get; }
        public DelegateCommand<object> DeleteVendorRequestCommand { get; }

        public VendorsVM(IEnumerable<Vendor> items)
        {
            Vendors = new ObservableCollection<Vendor>(items);

            AddVendorRequestCommand = new DelegateCommand<object>(
                (obj) => AddVendorRequest?.Invoke());

            EditVendorRequestCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedVendor != null,
                execute: (obj) => EditVendorRequest?.Invoke(selectedVendor));

            DeleteVendorRequestCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedVendor != null,
                execute: (obj) => DeleteVendorRequest?.Invoke(selectedVendor));
        }

        public void UpdateVendorsList(IEnumerable<Vendor> items)
        {
            Vendors = new ObservableCollection<Vendor>(items);
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
