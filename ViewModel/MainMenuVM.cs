using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class MainMenuVM
    {
        public event Action OpenCatalogWindowRequest;
        public event Action OpenVendorsWindowRequest;

        public DelegateCommand<object> OpenCatalogWindowCommand { get; }
        public DelegateCommand<object> OpenVendorsWindowCommand { get; }

        public MainMenuVM()
        {
            OpenCatalogWindowCommand = new DelegateCommand<object>(
                (obj) => OpenCatalogWindowRequest?.Invoke());

            OpenVendorsWindowCommand = new DelegateCommand<object>(
                (obj) => OpenVendorsWindowRequest?.Invoke());
        }

    }
}
