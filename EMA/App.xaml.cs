using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ViewModel;

namespace EMA
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainVM _mainVM;
        MainWindow _mainWindow;

        private void OnStartup(object sender, StartupEventArgs e)
        {
            _mainVM = new MainVM();
            _mainVM.MainMenuVM.OpenCatalogWindowRequest += () =>
            {
                var catalogWindow = new CatalogWindow();
                catalogWindow.DataContext = new CatalogVM();
                catalogWindow.Owner = _mainWindow;
                catalogWindow.Show();
            };

            _mainWindow = new MainWindow();
            _mainWindow.DataContext = _mainVM;
            _mainWindow.Show();
        }
    }
}
