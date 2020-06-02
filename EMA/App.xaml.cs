using System.Windows;
using ViewModel;
using Model;

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
                CreateViewModelWindow<CatalogWindow>(new CatalogVM(_catalogManager.GetCatalog())).Show();
            };
            _mainVM.MainMenuVM.OpenVendorsWindowRequest += () =>
            {
                var vendorsVM = new VendorsVM(_catalogManager.GetVendors());
                vendorsVM.EditVendorRequest += VendorsVM_EditVendorRequest;
                CreateViewModelWindow<VendorsWindow>(vendorsVM).Show();
            };

            _mainWindow = new MainWindow();
            _mainWindow.DataContext = _mainVM;
            _mainWindow.Show();
        }

        private void VendorsVM_EditVendorRequest(Repository.EF.Vendor obj)
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

        Window CreateViewModelWindow<TWindow>(object context) where TWindow : Window, new()
        {
            TWindow window = new TWindow();
            window.DataContext = context;
            window.Owner = _mainWindow;
            return window;
        }
    }
}
