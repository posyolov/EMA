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
        VendorsManager vendorsManager;
        CatalogManager catalogManager;
        PositionsManager positionManager;
        EntriesManager entriesManager;
        EntryReasonsManager entryReasonsManager;
        EntryContinuationCriteriaManager entryContinuationCriteriaManager;

        MainVM mainVM;
        MainWindow mainWindow;

        private void OnStartup(object sender, StartupEventArgs e)
        {
            vendorsManager = new VendorsManager();
            catalogManager = new CatalogManager();
            positionManager = new PositionsManager();
            entriesManager = new EntriesManager();
            entryReasonsManager = new EntryReasonsManager();
            entryContinuationCriteriaManager = new EntryContinuationCriteriaManager();

            mainVM = new MainVM();

            mainVM.MainMenuVM = new MainMenuVM();
            mainVM.MainMenuVM.VendorsRequest += () => ShowEntitiesListWindow<Vendor, VendorVM, VendorEditWindow, VendorsWindow>(vendorsManager);
            mainVM.MainMenuVM.CatalogRequest += () => ShowEntitiesListWindow<CatalogItem, CatalogItemVM, CatalogItemEditWindow, CatalogWindow>(catalogManager, vendorsManager.Get());
            mainVM.MainMenuVM.PositionsRequest += () => ShowEntitiesListWindow<Position, PositionVM, PositionEditWindow, PositionsListWindow>(positionManager, GetPositionRelationData());
            mainVM.MainMenuVM.EntriesRequest += () => ShowEntitiesListWindow<Entry, EntryVM, EntryEditWindow, EntriesListWindow>(entriesManager, GetEntryRelationData());

            mainVM.PositionsTreeVM = new PositionsTreeVM(positionManager.GetPositionsTree, positionManager.GetPositionFullData);
            mainVM.PositionsTreeVM.AddEntryRequest += (obj) => ShowEntityEditDialog<Entry, EntryVM, EntryEditWindow>(new Entry { PositionId = obj }, entriesManager.Add, GetEntryRelationData());
            mainVM.PositionsTreeVM.AddPositionRequest += (obj) => ShowEntityEditDialog<Position, PositionVM, PositionEditWindow>(obj, positionManager.Add, GetPositionRelationData());
            mainVM.PositionsTreeVM.EditPositionRequest += (obj) => ShowEntityEditDialog<Position, PositionVM, PositionEditWindow>(obj, positionManager.Update, GetPositionRelationData());
            mainVM.PositionsTreeVM.DeletePositionRequest += (obj) => ShowEntityDeleteDialog<Position>(obj, positionManager.Delete);
            positionManager.EntitiesChanged += mainVM.PositionsTreeVM.UpdateTree;

            mainVM.EntriesTreeVM = new EntriesTreeVM(entriesManager.Get, entriesManager.GetEntriesTree);
            mainVM.EntriesTreeVM.AddEntryRequest += (obj) => ShowEntityEditDialog<Entry, EntryVM, EntryEditWindow>(obj, entriesManager.Add, GetEntryRelationData());
            mainVM.EntriesTreeVM.AddChildEntryRequest += (obj) => ShowEntityEditDialog<Entry, EntryVM, EntryEditWindow>(obj, entriesManager.Add, GetEntryRelationData());
            mainVM.EntriesTreeVM.EditEntryRequest += (obj) => ShowEntityEditDialog<Entry, EntryVM, EntryEditWindow>(obj, entriesManager.Update, GetEntryRelationData());
            mainVM.EntriesTreeVM.DeleteEntryRequest += (obj) => ShowEntityDeleteDialog<Entry>(obj, entriesManager.Delete);
            entriesManager.EntitiesChanged += mainVM.EntriesTreeVM.UpdateList;

            mainWindow = new MainWindow { DataContext = mainVM };
            mainWindow.Show();
        }


        private void ShowEntitiesListWindow<TEntity, TEntityVM, TEntityV, TEntitiesListV>(IEntityManager<TEntity> entityManager, object relationData = null) where TEntity : class, new() where TEntityV : Window, new() where TEntityVM : IEntityVM<TEntity>, new() where TEntitiesListV : Window, new()
        {
            var vm = new EntitiesListEditVM<TEntity>(entityManager.Get);
            vm.AddItemRequest += (obj) => ShowEntityEditDialog<TEntity, TEntityVM, TEntityV>(obj, entityManager.Add, relationData);
            vm.EditItemRequest += (obj) => ShowEntityEditDialog<TEntity, TEntityVM, TEntityV>(obj, entityManager.Update, relationData);
            vm.DeleteItemRequest += (obj) => ShowEntityDeleteDialog<TEntity>(obj, entityManager.Delete);
            entityManager.EntitiesChanged += vm.UpdateList;
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
            return new object[] { positionManager.Get(), catalogManager.Get() };
        }

        private object[] GetEntryRelationData()
        {
            return new object[] { entriesManager.Get(), positionManager.Get(), entryReasonsManager.Get(), entryContinuationCriteriaManager.Get() };
        }
    }
}
