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
            mainVM.MainMenuVM.VendorsRequest += () => ShowEntitiesListWindow<Vendor, VendorVM, VendorEditWindow, VendorsWindow>(catalogManager.GetVendors, catalogManager.AddVendor, catalogManager.UpdateVendor, catalogManager.DeleteVendor);
            mainVM.MainMenuVM.CatalogRequest += () => ShowEntitiesListWindow<CatalogItem, CatalogItemVM, CatalogItemEditWindow, CatalogWindow>(catalogManager.GetCatalog, catalogManager.AddCatalogItem, catalogManager.UpdateCatalogItem, catalogManager.DeleteCatalogItem, catalogManager.GetVendors());
            mainVM.MainMenuVM.PositionsRequest += () => ShowEntitiesListWindow<Position, PositionVM, PositionEditWindow, PositionsListWindow>(positionManager.GetPositions, positionManager.AddPosition, positionManager.UpdatePosition, positionManager.DeletePosition, GetPositionRelationData());
            mainVM.MainMenuVM.EntriesRequest += () => ShowEntitiesListWindow<Entry, EntryVM, EntryEditWindow, EntriesListWindow>(entriesManager.GetEntries, entriesManager.AddEntry, entriesManager.UpdateEntry, entriesManager.DeleteEntry, GetEntryRelationData());

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


        private void ShowEntitiesListWindow<TEntity, TEntityVM, TEntityV, TEntitiesListV>(Func<IEnumerable<TEntity>> getListDelegate, Func<TEntity, bool> addDelegate, Func<TEntity, bool> updateDelegate, Func<TEntity, bool> deleteDelegate, object relationData = null) where TEntity : new() where TEntityV : Window, new() where TEntityVM : IEntityVM<TEntity>, new() where TEntitiesListV : Window, new()
        {
            var vm = new EntitiesListEditVM<TEntity>(getListDelegate);
            vm.AddItemRequest += (obj) => ShowEntityEditDialog<TEntity, TEntityVM, TEntityV>(obj, addDelegate, relationData);
            vm.EditItemRequest += (obj) => ShowEntityEditDialog<TEntity, TEntityVM, TEntityV>(obj, updateDelegate, relationData);
            vm.DeleteItemRequest += (obj) => ShowEntityDeleteDialog<TEntity>(obj, deleteDelegate);
            entriesManager.EntriesChanged += vm.UpdateList;
            var win = new TEntitiesListV { DataContext = vm, Owner = mainWindow };
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
