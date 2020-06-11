using System.Windows;
using ViewModel;
using Model;
using Repository.EF;
using System;
using System.Collections.Generic;

namespace EMA
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        CatalogManager catalogManager;

        MainVM _mainVM;
        MainWindow _mainWindow;

        private void OnStartup(object sender, StartupEventArgs e)
        {
            catalogManager = new CatalogManager();

            _mainVM = new MainVM();

            _mainVM.MainMenuVM.CatalogRequest += () =>
            {
                var vm = new EntitiesListEditVM<CatalogItem>(catalogManager.GetCatalog, CreateCatalogItemAddWindow, CreateCatalogItemEditWindow, CatalogItemDelete);
                catalogManager.CatalogChanged += vm.UpdateList;
                var win = new CatalogWindow { DataContext = vm, Owner = _mainWindow };
                win.Show();
            };

            _mainVM.MainMenuVM.VendorsRequest += () =>
            {
                var vm = new EntitiesListEditVM<Vendor>(catalogManager.GetVendors, CreateVendorAddWindow, CreateVendorEditWindow, VendorDelete);
                catalogManager.VendorsChanged += vm.UpdateList;
                var win = new VendorsWindow { DataContext = vm, Owner = _mainWindow };
                win.Show();
            };

            _mainWindow = new MainWindow { DataContext = _mainVM };
            _mainWindow.Show();
        }

        private void CreateVendorAddWindow()
        {
            var win = new VendorEditWindow();
            win.DataContext = new VendorEditVM(new Vendor(), catalogManager.AddVendor, win.Close);
            win.Owner = _mainWindow;
            win.ShowDialog();
        }

        private void CreateVendorEditWindow(Vendor obj)
        {
            var win = new VendorEditWindow();
            win.DataContext = new VendorEditVM(obj, catalogManager.UpdateVendor, win.Close);
            win.Owner = _mainWindow;
            win.ShowDialog();
        }

        private void VendorDelete(Vendor obj)
        {
            catalogManager.DeleteVendor(obj);
        }


        private void CreateCatalogItemAddWindow()
        {
            var win = new CatalogItemEditWindow();
            win.DataContext = new CatalogItemEditVM(new CatalogItem(), catalogManager.GetVendors(), catalogManager.AddCatalogItem, win.Close);
            win.Owner = _mainWindow;
            win.ShowDialog();
        }

        private void CreateCatalogItemEditWindow(CatalogItem obj)
        {
            var win = new CatalogItemEditWindow();
            win.DataContext = new CatalogItemEditVM(obj, catalogManager.GetVendors(), catalogManager.UpdateCatalogItem, win.Close);
            win.Owner = _mainWindow;
            win.ShowDialog();
        }

        private void CatalogItemDelete(CatalogItem obj)
        {
            catalogManager.DeleteCatalogItem(obj);
        }

    }
}
