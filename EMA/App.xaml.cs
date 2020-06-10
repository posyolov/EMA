using System.Windows;
using ViewModel;
using Model;
using Repository.EF;
using System;

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
            _mainVM.MainMenuVM.CatalogRequest += CreateCatalogWindow;
            _mainVM.MainMenuVM.VendorsRequest += CreateVendorsWindow;

            _mainWindow = new MainWindow();
            _mainWindow.DataContext = _mainVM;
            _mainWindow.Show();
        }

        private void CreateCatalogWindow()
        {
            var catalogVM = new CatalogVM(_catalogManager.GetCatalog());
            _catalogManager.CatalogChanged += () => catalogVM.UpdateCatalogList(_catalogManager.GetCatalog());
            catalogVM.AddItemRequest += CreateCatalogItemAddWindow;
            catalogVM.EditItemRequest += CreateCatalogItemEditWindow;
            catalogVM.DeleteItemRequest += DeleteCatalogItem;
            CreateViewModelWindow<CatalogWindow>(catalogVM).Show();
        }

        private void CreateVendorsWindow()
        {
            var vendorsVM = new VendorsVM(_catalogManager.GetVendors());
            _catalogManager.VendorsChanged += () => vendorsVM.UpdateVendorsList(_catalogManager.GetVendors());
            vendorsVM.AddVendorRequest += CreateVendorAddWindow;
            vendorsVM.EditVendorRequest += CreateVendorEditWindow;
            vendorsVM.DeleteVendorRequest += VendorDelete;
            CreateViewModelWindow<VendorsWindow>(vendorsVM).Show();
        }


        private void CreateVendorAddWindow()
        {
            var newVendor = new Vendor();
            var vendorEdit = new VendorEditVM(newVendor);
            var dialogViewModel = new DialogVM(vendorEdit);
            var win = CreateViewModelWindow<VendorEditWindow>(dialogViewModel);
            dialogViewModel.Ok += () =>
            {
                //add check
                newVendor.Name = vendorEdit.Name;

                _catalogManager.AddVendor(newVendor);
                win.Close();
            };
            win.ShowDialog();
        }

        private void CreateVendorEditWindow(Vendor obj)
        {
            var vendorEdit = new VendorEditVM(obj);
            var dialogViewModel = new DialogVM(vendorEdit);
            var win = CreateViewModelWindow<VendorEditWindow>(dialogViewModel);
            dialogViewModel.Ok += () =>
            {
                //add check
                obj.Name = vendorEdit.Name;

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
            var catalogItemEdit = new CatalogItemEditVM(newCatalogItem, _catalogManager.GetVendors());
            var dialogViewModel = new DialogVM(catalogItemEdit);
            var win = CreateViewModelWindow<CatalogItemEditWindow>(dialogViewModel);
            dialogViewModel.Ok += () =>
            {
                //add check
                newCatalogItem.GlobalId = catalogItemEdit.GlobalId;
                newCatalogItem.VendorId = catalogItemEdit.VendorId;
                newCatalogItem.ProductCode = catalogItemEdit.ProductCode;
                newCatalogItem.Title = catalogItemEdit.Title;

                _catalogManager.AddCatalogItem(newCatalogItem);
                win.Close();
            };
            win.ShowDialog();
        }

        private void CreateCatalogItemEditWindow(CatalogItem obj)
        {
            var catalogItemEdit = new CatalogItemEditVM(obj, _catalogManager.GetVendors());
            var dialogViewModel = new DialogVM(catalogItemEdit);
            var win = CreateViewModelWindow<CatalogItemEditWindow>(dialogViewModel);
            dialogViewModel.Ok += () =>
            {
                //add check
                obj.GlobalId = catalogItemEdit.GlobalId;
                obj.VendorId = catalogItemEdit.VendorId;
                obj.ProductCode = catalogItemEdit.ProductCode;
                obj.Title = catalogItemEdit.Title;

                _catalogManager.UpdateCatalogItem(obj);
                win.Close();
            };
            win.ShowDialog();
        }

        private void DeleteCatalogItem(CatalogItem obj)
        {
            _catalogManager.DeleteCatalogItem(obj);
        }


        Window CreateViewModelWindow<TWindow>(object context) where TWindow : Window, new()
        {
            return new TWindow { DataContext = context, Owner = _mainWindow };
        }
    }
}
