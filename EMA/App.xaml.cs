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
        PositionsManager positionManager;

        MainVM mainVM;
        MainWindow mainWindow;

        private void OnStartup(object sender, StartupEventArgs e)
        {
            catalogManager = new CatalogManager();
            positionManager = new PositionsManager();

            mainVM = new MainVM();

            mainVM.MainMenuVM.CatalogRequest += () =>
            {
                var vm = new EntitiesListEditVM<CatalogItem>(
                    catalogManager.GetCatalog, 
                    (obj) => CreateEntityEditDialog<CatalogItem, CatalogItemVM, CatalogItemEditWindow>(catalogManager.AddCatalogItem, obj, catalogManager.GetVendors()),
                    (obj) => CreateEntityEditDialog<CatalogItem, CatalogItemVM, CatalogItemEditWindow>(catalogManager.UpdateCatalogItem, obj, catalogManager.GetVendors()),
                    (obj) => CreateEntityEditDialog<CatalogItem, CatalogItemVM, CatalogItemDeleteWindow>(catalogManager.DeleteCatalogItem, obj, null));
                catalogManager.CatalogChanged += vm.UpdateList;
                var win = new CatalogWindow { DataContext = vm, Owner = mainWindow };
                win.Show();
            };

            mainVM.MainMenuVM.VendorsRequest += () =>
            {
                var vm = new EntitiesListEditVM<Vendor>(
                    catalogManager.GetVendors, 
                    (obj) => CreateEntityEditDialog<Vendor, VendorVM, VendorEditWindow>(catalogManager.AddVendor, obj, null), 
                    (obj) => CreateEntityEditDialog<Vendor, VendorVM, VendorEditWindow>(catalogManager.UpdateVendor, obj, null), 
                    (obj) => CreateEntityEditDialog<Vendor, VendorVM, VendorDeleteWindow>(catalogManager.DeleteVendor, obj, null));
                catalogManager.VendorsChanged += vm.UpdateList;
                var win = new VendorsWindow { DataContext = vm, Owner = mainWindow };
                win.Show();
            };

            mainVM.MainMenuVM.PositionsRequest += () =>
            {
                var vm = new EntitiesListEditVM<Position>(
                    positionManager.GetPositions,
                    (obj) => CreateEntityEditDialog<Position, PositionVM, PositionEditWindow>(positionManager.AddPosition, obj, new object[] { positionManager.GetPositions(), catalogManager.GetCatalog() }),
                    (obj) => CreateEntityEditDialog<Position, PositionVM, PositionEditWindow>(positionManager.UpdatePosition, obj, new object[] { positionManager.GetPositions(), catalogManager.GetCatalog() }),
                    (obj) => CreateEntityEditDialog<Position, PositionVM, PositionDeleteWindow>(positionManager.DeletePosition, obj, null));
                positionManager.PositionsChanged += vm.UpdateList;
                var win = new PositionsListWindow { DataContext = vm, Owner = mainWindow };
                win.Show();
            };

            mainVM.PositionsTreeVM = new PositionsTreeVM(positionManager.GetPositionsTree, positionManager.GetPositionFullData);
            positionManager.PositionsChanged += mainVM.PositionsTreeVM.UpdateTree;

            mainWindow = new MainWindow { DataContext = mainVM };
            mainWindow.Show();
        }

        private void CreateEntityEditDialog<TEntity, TEntityVM, TEntityV>(Func<TEntity, bool> executeDelegate, TEntity entity,  object relationData) where TEntityV : Window, new() where TEntityVM : IEntityVM<TEntity>, new()
        {
            var win = new TEntityV();
            win.DataContext = new EntityEditVM<TEntity, TEntityVM>(executeDelegate, win.Close, entity, relationData);
            win.Owner = mainWindow;
            win.ShowDialog();
        }
    }
}
