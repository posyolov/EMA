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
                _catalogManager.CatalogChanged += () => 
                CreateEntitiesListEditWindow<CatalogWindow, CatalogItem>(
                    _catalogManager.GetCatalog,
                    CreateCatalogItemAddWindow,
                    CreateCatalogItemEditWindow,
                    DeleteCatalogItem).UpdateList(_catalogManager.GetCatalog());
            };

            _mainVM.MainMenuVM.VendorsRequest += () =>
               CreateEntitiesListEditWindow<VendorsWindow, Vendor>(
                    _catalogManager.GetVendors,
                    CreateVendorAddWindow,
                    CreateVendorEditWindow,
                    VendorDelete);

            _mainWindow = new MainWindow();
            _mainWindow.DataContext = _mainVM;
            _mainWindow.Show();
        }

        private void CreateCatalogWindow()
        {
            var catalogVM = new EntitiesListEditVM<CatalogItem>(_catalogManager.GetCatalog());
            _catalogManager.CatalogChanged += () => catalogVM.UpdateList(_catalogManager.GetCatalog());
            catalogVM.AddItemRequest += CreateCatalogItemAddWindow;
            catalogVM.EditItemRequest += CreateCatalogItemEditWindow;
            catalogVM.DeleteItemRequest += DeleteCatalogItem;
            CreateViewModelWindow<CatalogWindow>(catalogVM).Show();
        }

        private void CreateVendorsWindow()
        {
            var vendorsVM = new EntitiesListEditVM<Vendor>(_catalogManager.GetVendors());
            _catalogManager.VendorsChanged += () => vendorsVM.UpdateList(_catalogManager.GetVendors());
            vendorsVM.AddItemRequest += CreateVendorAddWindow;
            vendorsVM.EditItemRequest += CreateVendorEditWindow;
            vendorsVM.DeleteItemRequest += VendorDelete;
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

        private EntitiesListEditVM<TEntity> CreateEntitiesListEditWindow<TWindow, TEntity>(
            Func<IEnumerable<TEntity>> getItemsDelegate,
            Action addItemDelegate,
            Action<TEntity> editItemDelegate,
            Action<TEntity> deleteItemDelegate
            ) where TWindow : Window, new()
        {
            var vm = new EntitiesListEditVM<TEntity>(getItemsDelegate());
            //itemsChangedEvent += () => vm.UpdateList(getItemsDelegate());
            vm.AddItemRequest += addItemDelegate;
            vm.EditItemRequest += editItemDelegate;
            vm.DeleteItemRequest += deleteItemDelegate;
            CreateViewModelWindow<TWindow>(vm).Show();

            return vm;
        }
    }
}
