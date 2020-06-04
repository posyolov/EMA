using System.Windows;
using ViewModel;
using Model;
using Repository.EF;

namespace EMA
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        CatalogManager _catalogManager;

        MainVM _mainVM;
        MainWindow _mainWindow;

        private void OnStartup(object sender, StartupEventArgs e)
        {
            _catalogManager = new CatalogManager();

            _mainVM = new MainVM();
            _mainVM.MainMenuVM.OpenCatalogWindowRequest += () =>
            {
                //var catalogWindow = new CatalogWindow();
                //catalogWindow.DataContext = new CatalogVM();
                //catalogWindow.Owner = _mainWindow;
                //catalogWindow.Show();
                //CreateViewModelWindow<CatalogWindow>(new CatalogVM(_catalogManager.GetCatalog())).Show();

                var catalogVM = new CatalogVM(_catalogManager.GetCatalog());
                catalogVM.CreateItemRequest += CatalogVM_CreateItemRequest;
                catalogVM.EditItemRequest += CatalogVM_EditItemRequest;
                catalogVM.DeleteItemRequest += CatalogVM_DeleteItemRequest;
                CreateViewModelWindow<CatalogWindow>(catalogVM).Show();

            };
            _mainVM.MainMenuVM.OpenVendorsWindowRequest += () =>
            {
                var vendorsVM = new VendorsVM(_catalogManager.GetVendors());
                vendorsVM.CreateVendorRequest += VendorsVM_CreateVendorRequest;
                vendorsVM.EditVendorRequest += VendorsVM_EditVendorRequest;
                vendorsVM.DeleteVendorRequest += VendorsVM_DeleteVendorRequest;
                CreateViewModelWindow<VendorsWindow>(vendorsVM).Show();
            };

            _mainWindow = new MainWindow();
            _mainWindow.DataContext = _mainVM;
            _mainWindow.Show();
        }


        private void VendorsVM_DeleteVendorRequest(Vendor obj)
        {
            _catalogManager.DeleteVendor(obj);
        }

        private void VendorsVM_CreateVendorRequest()
        {
            var newVendor = new Vendor();
            var dialogViewModel = new DialogVM(newVendor);
            var win = CreateViewModelWindow<VendorEditWindow>(dialogViewModel);
            dialogViewModel.Ok += () =>
            {
                _catalogManager.AddVendor(newVendor);
                win.Close();
            };
            win.ShowDialog();
        }

        private void VendorsVM_EditVendorRequest(Vendor obj)
        {
            var dialogViewModel = new DialogVM(obj);
            var win = CreateViewModelWindow<VendorEditWindow>(dialogViewModel);
            dialogViewModel.Ok += () =>
            {
                _catalogManager.UpdateVendor(obj);
                win.Close();
            };
            win.ShowDialog();
        }


        private void CatalogVM_DeleteItemRequest(CatalogItem obj)
        {
            _catalogManager.DeleteCatalogItem(obj);
        }

        private void CatalogVM_CreateItemRequest()
        {
            var newCatalogItem = new CatalogItem();
            var dialogViewModel = new DialogVM(newCatalogItem);
            var win = CreateViewModelWindow<CatalogItemEditWindow>(dialogViewModel);
            dialogViewModel.Ok += () =>
            {
                _catalogManager.AddCatalogItem(newCatalogItem);
                win.Close();
            };
            win.ShowDialog();
        }

        private void CatalogVM_EditItemRequest(CatalogItem obj)
        {
            var catalogItemEditViewModel = new CatalogItemEditVM(obj, _catalogManager.GetVendors());
            var dialogViewModel = new DialogVM(catalogItemEditViewModel);
            var win = CreateViewModelWindow<CatalogItemEditWindow>(dialogViewModel);
            dialogViewModel.Ok += () =>
            {
                _catalogManager.UpdateCatalogItem(obj);
                win.Close();
            };
            win.ShowDialog();
        }


        Window CreateViewModelWindow<TWindow>(object context) where TWindow : Window, new()
        {
            TWindow window = new TWindow();
            window.DataContext = context;
            window.Owner = _mainWindow;
            return window;
        }
    }
}
