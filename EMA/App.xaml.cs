﻿using System.Windows;
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
                var vm = new EntitiesListEditVM<CatalogItem>(
                    catalogManager.GetCatalog, 
                    (obj) => CreateEntityEditDialog<CatalogItem, CatalogItemVM, CatalogItemEditWindow>(catalogManager.AddCatalogItem, obj, catalogManager.GetVendors()),
                    (obj) => CreateEntityEditDialog<CatalogItem, CatalogItemVM, CatalogItemEditWindow>(catalogManager.UpdateCatalogItem, obj, catalogManager.GetVendors()),
                    (obj) => CreateEntityEditDialog<CatalogItem, CatalogItemVM, CatalogItemDeleteWindow>(catalogManager.DeleteCatalogItem, obj, null));
                catalogManager.CatalogChanged += vm.UpdateList;
                var win = new CatalogWindow { DataContext = vm, Owner = _mainWindow };
                win.Show();
            };

            _mainVM.MainMenuVM.VendorsRequest += () =>
            {
                var vm = new EntitiesListEditVM<Vendor>(
                    catalogManager.GetVendors, 
                    (obj) => CreateEntityEditDialog<Vendor, VendorVM, VendorEditWindow>(catalogManager.AddVendor, obj, null), 
                    (obj) => CreateEntityEditDialog<Vendor, VendorVM, VendorEditWindow>(catalogManager.UpdateVendor, obj, null), 
                    (obj) => CreateEntityEditDialog<Vendor, VendorVM, VendorDeleteWindow>(catalogManager.DeleteVendor, obj, null));
                catalogManager.VendorsChanged += vm.UpdateList;
                var win = new VendorsWindow { DataContext = vm, Owner = _mainWindow };
                win.Show();
            };

            _mainWindow = new MainWindow { DataContext = _mainVM };
            _mainWindow.Show();
        }

        private void CreateEntityEditDialog<TEntity, TEntityVM, TEntityV>(Func<TEntity, bool> executeDelegate, TEntity entity,  object relationData) where TEntityV : Window, new() where TEntityVM : IEntityVM<TEntity>, new()
        {
            var win = new TEntityV();
            win.DataContext = new EntityEditVM<TEntity, TEntityVM>(executeDelegate, win.Close, entity, relationData);
            win.Owner = _mainWindow;
            win.ShowDialog();
        }
    }
}
