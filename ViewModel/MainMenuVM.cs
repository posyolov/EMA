using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class MainMenuVM
    {
        public event Action OpenCatalogWindowRequest;

        public DelegateCommand<object> OpenCatalogWindowCommand { get; }

        public MainMenuVM()
        {
            OpenCatalogWindowCommand = new DelegateCommand<object>(
                (obj) => OpenCatalogWindowRequest?.Invoke());
        }

    }
}
