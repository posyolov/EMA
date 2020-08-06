using System;

namespace ViewModel
{
    public class MainMenuVM
    {
        public event Action CatalogRequest;
        public event Action VendorsRequest;
        public event Action PositionsRequest;
        public event Action EntriesRequest;

        public DelegateCommand<object> OpenCatalogWindowCommand { get; }
        public DelegateCommand<object> OpenVendorsWindowCommand { get; }
        public DelegateCommand<object> OpenPositionsWindowCommand { get; }
        public DelegateCommand<object> OpenEntriesWindowCommand { get; }


        public MainMenuVM()
        {
            OpenCatalogWindowCommand = new DelegateCommand<object>(
                (obj) => CatalogRequest?.Invoke());

            OpenVendorsWindowCommand = new DelegateCommand<object>(
                (obj) => VendorsRequest?.Invoke());

            OpenPositionsWindowCommand = new DelegateCommand<object>(
                (obj) => PositionsRequest?.Invoke());

            OpenEntriesWindowCommand = new DelegateCommand<object>(
                (obj) => EntriesRequest?.Invoke());
        }

    }
}
