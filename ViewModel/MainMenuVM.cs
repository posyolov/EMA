using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class MainMenuVM
    {
        public event Action CatalogRequest;
        public event Action VendorsRequest;
        public event Action PositionsRequest;

        public DelegateCommand<object> OpenCatalogWindowCommand { get; }
        public DelegateCommand<object> OpenVendorsWindowCommand { get; }
        public DelegateCommand<object> OpenPositionsWindowCommand { get; }

        public MainMenuVM()
        {
            OpenCatalogWindowCommand = new DelegateCommand<object>(
                (obj) => CatalogRequest?.Invoke());

            OpenVendorsWindowCommand = new DelegateCommand<object>(
                (obj) => VendorsRequest?.Invoke());

            OpenPositionsWindowCommand = new DelegateCommand<object>(
                (obj) => PositionsRequest?.Invoke());
        }

    }
}
