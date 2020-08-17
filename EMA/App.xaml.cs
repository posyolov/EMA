using System.Windows;
using ViewModel;
using Model;
using Repository;
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
        EntriesManager entriesManager;

        MainVM mainVM;
        MainWindow mainWindow;

        private void OnStartup(object sender, StartupEventArgs e)
        {
            catalogManager = new CatalogManager();
            positionManager = new PositionsManager();
            entriesManager = new EntriesManager();

            mainVM = new MainVM();

            mainVM.MainMenuVM = new MainMenuVM();
            mainVM.MainMenuVM.CatalogRequest += ShowCatalogWindow;
            mainVM.MainMenuVM.VendorsRequest += ShowVendorsWindow;
            mainVM.MainMenuVM.PositionsRequest += ShowPositionsWindow;
            mainVM.MainMenuVM.EntriesRequest += ShowEntriesWindow;

            mainVM.PositionsTreeVM = new PositionsTreeVM(positionManager.GetPositionsTree, positionManager.GetPositionFullData);
            mainVM.PositionsTreeVM.AddEntryRequest += (obj) => ShowEntityEditDialog<Entry, EntryVM, EntryEditWindow>(new Entry { PositionId = obj }, entriesManager.AddEntry, GetEntryRelationData());
            mainVM.PositionsTreeVM.AddPositionRequest += (obj) => ShowEntityEditDialog<Position, PositionVM, PositionEditWindow>(obj, positionManager.AddPosition, GetPositionRelationData());
            mainVM.PositionsTreeVM.EditPositionRequest += (obj) => ShowEntityEditDialog<Position, PositionVM, PositionEditWindow>(obj, positionManager.UpdatePosition, GetPositionRelationData());
            mainVM.PositionsTreeVM.DeletePositionRequest += (obj) => ShowEntityDeleteDialog<Position>(obj, positionManager.DeletePosition);
            positionManager.PositionsChanged += mainVM.PositionsTreeVM.UpdateTree;

            mainVM.EntriesTreeVM = new EntriesTreeVM(entriesManager.GetEntries, entriesManager.GetEntriesTree);
            mainVM.EntriesTreeVM.AddEntryRequest += (obj) => ShowEntityEditDialog<Entry, EntryVM, EntryEditWindow>(obj, entriesManager.AddEntry, GetEntryRelationData());
            mainVM.EntriesTreeVM.AddChildEntryRequest += (obj) => ShowEntityEditDialog<Entry, EntryVM, EntryEditWindow>(obj, entriesManager.AddEntry, GetEntryRelationData());
            mainVM.EntriesTreeVM.EditEntryRequest += (obj) => ShowEntityEditDialog<Entry, EntryVM, EntryEditWindow>(obj, entriesManager.UpdateEntry, GetEntryRelationData());
            mainVM.EntriesTreeVM.DeleteEntryRequest += (obj) => ShowEntityDeleteDialog<Entry>(obj, entriesManager.DeleteEntry);
            entriesManager.EntriesChanged += mainVM.EntriesTreeVM.UpdateList;

            mainWindow = new MainWindow { DataContext = mainVM };
            mainWindow.Show();
        }

        private void ShowEntriesWindow()
        {
            var vm = new EntitiesListEditVM<Entry>(entriesManager.GetEntries);
            vm.AddItemRequest += (obj) => ShowEntityEditDialog<Entry, EntryVM, EntryEditWindow>(obj, entriesManager.AddEntry, GetEntryRelationData());
            vm.EditItemRequest += (obj) => ShowEntityEditDialog<Entry, EntryVM, EntryEditWindow>(obj, entriesManager.UpdateEntry, GetEntryRelationData());
            vm.DeleteItemRequest += (obj) => ShowEntityDeleteDialog<Entry>(obj, entriesManager.DeleteEntry);
            entriesManager.EntriesChanged += vm.UpdateList;
            var win = new EntriesListWindow { DataContext = vm, Owner = mainWindow };
            win.Show();
        }

        private void ShowPositionsWindow()
        {
            var vm = new EntitiesListEditVM<Position>(positionManager.GetPositions);
            vm.AddItemRequest += (obj) => ShowEntityEditDialog<Position, PositionVM, PositionEditWindow>(obj, positionManager.AddPosition, GetPositionRelationData());
            vm.EditItemRequest += (obj) => ShowEntityEditDialog<Position, PositionVM, PositionEditWindow>(obj, positionManager.UpdatePosition, GetPositionRelationData());
            vm.DeleteItemRequest += (obj) => ShowEntityDeleteDialog<Position>(obj, positionManager.DeletePosition);
            positionManager.PositionsChanged += vm.UpdateList;
            var win = new PositionsListWindow { DataContext = vm, Owner = mainWindow };
            win.Show();
        }

        private void ShowCatalogWindow()
        {
            var vm = new EntitiesListEditVM<CatalogItem>(catalogManager.GetCatalog);
            vm.AddItemRequest += (obj) => ShowEntityEditDialog<CatalogItem, CatalogItemVM, CatalogItemEditWindow>(obj, catalogManager.AddCatalogItem, catalogManager.GetVendors());
            vm.EditItemRequest += (obj) => ShowEntityEditDialog<CatalogItem, CatalogItemVM, CatalogItemEditWindow>(obj, catalogManager.UpdateCatalogItem, catalogManager.GetVendors());
            vm.DeleteItemRequest += (obj) => ShowEntityDeleteDialog<CatalogItem>(obj, catalogManager.DeleteCatalogItem);
            catalogManager.CatalogChanged += vm.UpdateList;
            var win = new CatalogWindow { DataContext = vm, Owner = mainWindow };
            win.Show();
        }

        private void ShowVendorsWindow()
        {
            var vm = new EntitiesListEditVM<Vendor>(catalogManager.GetVendors);
            vm.AddItemRequest += (obj) => ShowEntityEditDialog<Vendor, VendorVM, VendorEditWindow>(obj, catalogManager.AddVendor, null);
            vm.EditItemRequest += (obj) => ShowEntityEditDialog<Vendor, VendorVM, VendorEditWindow>(obj, catalogManager.UpdateVendor, null);
            vm.DeleteItemRequest += (obj) => ShowEntityDeleteDialog<Vendor>(obj, catalogManager.DeleteVendor);
            catalogManager.VendorsChanged += vm.UpdateList;
            var win = new VendorsWindow { DataContext = vm, Owner = mainWindow };
            win.Show();
        }


        private void ShowEntitiesListWindow<TEntity, TEntityVM, TEntityV>(Func<IEnumerable<TEntity>> getListDelegate, Func<TEntity, bool> addDelegate, Func<TEntity, bool> updateDelegate, Func<TEntity, bool> deleteDelegate) where TEntity : new() where TEntityV : Window, new() where TEntityVM : IEntityVM<TEntity>, new()
        {
            var vm = new EntitiesListEditVM<TEntity>(getListDelegate);
            vm.AddItemRequest += (obj) => ShowEntityEditDialog<TEntity, TEntityVM, TEntityV>(obj, addDelegate, GetEntryRelationData());
            vm.EditItemRequest += (obj) => ShowEntityEditDialog<TEntity, TEntityVM, TEntityV>(obj, updateDelegate, GetEntryRelationData());
            vm.DeleteItemRequest += (obj) => ShowEntityDeleteDialog<TEntity>(obj, deleteDelegate);
            entriesManager.EntriesChanged += vm.UpdateList;
            var win = new EntriesListWindow { DataContext = vm, Owner = mainWindow };
            win.Show();
        }


        private void ShowEntityEditDialog<TEntity, TEntityVM, TEntityV>(TEntity entity, Func<TEntity, bool> executeDelegate, object relationData = null) where TEntityV : Window, new() where TEntityVM : IEntityVM<TEntity>, new()
        {
            var win = new TEntityV();
            win.DataContext = new EntityEditVM<TEntity, TEntityVM>(entity, executeDelegate, win.Close, relationData);
            win.Owner = mainWindow;
            win.ShowDialog();
        }

        private void ShowEntityDeleteDialog<TEntity>(TEntity entity, Func<TEntity, bool> executeDelegate)
        {
            var win = new EntityDeleteWindow();
            win.DataContext = new EntityDeleteVM<TEntity>(entity, executeDelegate, win.Close);
            win.Owner = mainWindow;
            win.ShowDialog();
        }

        private object[] GetPositionRelationData()
        {
            return new object[] { positionManager.GetPositions(), catalogManager.GetCatalog() };
        }

        private object[] GetEntryRelationData()
        {
            return new object[] { entriesManager.GetEntries(), positionManager.GetPositions(), entriesManager.GetReasons(), entriesManager.GetContinuationCriterias() };
        }
    }
}
