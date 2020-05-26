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
                CreateCrudGetWindow<CatalogWindow>(new CatalogVM(_catalogManager.GetCatalog())).Show();
            };
            _mainVM.MainMenuVM.OpenVendorsWindowRequest += () =>
            {
                //var vendorsWindow = new VendorsWindow();
                //vendorsWindow.DataContext = new VendorsVM();
                //vendorsWindow.Owner = _mainWindow;
                //vendorsWindow.Show();
                CreateCrudGetWindow<VendorsWindow>(new VendorsVM(_catalogManager.GetVendors())).Show();
            };

            _mainWindow = new MainWindow();
            _mainWindow.DataContext = _mainVM;
            _mainWindow.Show();
        }

        Window CreateCrudGetWindow<TWindow>(object context) where TWindow : Window,new()
        {
            TWindow window = new TWindow();
            window.DataContext = context;
            window.Owner = _mainWindow;
            return window;
        }
    }
}
