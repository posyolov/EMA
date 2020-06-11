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
        CatalogManager _catalogManager;

        MainVM _mainVM;
        MainWindow _mainWindow;

        private void OnStartup(object sender, StartupEventArgs e)
        {
            _catalogManager = new CatalogManager();

            _mainVM = new MainVM();

            _mainVM.MainMenuVM.CatalogRequest += () =>
            {
                var vm = new EntitiesListEditVM<CatalogItem>(_catalogManager.GetCatalog, CreateCatalogItemAddWindow, CreateCatalogItemEditWindow, CatalogItemDelete);
                _catalogManager.CatalogChanged += vm.UpdateList;
                var win = new CatalogWindow { DataContext = vm, Owner = _mainWindow };
                win.Show();
            };

            _mainVM.MainMenuVM.VendorsRequest += () =>
            {
                var vm = new EntitiesListEditVM<Vendor>(_catalogManager.GetVendors, CreateVendorAddWindow, CreateVendorEditWindow, VendorDelete);
                _catalogManager.VendorsChanged += vm.UpdateList;
                var win = new VendorsWindow { DataContext = vm, Owner = _mainWindow };
                win.Show();
            };

            _mainWindow = new MainWindow { DataContext = _mainVM };
            _mainWindow.Show();
        }

        private void CreateVendorAddWindow()
        {
            var newVendor = new Vendor();

            var vendorEditVM = new VendorEditVM(newVendor);
            var win = new VendorEditWindow { DataContext = vendorEditVM, Owner = _mainWindow };
            vendorEditVM.Ok += () =>
            {
                //add check
                vendorEditVM.Apply();

                _catalogManager.AddVendor(newVendor);
                win.Close();
            };
            win.ShowDialog();
        }

        private void CreateVendorEditWindow(Vendor obj)
        {
            var vendorEditVM = new VendorEditVM(obj);
            var win = new VendorEditWindow { DataContext = vendorEditVM, Owner = _mainWindow };
            vendorEditVM.Ok += () =>
            {
                //add check
                vendorEditVM.Apply();

                _catalogManager.UpdateVendor(obj);
                win.Close();
            };
            win.ShowDialog();
        }

        private void VendorDelete(Vendor obj)
        {
            _catalogManager.DeleteVendor(obj);
        }


        private void CreateCatalogItemAddWindow()
        {
            var newCatalogItem = new CatalogItem();

            var catalogItemEditVM = new CatalogItemEditVM(newCatalogItem, _catalogManager.GetVendors());
            var win = new CatalogItemEditWindow { DataContext = catalogItemEditVM, Owner = _mainWindow };
            catalogItemEditVM.Ok += () =>
            {
                //add check
                catalogItemEditVM.Apply();

                _catalogManager.AddCatalogItem(newCatalogItem);
                win.Close();
            };
            win.ShowDialog();
        }

        private void CreateCatalogItemEditWindow(CatalogItem obj)
        {
            var catalogItemEditVM = new CatalogItemEditVM(obj, _catalogManager.GetVendors());
            var win = new CatalogItemEditWindow { DataContext = catalogItemEditVM, Owner = _mainWindow };
            catalogItemEditVM.Ok += () =>
            {
                //add check
                catalogItemEditVM.Apply();

                _catalogManager.UpdateCatalogItem(obj);
                win.Close();
            };
            win.ShowDialog();
        }

        private void CatalogItemDelete(CatalogItem obj)
        {
            _catalogManager.DeleteCatalogItem(obj);
        }

    }
}
