using System;

namespace ViewModel
{
    public class MainMenuVM
    {
        public event Action ShowVendorsRequest;
        public event Action ShowCatalogRequest;
        public event Action ShowPositionsRequest;
        public event Action ShowEntriesRequest;

        public DelegateCommand<object> ShowVendorsCommand { get; }
        public DelegateCommand<object> ShowCatalogCommand { get; }
        public DelegateCommand<object> ShowPositionsCommand { get; }
        public DelegateCommand<object> ShowEntriesCommand { get; }


        public MainMenuVM()
        {
            ShowVendorsCommand = new DelegateCommand<object>(
                (obj) => ShowVendorsRequest?.Invoke());

            ShowCatalogCommand = new DelegateCommand<object>(
                (obj) => ShowCatalogRequest?.Invoke());

            ShowPositionsCommand = new DelegateCommand<object>(
                (obj) => ShowPositionsRequest?.Invoke());

            ShowEntriesCommand = new DelegateCommand<object>(
                (obj) => ShowEntriesRequest?.Invoke());
        }

    }
}
