using Repository.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace ViewModel
{
    public class VendorsVM : INotifyPropertyChanged
    {
        private Vendor selectedVendor;

        //public ObservableCollection<Vendor> Vendors { get; }
        public ObservableCollection<Vendor> Vendors { get; }
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

        public event Action CreateVendorRequest;
        public event Action<Vendor> EditVendorRequest;
        public event Action<Vendor> DeleteVendorRequest;
        public event PropertyChangedEventHandler PropertyChanged;

        public DelegateCommand<object> CreateVendorRequestCommand { get; }
        public DelegateCommand<object> EditVendorRequestCommand { get; }
        public DelegateCommand<object> DeleteVendorRequestCommand { get; }

        public VendorsVM(IEnumerable<Vendor> vendorItems)
        {
            Vendors = new ObservableCollection<Vendor>(vendorItems);
            //Vendors = vendorItems;

            CreateVendorRequestCommand = new DelegateCommand<object>(
                (obj) => CreateVendorRequest?.Invoke());

            EditVendorRequestCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedVendor != null,
                execute: (obj) => EditVendorRequest?.Invoke(selectedVendor));

            DeleteVendorRequestCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedVendor != null,
                execute: (obj) => DeleteVendorRequest?.Invoke(selectedVendor));
        }

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
